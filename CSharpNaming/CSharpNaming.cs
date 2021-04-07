using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNaming
{
    /// <summary>
    /// Naming conventions 
    /// https://www.dofactory.com/reference/csharp-coding-standards
    /// </summary>

    #region I. PascalCasing for classes : public class NamingConvention
    #endregion
    public class NamingConvention
    {
        #region supporter classes
        private class UserGroup { }
        private class Assignment { }
        private class CustomerId { }
        #endregion

        private void FunctionII()
        {
            #region II. Do use camelCasing for local variables and method arguments: int localVariable = 1;
            // camelCasing for local variables
            int localVariable = 1;
            // Avoid : hungarian casing for type information
            int iMaximum = 0;
            #endregion

            #region II. Use Verb Noun for Methods: void ClearStack()
            void ClearStack() {}
            #endregion

            #region
            Assert.AreEqual(localVariable,0);
            Assert.AreEqual(iMaximum,0);
            ClearStack();
            #endregion
        }

        #region III. Do use camelCasing for local variables and method arguments: int counter=0
        //      do not use Hungarian notation or any other type identification in identifiers
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

        // Avoid : Screaming caps
        // ReSharper disable once IdentifierTypo
        // ReSharper disable once InconsistentNaming
        public const string SHIPPINGTYPE = "DropShip";
        #endregion

        private void FunctionVI()
        {
            #region V. Avoid using Abbreviations: UserGroup userGroup;     Avoid UserGroup usrGrp;

            //    Exceptions: abbreviations commonly used as names, such as Id, Xml, Ftp, Uri

            // Correct
            // ReSharper disable once JoinDeclarationAndInitializer
            UserGroup userGroup;
            // ReSharper disable once JoinDeclarationAndInitializer
            Assignment employeeAssignment;

            // Avoid
            // ReSharper disable once JoinDeclarationAndInitializer
            UserGroup usrGrp;
            // ReSharper disable once JoinDeclarationAndInitializer
            Assignment empAssignment;
            // Exceptions: Id
            // ReSharper disable once JoinDeclarationAndInitializer
            CustomerId customerId;

            #endregion

            #region
            userGroup = new UserGroup();
            usrGrp = null;
            employeeAssignment = new Assignment();
            empAssignment = null;
            customerId = new CustomerId();

            Assert.IsNotNull(userGroup);
            Assert.IsNull(usrGrp);
            Assert.IsNotNull(employeeAssignment);
            Assert.IsNull(empAssignment);
            Assert.IsNotNull(customerId);
            #endregion

        }

        #region VI. Do use PascalCasing for abbreviations 3 characters or more (2 chars are both uppercase) : htmlHelper
        // here for the type name
        //HtmlHelper htmlHelper;
        //FtpTransfer ftpTransfer;
        //UIControl uiControl;
        #endregion

        #region VII. Do not use Underscores in identifiers: public DateTime clientAppointment;    Avoid: client_Appointment;
        //      Exception: you can prefix private static variables with an underscore.
        // Correct
        public DateTime ClientAppointment;
        public TimeSpan TimeLeft;

        // Avoid
        // ReSharper disable once InconsistentNaming
        public DateTime client_Appointment;

        // ReSharper disable once InconsistentNaming
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

            #region 
            Assert.IsNotNull(firstName);
            Assert.AreEqual(lastIndex,0);

            Assert.IsNotNull(stream);
            Assert.IsNotNull(customers);
            Assert.IsFalse(isSaved);

            Assert.IsNotNull(lastName);
            Assert.AreEqual(firstIndex,0);
            Assert.IsFalse(isDeleted);
          
            Assert.AreEqual(index,100);
            Assert.AreEqual(timeSheet,"");
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            Assert.AreEqual(isCompleted, false);
            #endregion

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
        // ReSharper disable once IdentifierTypo
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
            public void Method1()
            {
            }
        }
        #endregion

        #region XIV. Do declare all member variables at the top of a class, with static variables at the very top. 
        private class Account
        {
            // ReSharper disable once MemberCanBePrivate.Local
            public static string BankName="";

            // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
            private static decimal _reserves;

            private string Number { get; }
            private DateTime DateOpened { get; }
            private DateTime DateClosed { get; }
            private decimal Balance { get; }

            #region
            // Constructor
            public Account()
            {
                BankName = "-";
                Assert.AreEqual(BankName,"-");
                _reserves = 0;
                Assert.AreEqual(_reserves,0);
                Number = "1";
                string t = Number;
                Assert.AreEqual(t,"1");

                DateOpened = DateTime.Now;
                DateClosed = DateOpened;
                Assert.AreEqual(DateOpened, DateClosed);

                Balance = 0;
                Assert.AreEqual(Balance,0);

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
        // ReSharper disable once IdentifierTypo
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

        private class Master : IShape, IShapeCollection, IGroupable
        {
        }

        private void ConsumeVariables()
        {
            FunctionII();
            FunctionVI();
            Test();

            Account account = new Account();
            Assert.IsNotNull(account);

            Example example = new Example();
            example.Method1();

            Color color = Color.Green;
            Assert.AreEqual(color, Color.Green);
            color = Color.Red;
            Assert.AreEqual(color, Color.Red);

            Dockings d = Dockings.Bottom | Dockings.None | Dockings.Top;
            Assert.AreNotEqual(d, Dockings.Bottom);

            WrongDirection w = WrongDirection.East;
            Assert.AreEqual(w, WrongDirection.East);
            w = WrongDirection.South;
            Assert.AreEqual(w, WrongDirection.South);
            w = WrongDirection.West;
            Assert.AreEqual(w, WrongDirection.West);
            w = WrongDirection.North;
            Assert.AreEqual(w, WrongDirection.North);

            Direction w2 = Direction.East;
            Assert.AreEqual(w2, Direction.East);
            w2 = Direction.South;
            Assert.AreEqual(w2, Direction.South);
            w2 = Direction.West;
            Assert.AreEqual(w2, Direction.West);
            w2 = Direction.North;
            Assert.AreEqual(w2, Direction.North);

            CoinEnum coin = CoinEnum.Nickel;
            Assert.AreEqual(coin, CoinEnum.Nickel);
            coin = CoinEnum.Penny;
            Assert.AreEqual(coin, CoinEnum.Penny);

            Coin coin2 = Coin.Nickel;
            Assert.AreEqual(coin2, Coin.Nickel);
            coin2 = Coin.Penny;
            Assert.AreEqual(coin2, CoinEnum.Penny);


            Employee e = new Employee(); 
            Assert.IsNotNull(e);

            BusinessLocation b = new BusinessLocation();
            Assert.IsNotNull(b);

            DocumentCollection dc = new DocumentCollection();
            Assert.IsNotNull(dc);

            Master m = new Master();
            Assert.IsNotNull(m);

            string s = ShippingType;
            Assert.AreEqual(s, ShippingType);
            s = SHIPPINGTYPE;
            Assert.AreEqual(s, SHIPPINGTYPE);

            this.ClientAppointment = DateTime.Now;
            this.TimeLeft = TimeSpan.Zero;

            this.client_Appointment = DateTime.Now;
            this.time_Left = TimeSpan.Zero;

            if ((counter == 0) || (iCounter == 0))
                return;

            if ((name == "") || (strName == ""))
                return;

            if (_registrationDate.CompareTo(DateTime.Now) == 0)
                return;

            _registrationDate = DateTime.Now;
            Assert.IsNotNull(_registrationDate);

        }
        #endregion

        #region Test
        public static void ShowNamingConventions()
        {
            if (DateTime.Now < new DateTime(1970, 1, 1))
            {
                NamingConvention namingConvention = new NamingConvention();
                namingConvention.ConsumeVariables();
            }
        }
        #endregion
    }
}
