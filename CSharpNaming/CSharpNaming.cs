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
    public class NamingConvertion
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
            int iCounter=0;

            if ((localVariable == 0) || (iCounter == 0))
                return;
            #endregion
        }

        #region III. do use camelCasing for local variables and method arguments. 
        //      do notuse Hungarian notation or any other type identification in identifiers

        // Correct
        int counter=0;
        string name="";

        // Avoid
        int iCounter=0;
        string strName="";
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
        Assignment employeeAssignment ;

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
        string firstName ="";
        int lastIndex=0;
        bool isSaved=false;

        // Avoid
        String lastName="";
        Int32 firstIndex=0;
        Boolean isDeleted=false;
        #endregion

        #region IX. douse implicit type var for local variable declarations. 
        // Exception: primitive types (int, string, double, etc) use predefined names.
        private void Test()
        {
            var stream = File.Create("");
            var customers = new Dictionary<int,int>();

            // Exceptions
            int index = 100;
            string timeSheet="";
            bool isCompleted=false;

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
    }
}
