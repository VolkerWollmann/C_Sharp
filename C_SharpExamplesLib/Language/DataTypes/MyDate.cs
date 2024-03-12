using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.DataTypes
{
    public class MyDate
    {
        /// <summary>
        /// #DateTime #equal #comparison
        /// </summary>
        public static void DateTimeComparison()
        {
            DateTime d1 = new DateTime(2024, 3, 11, 11, 17, 23, DateTimeKind.Utc);
            DateTime d2 = new DateTime(2024, 3, 11, 12, 17, 23, DateTimeKind.Local);

            bool b1 = d1.Equals(d2);
            Assert.AreEqual(false, b1);

            DateTime d1L = d1.ToLocalTime();
            DateTime d2L = d2.ToLocalTime();

            bool b2 = d1L.Equals(d2L);
            Assert.AreEqual(true,b2);
        }
    }
}
