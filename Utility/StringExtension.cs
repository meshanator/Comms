using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Utility
{
	public static class StringExtension
	{
		static readonly string Prefix = "Hello my name is, ";
		static readonly string Pattern = $"(?<={Prefix}).*$";

		//suboptimal, two regex checks are ultimately needed currently
		public static bool ValidForStarWarsName(this string str)
		{
			var reg = Regex.Match(str, Pattern);
			return reg.Success;
		}

		public static string StarWarsName(this string str)
		{
			var reg = Regex.Match(str, Pattern);
			return reg.Value;
		}
	}
}