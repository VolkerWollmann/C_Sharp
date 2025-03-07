﻿using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNew
{
    public abstract class MyVector
    {
        // #vector
        public static void Test()
        {
            bool b = Vector.IsHardwareAccelerated;
            Assert.IsTrue(b);

            int vectorSize = Vector<int>.Count;
            Assert.AreEqual(8, vectorSize);

            int[] vector1Data = new int[8];
            int[] vector2Data = new int[8];
            for (int i = 0; i < 8; i++)
            {
                vector1Data[i] = i;
                vector2Data[i] = i;
            }

            Vector<int> vector1 = new Vector<int>(vector1Data);
            Vector<int> vector2 = new Vector<int>(vector2Data);

            Vector<int> vector3 = vector1 + vector2;
            
            for(int i=0; i < 8; i++ )
            {
                Assert.AreEqual(2*i, vector3[i]);
            }

        }
    }
}
