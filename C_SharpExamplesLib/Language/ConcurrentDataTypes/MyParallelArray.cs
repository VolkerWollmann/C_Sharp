using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.ConcurrentDataTypes
{
    internal class MyParallelArray
    {
        int _maxX, _maxY;
        int[,] _theArray;

        internal MyParallelArray(int x, int y)
        {
            _maxX = x;
            _maxY = y;

            _theArray = new int[x, y];

        }

        internal delegate void FieldOperationDelegate(int x, int y);

        internal FieldOperationDelegate _theOperation;

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

        internal async void PerformTest(FieldOperationDelegate theDelegate)
        {
            Console.WriteLine("Array size: {0} {1} ", _maxX, _maxY);
            _theOperation = theDelegate;
            DateTime start = DateTime.Now;
            for (int x = 0; x < _maxX; x++)
            {
                for (int y = 0; y < _maxY; y++)
                {
                    this._theOperation(x, y);
                }
            }

            TimeSpan t1 = DateTime.Now.Subtract(start);

            Console.WriteLine("Non parallel   : {0} ", t1);
        }

        internal async void PerformTestFieldParallel(FieldOperationDelegate theDelegate)
        {
            _theOperation = theDelegate;
            DateTime pStart = DateTime.Now;
            var tasks = new List<System.Threading.Tasks.Task>();
            for (int x = 0; x < _maxX; x++)
            {
                for (int y = 0; y < _maxY; y++)
                {
                    tasks.Add(
                        System.Threading.Tasks.Task.Run(DoOperationAsyncParallel(x, y, _theOperation)));
                }
            }

            await System.Threading.Tasks.Task.WhenAll(tasks);
            TimeSpan t2 = DateTime.Now.Subtract(pStart);
            Console.WriteLine("Field parallel : {0} ", t2);

        }

        internal async void PerformTestColumnParallel(FieldOperationDelegate theDelegate)
        {
            _theOperation = theDelegate;
            DateTime pStart = DateTime.Now;
            var tasks = new List<System.Threading.Tasks.Task>();
            for (int x = 0; x < _maxX; x++)
            {
                tasks.Add(
                    System.Threading.Tasks.Task.Run(DoOperationAsyncParallel(x, 0,_theOperation)));

            }

            await System.Threading.Tasks.Task.WhenAll(tasks);
            TimeSpan t2 = DateTime.Now.Subtract(pStart);
            Console.WriteLine("Column parallel: {0} ", t2);

        }
    }

    public class MyParallelArrayTest
    {
        public static void Test()
        {
            MyParallelArray myParallelArray = new MyParallelArray(30, 30);
            myParallelArray.PerformTest(myParallelArray.SetToOne);
            myParallelArray.PerformTestFieldParallel(myParallelArray.SetToOne);
            myParallelArray.PerformTestColumnParallel(myParallelArray.SetToOneByColumn);

            for (int i = 0; i < 150; i++)
            {
                System.Threading.Thread.Sleep(100);
                
            }
        }
    }
}
