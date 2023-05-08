using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    public class DebugStepThrough
    {
        public static void Level2()
        {
            // Put Breakpoint here
            ;
        }

        [DebuggerStepThrough]
        public static void Level1()
        {
            Level2();
        }
        public static void StepThroughExample()
        {
            Level1();
        }
    }
}
