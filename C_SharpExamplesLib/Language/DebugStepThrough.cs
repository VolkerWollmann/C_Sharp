using System.Diagnostics;

namespace C_SharpExamplesLib.Language
{
    public abstract class DebugStepThrough
    {
	    private static void Level2()
        {
            // Put Breakpoint here
            // ReSharper disable once EmptyStatement
            ;
        }

        [DebuggerStepThrough]
        private static void Level1()
        {
            Level2();
        }
        public static void StepThroughExample()
        {
            Level1();
        }
    }
}
