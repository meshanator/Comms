using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Utility
{
	public static class StringExtension
	{
		//static readonly string Prefix = "Hello my name is, ";
		//static readonly string Pattern = $"(?<={Prefix}).*$";

		//suboptimal, two regex checks are ultimately needed currently
		public static bool ValidForStarWarsName(this string str)
		{
			var Prefix1 = "Hello my name is, ";
			var Pattern1 = $"(?<={Prefix1}).*$";
			var reg = Regex.Match(Pattern1, str);
			return reg.Success;
		}

		public static string StarWarsName(this string str)
		{
			var Prefix1 = "Hello my name is, ";
			var Pattern1 = $"(?<={Prefix1}).*$";
			var reg = Regex.Match(Pattern1, str);
			return reg.Value;
		}
	}
}
