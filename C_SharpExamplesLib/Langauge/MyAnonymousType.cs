﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	public class MyAnonymousType
	{
		public static void AnonymousType_Test()
		{
			var dog = new { Name = MyEnum.Dog.Cerberus, Größe = "50cm" };
			var dogType = dog.GetType();
			Console.WriteLine(dogType.ToString());
			Assert.IsTrue(dog.Name == MyEnum.Dog.Cerberus);

			var dog2 = new { Name = MyEnum.Dog.KomissarRex, Größe = "40cm" };
			var dog2Type = dog2.GetType();

			Assert.IsTrue(dogType == dog2Type);
		}
	}
}
