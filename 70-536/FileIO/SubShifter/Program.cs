using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SubShifter
{
	class Program
	{
		static void Main(string[] args)
		{
			CmdLineArgs ca;
			try
			{
				ca = CmdLineArgs.ParseCmdLine(args);
			}
			catch (ArgumentException ae)
			{
				Console.WriteLine("{0}{1}{2}",
					ae.Message, Environment.NewLine, CmdLineArgs.USAGE);
				return;
			}

			ProcessFileData(ca);
		}

		private static void ProcessFileData(CmdLineArgs ca)
		{
			using (StreamReader reader = new StreamReader(ca.InFile, Encoding.Default))
			{
				using (StreamWriter writer = new StreamWriter(ca.OutFile, false, Encoding.Default))
				{
					Regex r = new Regex(@"^\{(?<start>\d+)\}\{(?<end>\d+)\}");
					string s;
					while ((s = reader.ReadLine()) != null)
					{
						Match m = r.Match(s);
						if (m != Match.Empty)
						{
							int start = Int32.Parse(m.Groups["start"].Value) + ca.Shift;
							int end = Int32.Parse(m.Groups["end"].Value) + ca.Shift;
							Debug.Assert(end >= start);

							string text = s.Substring(m.Length);

							writer.WriteLine("{{{0}}}{{{1}}}{2}",
								start, end, text);
						}
						else
						{
							writer.WriteLine(s);
						}
					}
				}
			}
		}
	}
}
