using System.Diagnostics;

namespace C_SharpExamplesLib.Language
{
    public abstract class DebugStepThrough
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
