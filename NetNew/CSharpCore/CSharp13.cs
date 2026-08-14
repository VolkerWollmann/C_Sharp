using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNew
{
    public abstract class CSharp13
    {
        #region params collections
        // #params #collections
        // Until C# 12 a params parameter had to be an array. Now any collection type works.
        private static int Sum(params ReadOnlySpan<int> numbers)
        {
            int sum = 0;
            foreach (int number in numbers)
                sum += number;

            return sum;
        }

        private static string Join(params List<string> parts) => string.Join("-", parts);

        public static void ParamsCollections()
        {
            // No int[] is allocated here, the arguments land on the stack.
            Assert.AreEqual(6, Sum(1, 2, 3));
            Assert.AreEqual(0, Sum());

            Assert.AreEqual("a-b-c", Join("a", "b", "c"));
        }
        #endregion

        #region new lock type
        // #lock #System.Threading.Lock
        private static readonly Lock CounterLock = new();

        public static void NewLockType()
        {
            int counter = 0;

            Parallel.For(0, 1000, _ =>
            {
                // CounterLock is a System.Threading.Lock, not a plain object.
                // The compiler notices that and emits EnterScope() instead of Monitor.Enter/Exit.
                lock (CounterLock)
                    counter++;
            });

            Assert.AreEqual(1000, counter);
        }
        #endregion

        #region implicit index access in object initializers
        // #index #objectinitializer
        private class Countdown
        {
            public int[] Values { get; } = new int[5];
        }

        public static void ImplicitIndexInObjectInitializer()
        {
            // ^1 from the end is now allowed inside an object initializer.
            Countdown countdown = new() { Values = { [^1] = 1, [^2] = 2 } };

            Assert.AreEqual(1, countdown.Values[4]);
            Assert.AreEqual(2, countdown.Values[3]);
        }
        #endregion

        #region escape sequence for ESC
        // #escape
        public static void EscapeSequence()
        {
            // \e is the ESC character. The old \x1b was risky, because \x eats
            // as many hex digits as it finds: "\x1b[31m" would swallow the b.
            string red = "\e[31m";

            Assert.AreEqual((char)0x1b, red[0]);
            Assert.AreEqual(5, red.Length);
        }
        #endregion

        #region allows ref struct
        // #generic #constraint #refstruct
        private static string TypeNameOf<T>(T value) where T : allows ref struct
        {
            return typeof(T).Name;
        }

        public static void AllowsRefStruct()
        {
            Span<int> span = stackalloc int[3];

            // Without the "allows ref struct" constraint Span<int> could not be
            // used as a type argument at all, because a ref struct must not be boxed.
            Assert.AreEqual("Span`1", TypeNameOf(span));
            Assert.AreEqual("Int32", TypeNameOf(42));
        }
        #endregion

        #region partial properties
        // #partial #property
        private partial class Config
        {
            // Declaring part: no body, like a partial method.
            public partial string Name { get; }
        }

        private partial class Config
        {
            // Implementing part, in real life written by a source generator.
            public partial string Name { get => "C# 13"; }
        }

        public static void PartialProperties()
        {
            Assert.AreEqual("C# 13", new Config().Name);
        }
        #endregion
    }
}
