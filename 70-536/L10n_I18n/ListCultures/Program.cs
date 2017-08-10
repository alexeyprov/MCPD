using System;
using System.Globalization;
using System.Linq;

internal static class Program
{
	private static void Main()
	{
		Console.WriteLine("NAME   ISO ISO WIN DISPLAY_NAME                  ENGLISH_NAME");

		foreach (CultureInfo ci in CultureInfo
			.GetCultures(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures)
			.OrderBy(c => c.Name))
		{
			Console.WriteLine(
				"{0,-7}{1,-4}{2,-4}{3,-4}{4,-30}{5,-30}",
				ci.Name,
				ci.TwoLetterISOLanguageName,
				ci.ThreeLetterISOLanguageName,
				ci.ThreeLetterWindowsLanguageName,
				ci.DisplayName,
				ci.EnglishName);
		}
	}
}