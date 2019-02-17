using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility;

namespace Tests
{
	[TestClass]
	public class StarWarsValidationTests
	{
		[TestMethod]
		public void SW_Validation_Proper_Message()
		{
			//arrange
			var name = "Luke";

			//act
			var valid = name.ValidForStarWarsReturnMessage();

			//assert
			Assert.IsTrue(valid);
		}

		[TestMethod]
		public void SW_Validation_Blank_Message()
		{
			//arrange
			var name = string.Empty;

			//act
			var valid = name.ValidForStarWarsReturnMessage();

			//assert
			Assert.IsFalse(valid);
		}

		[TestMethod]
		public void SW_Validation_Whitespace_Message()
		{
			//arrange
			var name = "          ";

			//act
			var valid = name.ValidForStarWarsReturnMessage();

			//assert
			Assert.IsFalse(valid);
		}
	}
}
