﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	/// <summary>
	/// 
	/// </summary>
	public class MyVirtual
	{
        public virtual int One()
        {
            return 1;
        }

    }

    public class MyConcrete1 : MyVirtual
    {
    }

    public class MyConcrete2 : MyVirtual
    {
        public override int One()
        {
            return 2;
        }
    }

    public class VirtualTest
    {
        public static void Test()
        {
            MyConcrete1 concrete1 = new MyConcrete1();
            int t1 = concrete1.One();
            Assert.AreEqual(t1, 1);

            MyConcrete2 concrete2 = new MyConcrete2();
            int t2 = concrete2.One();
            Assert.AreEqual(t2,2);
        }
    }

}
