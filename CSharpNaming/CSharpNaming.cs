using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpNaming
{
    /// <summary>
    /// Naming conventions
    /// 
    /// https://www.dofactory.com/reference/csharp-coding-standards
    /// 

    #region I. PascalCasing for classes
    #endregion
    /// <summary>
    /// 
    /// </summary>
    public class NamingConvention
    {
        #region supporter classes
        private class UserGroup { }
        private class Assignment { }
        private class CustomerId { }
        #endregion

        private void MyFunction()
        {
            #region II. do use camelCasing for local variables and method arguments. 
            // camelCasing for local variables
            int localVariable = 1;

            // Avoid : hungarinCasing for type information
            int iCounter = 0;

            if ((localVariable == 0) || (iCounter == 0))
                return;
            #endregion

            #region II. a Use Verb Noun for Methods
            void ClearStack()
            {

            }

            ClearStack();
            #endregion
        }

        #region III. do use camelCasing for local variables and method arguments. 
        //      do notuse Hungarian notation or any other type identification in identifiers

        // Correct
        int counter = 0;
        string name = "";

        // Avoid
        int iCounter = 0;
        string strName = "";
        #endregion

        #region IV. do not use Screaming Caps for constants or readonly variables 
        // Correct : PascalCasing
        public const string ShippingType = "DropShip";

        // Avoid : Sreaming caps
        public const string SHIPPINGTYPE = "DropShip";
        #endregion

        #region V. Avoid using Abbreviations. 
        //    Exceptions: abbreviations commonly used as names, such as Id, Xml, Ftp, Uri

        // Correct
        UserGroup userGroup;
        Assignment employeeAssignment;

        // Avoid
        UserGroup usrGrp;
        Assignment empAssignment;

        // Exceptions
        CustomerId customerId;
        #endregion

        #region VI. do use PascalCasing for abbreviations 3 characters or more (2 chars are both uppercase) 
        // here for the type name
        //HtmlHelper htmlHelper;
        //FtpTransfer ftpTransfer;
        //UIControl uiControl;
        #endregion

        #region VII. Do not use Underscores in identifiers. 
        //      Exception: you can prefix private static variables with an underscore.
        // Correct
        public DateTime clientAppointment;
        public TimeSpan timeLeft;

        // Avoid
        public DateTime client_Appointment;
        public TimeSpan time_Left;

        // Exception : private static
        private static DateTime _registrationDate = DateTime.Now;
        #endregion

        #region VIII. Do use predefined type names instead of system type names like Int16, Single, UInt64, etc
        // Correct
        string firstName = "";
        int lastIndex = 0;
        bool isSaved = false;

        // Avoid
        String lastName = "";
        Int32 firstIndex = 0;
        Boolean isDeleted = false;
        #endregion

        #region IX. douse implicit type var for local variable declarations. 
        // Exception: primitive types (int, string, double, etc) use predefined names.
        private void Test()
        {
            var stream = File.Create("");
            var customers = new Dictionary<int, int>();

            // Exceptions
            int index = 100;
            string timeSheet = "";
            bool isCompleted = false;

            if ((index == 0) || (timeSheet == "") || isCompleted)
                return;

            if ((firstIndex == 0) || (firstName == "") || isDeleted)
                return;

            if ((lastIndex == 0) || (lastName == "") || isSaved)
                return;
        }
        #endregion

        #region X. Do use noun or noun phrases to name a class. 
        public class Employee
        {
        }
        public class BusinessLocation
        {
        }
        public class DocumentCollection
        {
        }
        #endregion

        #region XI. Do prefix interfaces with the letter I.  
        //     Interface names are noun (phrases) or adjectives.
        public interface IShape
        {
        }
        public interface IShapeCollection
        {
        }
        public interface IGroupable
        {
        }
        #endregion

        #region XII. organize namespaces with a clearly defined structure
        //namespace Company.Product.Module.SubModule { } 
        //namespace Product.Module.Component { }
        //namespace Product.Layer.Module.Group { }
        #endregion

        #region XIII. Do vertically align curly brackets. 
        private class Example
        {
            private void Method1()
            {
            }
        }
        #endregion

        #region XIV. Do declare all member variables at the top of a class, with static variables at the very top. 
        private class Account
        {
            public static string BankName="";
            public static decimal Reserves= 0;

            public string Number { get; set; }
            public DateTime DateOpened { get; set; }
            public DateTime DateClosed { get; set; }
            public decimal Balance { get; set; }

            // Constructor
            public Account()
            {
                // ...
            }
        }
        #endregion

        #region XV.douse singular names for enums.Exception: bit field enums.
        // Correct
        public enum Color
        {
            Red,
            Green,
            Blue,
            Yellow,
            Magenta,
            Cyan
        }

        // Exception
        [Flags]
        public enum Dockings
        {
            None = 0,
            Top = 1,
            Right = 2,
            Bottom = 4,
            Left = 8
        }
        #endregion

        #region XVI. Do not explicitly specify a type of an enum or values of enums
        //      (except bit fields)
        
        // Don't
        public enum WrongDirection : long
        {
            North = 1,
            East = 2,
            South = 3,
            West = 4
        }

        // Correct
        public enum Direction
        {
            North,
            East,
            South,
            West
        }
        #endregion

        #region XVII. Do notsuffix enum names with Enum
        // Don't
        public enum CoinEnum
        {
            Penny,
            Nickel,
            Dime,
            Quarter,
            Dollar
        }

        // Correct
        public enum Coin
        {
            Penny,
            Nickel,
            Dime,
            Quarter,
            Dollar
        }
        #endregion

        #region suppress warnings
        private void ConsumeVaraibles()
        {
            userGroup = null;
            usrGrp = null;
            employeeAssignment = null;
            empAssignment = null;
            customerId = null;

            if ( (userGroup == null) || (usrGrp == null ) || (employeeAssignment == null) ||
                 (empAssignment == null) || ( customerId == null ) )
                return;
            
            if ((counter == 0) || (iCounter == 0))
                return;

            if ((name == "") || (strName == ""))
                return;
            if (_registrationDate.CompareTo(DateTime.Now) == 0)
                return;
        }
        #endregion

        public static void ShowNamingConventions()
        {

        }
    }
}
