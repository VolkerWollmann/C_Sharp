using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    public class CSharp
    {
        /// <summary>
        /// test
        /// #local method #local function
        /// </summary>
        public static void LocalFunction()
        {
            int GetOne()
            {
                return 1;
            }

            var one = GetOne();

            Assert.AreEqual(one, 1);
        }

        public static void MultiLineStringConstant()
        {
            string animals = @"donkey
                               dog
                               cat";

            Assert.IsNotNull(animals);
        }

        #region Implict Operator
        public readonly struct Digit
        {
            private readonly byte _digit;

            public Digit(byte digit)
            {
                if (digit > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(digit), "Digit cannot be greater than nine.");
                }
                this._digit = digit;
            }

            public static implicit operator byte(Digit d) => d._digit;
            public static explicit operator Digit(byte b) => new Digit(b);

            public override string ToString() => $"{_digit}";
        }



        public static void ImplicitExplicitOperator()
        {
            var d = new Digit(7);

            // #implicit operator
            byte number = d;
            Assert.AreEqual(number, 7);

            //  #explicit operator
            Digit digit = (Digit)number;
            Assert.AreEqual(digit, 7);
        }

        #endregion

        #region Named Parameter

        // #named parameter
        public static void NamedParmaters()
        {
            int Sum( int parameter1, int parameter2)
            {
                return parameter1 + parameter2;
            }

            int result = Sum(parameter2: 5, parameter1: 3);

            Assert.AreEqual(result, 8);
        }

        #endregion

        #region Optional Parameter
        // #optinal parameter
        public static void OptionalParameters()
        {
            int Sum(int parameter1, int parameter2=3)
            {
                return parameter1 + parameter2;
            }

            int result = Sum(2);
            Assert.AreEqual(result, 5);

            int result2 = Sum(1, 2);
            Assert.AreEqual(result2, 3);

        }
        #endregion

    }



}
