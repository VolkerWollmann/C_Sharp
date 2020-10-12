using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
    public class CSharp
    {
        /// <summary>
        /// #local method #local function
        /// </summary>
        public static void Test()
        {
            int GetOne()
            {
                return 1;
            }

            var one = GetOne();
        }
    }
}
