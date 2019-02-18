using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility;

namespace Tests
{
	[TestClass]
	public class StarWarsNameValidationTests
	{
		[TestMethod]
		public void SW_Validation_Valid_Message()
		{
			//Arrange
			var name = "Hello my name is, Leia";

			//act
			var valid = name.ValidForStarWarsName();
			var starWarsName = name.StarWarsName();

			//assert
			Assert.IsTrue(valid);
			Trace.WriteLine(starWarsName);
		}

		[TestMethod]
		public void SW_Validation_Invalid_Message()
		{
			//Arrange
			var name = "Howdy my name is, Leia";

			//act
			var valid = name.ValidForStarWarsName();

			//assert
			Assert.IsFalse(valid);
		}

		/*		[TestMethod]
				public void SW_Validation_Valid_Message()
				{
					//Arrange
					var prefix = "Hello my name is, ";
					var pattern = $"(?<={prefix}).*$";
					var name = "Hello my name is, Leia";

					//act
					var reg = Regex.Match(name, pattern);

					//assert
					Assert.IsTrue(reg.Success);
					Trace.WriteLine(reg.Value);
				}

				[TestMethod]
				public void SW_Validation_Invalid_Message()
				{
					//Arrange
					var prefix = "Hello my name is, ";
					var pattern = $"(?<={prefix}).*$";
					var name = "Howdy my name is, Leia";

					//act
					var reg = Regex.Match(name, pattern);

					//assert
					Assert.IsFalse(reg.Success);
				}*/
	}
}
