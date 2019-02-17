using System;

namespace Utility
{
	public static class StringExtension
	{
		public static bool ValidForStarWarsReturnMessage(this string str)
		{
			return !string.IsNullOrWhiteSpace(str);
		}
	}
}
