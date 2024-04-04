using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Pipes;
using System.Text;

namespace CSharpCore
{
    public class NamedPipe
    {
        private class StreamString
        {
            private readonly Stream _ioStream;
            private readonly UnicodeEncoding _streamEncoding;

            public StreamString(Stream ioStream)
            {
                this._ioStream = ioStream;
                _streamEncoding = new UnicodeEncoding();
            }

            public string ReadString()
            {
                int len = 0;

                len = _ioStream.ReadByte() * 256;
                len += _ioStream.ReadByte();
                byte[] inBuffer = new byte[len];
                _ioStream.Read(inBuffer, 0, len);

                return _streamEncoding.GetString(inBuffer);
            }

            public int WriteString(string outString)
            {
                byte[] outBuffer = _streamEncoding.GetBytes(outString);
                int len = outBuffer.Length;
                if (len > UInt16.MaxValue)
                {
                    len = (int)UInt16.MaxValue;
                }
                _ioStream.WriteByte((byte)(len / 256));
                _ioStream.WriteByte((byte)(len & 255));
                _ioStream.Write(outBuffer, 0, len);
                _ioStream.Flush();

                return outBuffer.Length + 2;
            }
        }

        private static string _result;

        private static void NamedPipeReader()
        {
            NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("testPipe", PipeDirection.InOut);

            // Wait for a client to connect
            pipeServer.WaitForConnection();

            StreamString ss = new StreamString(pipeServer);

            _result = ss.ReadString();
            
        }

        private static void NamedPipeWriter()
        {
            var pipeClient =
                new NamedPipeClientStream(".", "testPipe",
                    PipeDirection.InOut, PipeOptions.None);
            
            pipeClient.Connect();
            
            var ss = new StreamString(pipeClient);

            ss.WriteString("Donkey");
        }

        public static void NamedPipeTest()
        {
            _result = "";
            
            Task t1 = Task.Factory.StartNew(NamedPipeReader);
            Task t2 = Task.Factory.StartNew(NamedPipeWriter);
            
            Task.WaitAll(t1, t2);
            
            Assert.AreEqual("Donkey",_result);
        }
        
    }
}
