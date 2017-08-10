using System;
using System.Collections.Generic;
using System.Text;

namespace SubShifter
{
	sealed class CmdLineArgs
	{
		public const string USAGE = "SubShifter.exe /in:<infile> /out:<outfile> /shift:<delta>";
		const string IN = "/in";
		const string OUT = "/out";
		const string SHIFT = "/shift";

		string _inFile;
		string _outFile;
		int _shift;

		private CmdLineArgs(string inFile, string outFile, int shift)
		{
			_inFile = inFile;
			_outFile = outFile;
			_shift = shift;
		}

		public string InFile
		{
			get
			{
				return _inFile;
			}
		}

		public string OutFile
		{
			get
			{
				return _outFile;
			}
		}

		public int Shift
		{
			get
			{ 
				return _shift;
			}
		}

		public static CmdLineArgs ParseCmdLine(string[] args)
		{
			if (null == args)
			{
				throw new ArgumentNullException();
			}

			if (args.Length != 3)
			{
				throw new ArgumentException("Invalid number of parameters");
			}

			string infile = null;
			string outfile = null;
			int? shift = null;
			foreach (string arg in args)
			{
				string[] parts = arg.Split(':');
				if (parts.Length != 2)
				{
					throw new ArgumentException(String.Format("Invalid parameter format: {0}"), arg);
				}

				switch (parts[0].ToLowerInvariant())
				{
					case IN:
						infile = parts[1];
						break;
					case OUT:
						outfile = parts[1];
						break;
					case SHIFT:
						try
						{
							shift = Int32.Parse(parts[1]);
						}
						catch (FormatException)
						{
							throw new ArgumentException("Invalid format of parameter /shift");
						}
						break;
					default:
						throw new ArgumentException(String.Format("Unknown parameter: {0}"), parts[0]);
				}
			}

			if (null == outfile || null == infile || !shift.HasValue)
			{
				throw new ArgumentException("Not all required parameters were provided");
			}

			return new CmdLineArgs(infile, outfile, shift.Value);
		}
	}
}
