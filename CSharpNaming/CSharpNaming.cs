﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

    #region I. PascalCasing for classes : public class NamingConvention
    #endregion
    public class NamingConvention
    {
        #region supporter classes
        private class UserGroup { }
        private class Assignment { }
        private class CustomerId { }
        #endregion

        private void MyFunction()
        {
            #region II. Do use camelCasing for local variables and method arguments: int localVariable = 1;
            // camelCasing for local variables
            int localVariable = 1;
            #endregion

            #region

            // Avoid : hungarinCasing for type information
            int iCounter = 0;

            if ((localVariable == 0) || (iCounter == 0))
                return;
            #endregion

            #region II. Use Verb Noun for Methods: void ClearStack()
            void ClearStack() {}
            #endregion

            #region
            ClearStack();
            #endregion
        }

        #region III. Do use camelCasing for local variables and method arguments: int counter=0
        //      do notuse Hungarian notation or any other type identification in identifiers
        // Correct
        int counter = 0;
        string name = "";

        // Avoid
        int iCounter = 0;
        string strName = "";
        #endregion

        #region IV. Do not use Screaming Caps for constants or readonly variables:  const string ShippingType = "DropShip";   Avoid: const string SHIPPINGTYPE = "DropShip";
        // Correct : PascalCasing
        public const string ShippingType = "DropShip";

        // Avoid : Sreaming caps
        public const string SHIPPINGTYPE = "DropShip";
        #endregion

        #region V. Avoid using Abbreviations: UserGroup userGroup;     Avoid UserGroup usrGrp;
        //    Exceptions: abbreviations commonly used as names, such as Id, Xml, Ftp, Uri

        // Correct
        UserGroup userGroup;
        Assignment employeeAssignment;

        // Avoid
        UserGroup usrGrp;
        Assignment empAssignment;

        // Exceptions: Id
        CustomerId customerId;
        #endregion

        #region VI. Do use PascalCasing for abbreviations 3 characters or more (2 chars are both uppercase) : htmlHelper
        // here for the type name
        //HtmlHelper htmlHelper;
        //FtpTransfer ftpTransfer;
        //UIControl uiControl;
        #endregion

        #region VII. Do not use Underscores in identifiers: public DateTime clientAppointment;    Avoid: client_Appointment;
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

        #region IX. Do use implicit type var for local variable declarations: var stream = File.Create("");
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

        #region X. Do use noun or noun phrases to name a class: public class Employee, public class DocumentCollection
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

        #region XI. Do prefix interfaces with the letter I: public interface IShape 
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

        #region XII. Organize namespaces with a clearly defined structure: namespace Company.Product.Module.SubModule
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

            #region
            // Constructor
            public Account()
            {
                // ...
            }
            #endregion
        }
        #endregion

        #region XV. Do use singular names for enums.Exception: bit field enums: public enum Color { Red, ... 
        // Correct
        public enum Color
        {
            Red,
            Green,
        }

        // Exception
        [Flags]
        public enum Dockings
        {
            None = 0,
            Top = 1,
            Bottom = 4,
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

        #region XVII. Do notsuffix enum names with Enum : enum Coin   Avoid: enum CoinEnum
        // Don't
        public enum CoinEnum
        {
            Penny,
            Nickel
        }

        // Correct
        public enum Coin
        {
            Penny,
            Nickel,
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

        #region Test
        public static void ShowNamingConventions()
        {
        }
        #endregion
    }
}
