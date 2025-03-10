﻿using SysIOMem = System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNew.ProcessCommunication
{
	/// <summary>
	/// #MemoryMappedFile
	/// </summary>
	public abstract class MemoryMappedFile
    {
        private static SysIOMem.MemoryMappedFile _memoryMappedFile;
        private static void MemoryMappedFileWriter()
        {
	        using var accessor = _memoryMappedFile.CreateViewAccessor(0, 100);
	        accessor.Write(0, 'D');  // a char are 2 bytes !!!
	        accessor.Write(2, 'o');
	        accessor.Write(4, 'g');
                    
	        accessor.Flush();
        }

        private static void MemoryMappedFileReader()
        {
            Thread.Sleep(1000);

            using var accessor = _memoryMappedFile.CreateViewAccessor(2, 100);
            char c1 = accessor.ReadChar(0);
            Assert.AreEqual('o', c1);

            char c2 = accessor.ReadChar(2);
            Assert.AreEqual('g', c2);
        }

        public static void MemoryMappedFileTest()
        {
            _memoryMappedFile = SysIOMem.MemoryMappedFile.CreateOrOpen("MyMemoryMappedFile", 5000);
            Task t1 = Task.Factory.StartNew(MemoryMappedFileWriter);
            Task t2 = Task.Factory.StartNew(MemoryMappedFileReader);

            Task.WaitAll(t1, t2);
        }
    }
}
