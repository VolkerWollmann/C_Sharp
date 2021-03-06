﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    // #reflection
    internal class Ship
    {
        public string Name { get; }
        public int Speed { private set; get; }

        // ReSharper disable once UnusedMember.Global
        public bool SetSpeed(int speed)
        {
            if (speed < -5 || speed > 20)
                return false;

            Speed = speed;

            return true;
        }

        public Ship(string name)
        {
            Name = name;
        }
    }

    public class MyReflection
    {
        public static void Test()
        {
            Type t = typeof(Ship);

            ConstructorInfo[] ci =  t.GetConstructors();

            object ship = ci[0].Invoke(new object[] {"HMS Victory"});

            MethodInfo mi = t.GetMethods().First(m => m.Name == "SetSpeed");

            mi.Invoke(ship, new object[] {5});

            Assert.AreEqual(((Ship)ship).Speed, 5);
        }

    }
}
