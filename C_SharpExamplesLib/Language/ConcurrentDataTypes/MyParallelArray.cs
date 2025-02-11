using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.ConcurrentDataTypes
{
    internal class MyParallelArray
    {
	    readonly int _maxX;
	    readonly int _maxY;
	    readonly int[,] _theArray;

        internal MyParallelArray(int x, int y)
        {
            _maxX = x;
            _maxY = y;

            _theArray = new int[x, y];
            
            Assert.IsNotNull(_theArray);

        }

        internal delegate void FieldOperationDelegate(int x, int y);

        private FieldOperationDelegate? _theOperation;

        private static Action DoOperationAsyncParallel(int x, int y, FieldOperationDelegate operation)
        {
            return
                () => { operation(x, y); };
        }

        internal void SetToOne(int x, int y)
        {
            for(int z=0; z < 10000000; z++)
                _theArray[x, y] = 1;
        }

        internal void SetToOneByColumn(int x, int v)
        {
            for (int y = 0; y < _maxY; y++)
            {
                for (int z = 0; z < 10000000; z++)
                    _theArray[x, y] = 1;
            }
        }

        internal void PerformTest(FieldOperationDelegate theDelegate)
        {
            Console.WriteLine("Array size: {0} {1} ", _maxX, _maxY);
            _theOperation = theDelegate;
            DateTime start = DateTime.Now;
            for (int x = 0; x < _maxX; x++)
            {
                for (int y = 0; y < _maxY; y++)
                {
                    _theOperation(x, y);
                }
            }

            TimeSpan t1 = DateTime.Now.Subtract(start);

            Console.WriteLine("Non parallel   : {0} ", t1);
        }

        internal async Task PerformTestFieldParallel(FieldOperationDelegate theDelegate)
        {
            _theOperation = theDelegate;
            DateTime pStart = DateTime.Now;
            var tasks = new List<Task>();
            for (int x = 0; x < _maxX; x++)
            {
                for (int y = 0; y < _maxY; y++)
                {
                    tasks.Add(
                        Task.Run(DoOperationAsyncParallel(x, y, _theOperation)));
                }
            }

            await Task.WhenAll(tasks);
            TimeSpan t2 = DateTime.Now.Subtract(pStart);
            Console.WriteLine("Field parallel : {0} ", t2);

        }

        internal async Task PerformTestColumnParallel(FieldOperationDelegate theDelegate)
        {
            _theOperation = theDelegate;
            DateTime pStart = DateTime.Now;
            var tasks = new List<Task>();
            for (int x = 0; x < _maxX; x++)
            {
                tasks.Add(
                    Task.Run(DoOperationAsyncParallel(x, 0,_theOperation)));

            }

            await Task.WhenAll(tasks);
            TimeSpan t2 = DateTime.Now.Subtract(pStart);
            Console.WriteLine("Column parallel: {0} ", t2);

        }
    }

    public abstract class MyParallelArrayTest
    {
        public static void Test()
        {
            MyParallelArray myParallelArray = new MyParallelArray(30, 30);
            myParallelArray.PerformTest(myParallelArray.SetToOne);
            _ = myParallelArray.PerformTestFieldParallel(myParallelArray.SetToOne);
            _ = myParallelArray.PerformTestColumnParallel(myParallelArray.SetToOneByColumn);

            for (int i = 0; i < 150; i++)
            {
                Thread.Sleep(100);
                
            }
        }
    }
}
