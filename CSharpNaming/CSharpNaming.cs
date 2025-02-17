using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNaming
{
    #region supporter classes
    internal class UserGroup;
    internal class Assignment;
    internal class CustomerId;

    internal class HtmlHelper;

    internal class FtpTransfer;

    // ReSharper disable once InconsistentNaming
    internal class UIControl;

    #endregion

    // Merge Test : Change in branch Merge Test (2)

    // Merge Test : Change in branch Master

    /// <summary>
    /// Naming conventions 
    /// https://www.dofactory.com/reference/csharp-coding-standards
    /// </summary>

    // ReSharper disable once EmptyRegion
    #region I. PascalCasing for classes : public class NamingConvention
    #endregion
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class NamingConvention
    {
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
            Assert.AreEqual(0, localVariable);
            Assert.AreEqual(0, iMaximum);
            ClearStack();
            #endregion
        }

        #region III. Do use camelCasing for local variables and method arguments: int counter=0
        //      do not use Hungarian notation or any other type identification in identifiers
        // Correct
        readonly int counter = 0;
        readonly string name = "";

        // Avoid
        readonly int iCounter = 0;
        readonly string strName = "";
        #endregion

        #region IV. Do not use Screaming Caps for constants or readonly variables:  const string ShippingType = "DropShip";   Avoid: const string SHIPPINGTYPE = "DropShip";
        // Correct : PascalCasing
        private const string ShippingType = "DropShip";

        // Avoid : Screaming caps
        private const string SHIPPINGTYPE = "DropShip";
        #endregion

        [SuppressMessage("ReSharper", "JoinDeclarationAndInitializer")]
        private void FunctionV()
        {
            #region V. Avoid using Abbreviations: UserGroup userGroup;     Avoid UserGroup usrGrp;

            //    Exceptions: abbreviations commonly used as names, such as ID, Xml, Ftp, Uri

            // Correct
            UserGroup userGroup;
            Assignment employeeAssignment;

            // Avoid
            UserGroup usrGrp;
            Assignment empAssignment;
            // Exceptions: Id
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

        [SuppressMessage("ReSharper", "JoinDeclarationAndInitializer")]
        private void FunctionVI()
        {
            // here for the type name
            HtmlHelper htmlHelper;
            FtpTransfer ftpTransfer;
            UIControl uiControl;

            #region
            htmlHelper = new HtmlHelper();
            Assert.IsNotNull(htmlHelper);
            ftpTransfer = new FtpTransfer();
            Assert.IsNotNull(ftpTransfer);
            uiControl = new UIControl();
            Assert.IsNotNull(uiControl);

            #endregion

        }

        #endregion

        #region VII. Do not use Underscores in identifiers: public DateTime clientAppointment;    Avoid: client_Appointment;
        //      Exception: you can prefix private static variables with an underscore.
        // Correct
        private DateTime ClientAppointment;
        private TimeSpan TimeLeft;

        // Avoid
        private DateTime client_Appointment;
        private TimeSpan time_Left;

        // Exception : private static
        private static DateTime _registrationDate = DateTime.Now;
        #endregion

        #region VIII. Do use predefined type names instead of system type names like Int16, Single, UInt64, etc
        // Correct
        readonly string firstName = "";
        readonly int lastIndex = 0;
        readonly bool isSaved = false;

        // Avoid
        readonly String lastName = "";
        readonly Int32 firstIndex = 0;
        readonly Boolean isDeleted = false;
        #endregion

        #region IX. Do use implicit type var for local variable declarations: var stream = File.Create("");
        // Exception: primitive types (int, string, double, ...) use predefined names.
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
            Assert.AreEqual(0, lastIndex);

            Assert.IsNotNull(stream);
            Assert.IsNotNull(customers);
            Assert.IsFalse(isSaved);

            Assert.IsNotNull(lastName);
            Assert.AreEqual(0, firstIndex);
            Assert.IsFalse(isDeleted);
          
            Assert.AreEqual(100,index);
            Assert.AreEqual("", timeSheet);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            Assert.AreEqual(isCompleted, false);
            #endregion

        }
        #endregion

        #region X. Do use noun or noun phrases to name a class: public class Employee, public class DocumentCollection

        private class Employee;

        private class BusinessLocation;

        private class DocumentCollection;
        #endregion

        #region XI. Do prefix interfaces with the letter I: public interface IShape 
        //     Interface names are noun (phrases) or adjectives.
        private interface IShape;

        private interface IShapeCollection;
        // ReSharper disable once IdentifierTypo
        private interface IGroupable;
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
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        [SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
        private class Account
        {
            public static string BankName="";
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
                Assert.AreEqual("1", t);

                DateOpened = DateTime.Now;
                DateClosed = DateOpened;
                Assert.AreEqual(DateOpened, DateClosed);

                Balance = 0;
                Assert.AreEqual(0, Balance);

            }
            #endregion
        }
        #endregion

        #region XV. Do use singular names for enums.Exception: bit field enums: public enum Color { Red, ... 
        // Correct
        private enum Color
        {
            Red,
            Green,
        }

        // Exception
        [Flags]
        // ReSharper disable once IdentifierTypo
        private enum Dockings
        {
            None = 0,
            Top = 1,
            Bottom = 4,
        }
        #endregion

        #region XVI. Do not explicitly specify a type of an enum or values of enums
        //      (except bit fields)
        
        // Don't
        private enum WrongDirection : long
        {
            North = 1,
            East = 2,
            South = 3,
            West = 4
        }

        // Correct
        private enum Direction
        {
            North,
            East,
            South,
            West
        }
        #endregion

        #region XVII. Do notsuffix enum names with Enum : enum Coin   Avoid: enum CoinEnum
        // Don't
        private enum CoinEnum
        {
            Penny,
            Nickel
        }

        // Correct
        private enum Coin
        {
            Penny,
            Nickel,
        }
        #endregion

        #region suppress warnings

        private class Master : IShape, IShapeCollection, IGroupable;

        private void ConsumeVariables()
        {
            Assert.IsNotNull(ClientAppointment);
            Assert.IsNotNull(TimeLeft);
            Assert.IsNotNull(client_Appointment);
            Assert.IsNotNull(time_Left);
            
            FunctionII();
            FunctionV();
            FunctionVI();
            Test();

            Account account = new Account();
            Assert.IsNotNull(account);

            Example example = new Example();
            example.Method1();

            Color color = Color.Green;
            Assert.AreEqual(Color.Green, color);
            color = Color.Red;
            Assert.AreEqual(Color.Red, color);

            Dockings d = Dockings.Bottom | Dockings.None | Dockings.Top;
            Assert.AreNotEqual(d, Dockings.Bottom);

            WrongDirection w = WrongDirection.East;
            Assert.AreEqual(WrongDirection.East, w);
            w = WrongDirection.South;
            Assert.AreEqual(WrongDirection.South, w);
            w = WrongDirection.West;
            Assert.AreEqual(WrongDirection.West, w);
            w = WrongDirection.North;
            Assert.AreEqual(WrongDirection.North, w);

            Direction w2 = Direction.East;
            Assert.AreEqual(Direction.East, w2);
            w2 = Direction.South;
            Assert.AreEqual(Direction.South, w2);
            w2 = Direction.West;
            Assert.AreEqual(Direction.West, w2);
            w2 = Direction.North;
            Assert.AreEqual(Direction.North, w2);

            CoinEnum coin = CoinEnum.Nickel;
            Assert.AreEqual(CoinEnum.Nickel, coin);
            coin = CoinEnum.Penny;
            Assert.AreEqual(CoinEnum.Penny, coin);

            Coin coin2 = Coin.Nickel;
            Assert.AreEqual(Coin.Nickel, coin2);
            coin2 = Coin.Penny;
            Assert.AreEqual((Coin)CoinEnum.Penny, coin2);


            Employee e = new Employee(); 
            Assert.IsNotNull(e);

            BusinessLocation b = new BusinessLocation();
            Assert.IsNotNull(b);

            DocumentCollection dc = new DocumentCollection();
            Assert.IsNotNull(dc);

            Master m = new Master();
            Assert.IsNotNull(m);

            string s = ShippingType;
            Assert.AreEqual(ShippingType, s);
            s = SHIPPINGTYPE;
            Assert.AreEqual(SHIPPINGTYPE, s);

            ClientAppointment = DateTime.Now;
            TimeLeft = TimeSpan.Zero;

            client_Appointment = DateTime.Now;
            time_Left = TimeSpan.Zero;

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
