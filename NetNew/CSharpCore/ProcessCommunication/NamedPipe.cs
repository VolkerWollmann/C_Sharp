using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNew.ProcessCommunication
{
    public abstract class NamedPipe
    {
        private class StreamString(Stream ioStream)
        {
	        private readonly UnicodeEncoding _streamEncoding = new();

            public string ReadString()
            {
	            var len = ioStream.ReadByte() * 256;
                len += ioStream.ReadByte();
                byte[] inBuffer = new byte[len];
                int r = ioStream.Read(inBuffer, 0, len);
                Assert.IsTrue(r>=0);
                return _streamEncoding.GetString(inBuffer);
            }

            public int WriteString(string outString)
            {
                byte[] outBuffer = _streamEncoding.GetBytes(outString);
                int len = outBuffer.Length;
                if (len > UInt16.MaxValue)
                {
                    len = UInt16.MaxValue;
                }
                ioStream.WriteByte((byte)(len / 256));
                ioStream.WriteByte((byte)(len & 255));
                ioStream.Write(outBuffer, 0, len);
                ioStream.Flush();

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

            int result = ss.WriteString("Donkey");
            Assert.IsTrue(result > 0);
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
