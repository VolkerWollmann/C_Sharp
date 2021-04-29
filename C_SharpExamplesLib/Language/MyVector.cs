using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vector = System.Windows.Vector;

namespace C_Sharp.Language
{
    public class MyVector
    {
        // #vector
        public static void Test()
        {
            int[] vector1Data = new int[100];
            vector1Data[0] = 41;
            Vector<int> vector1 = new Vector<int>(vector1Data);
            
            int[] vector2Data = new int[100];
            vector2Data[0] = 1;
            Vector<int> vector2 = new Vector<int>(vector2Data);

            Vector<int> vector3 = vector1 + vector2;
            Assert.IsTrue(vector3[0] == 42);

        }
    }
}
