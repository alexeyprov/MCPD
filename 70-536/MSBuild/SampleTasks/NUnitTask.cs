using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.Win32;

public class NUnitTask : ToolTask
{
	#region Private Constants

	private const string XML_OUTPUT_FILE = "NUnitTask.xml";
	private const string PROGRAM_FILE = "nunit-console.exe";
	private const string GUI_PROGRAM_REGEX = "(.+)nunit\\.exe";
	private const string GUI_PROGRAM_REGISTRY_KEY = @"NUnitTestProject\shell\open\command";

	private const string RESULTS_NODE = "test-results";
	private const string TOTAL_ATTRIBUTE = "total";
	private const string FAILED_ATTRIBUTE = "failures";
	private const string IGNORED_ATTRIBUTE = "not-run";

	#endregion

	#region Private Fields

	private ITaskItem[] _assemblies;
	private ITaskItem _logPath;
	private bool _testInSameThread;
	private bool _continueOnError;

	private int _executedTests;
	private int _ignoredTests;
	private int _failedTests;

	#endregion

	#region Input Properties

	[Required]
	public ITaskItem[] Assemblies
	{
		get
		{
			return _assemblies;
		}
		set
		{
			_assemblies = value;
		}
	}

	[Required]
	public ITaskItem LogPath
	{
		get
		{
			return _logPath;
		}
		set
		{
			_logPath = value;
		}
	}

	public bool ContinueIfError
	{
		get
		{
			return _continueOnError;
		}
		set
		{
			_continueOnError = value;
		}
	}

	public bool TestInSameThread
	{
		get
		{
			return _testInSameThread; 
		}
		set
		{
			_testInSameThread = value; 
		}
	}


	#endregion

	#region Output Properties

	[Output]
	public int NumExecutedTests
	{
		get
		{
			return _executedTests;
		}
	}

	[Output]
	public int NumIgnoredTests
	{
		get
		{
			return _ignoredTests;
		}
	}

	[Output]
	public int NumFailedTests
	{
		get
		{
			return _failedTests;
		}
	}

	#endregion

	#region Overrides

	protected override string ToolName
	{
		get
		{
			return PROGRAM_FILE;
		}
	}

	protected override void LogEventsFromTextOutput(string singleLine, MessageImportance messageImportance)
	{
		File.AppendAllText(LogPath.ItemSpec, singleLine + Environment.NewLine);
	}

	protected override string GenerateFullPathToTool()
	{
		return Path.Combine(ProgramPath, ToolName);
	}

	protected override string GenerateCommandLineCommands()
	{
		CommandLineBuilder builder = new CommandLineBuilder();
		builder.AppendSwitch("/nologo");
		builder.AppendSwitch("/nodots");
		if (TestInSameThread)
		{
			builder.AppendSwitch("/nothread");
		}
		builder.AppendFileNamesIfNotNull(Assemblies, " ");
		builder.AppendSwitchIfNotNull("/xml=", XmlLogPath);
		//builder.AppendSwitchIfNotNull("/err=", LogPath);
		return builder.ToString();
	}

	public override bool Execute()
	{
		Debugger.Break();

		DeleteFile(LogPath.ItemSpec);
		bool retval = base.Execute();
		ParseResultsAndCleanUp();
		return retval;
	}

	protected override bool HandleTaskExecutionErrors()
	{
		if (1 == ExitCode && ContinueIfError)
		{
			// Nothing serious. Just failed tests
			// No need to raise MSBuild error
			return true;
		}
		return base.HandleTaskExecutionErrors();
	}

	#endregion

	#region Implementation

	private string XmlLogPath
	{
		get
		{
			return Path.Combine(Path.GetTempPath(),
				XML_OUTPUT_FILE);
		}
	}

	private string ProgramPath
	{
		get
		{
			using (RegistryKey buildKey = Registry.ClassesRoot.OpenSubKey(GUI_PROGRAM_REGISTRY_KEY))
			{
				Regex nunitRegex = new Regex(GUI_PROGRAM_REGEX, RegexOptions.IgnoreCase);
				Match pathMatch = nunitRegex.Match(buildKey.GetValue(null).ToString());
				return pathMatch.Groups[1].Value.Replace("\"", "");
			}
		}
	}

	private void ParseResultsAndCleanUp()
	{
		try
		{
			if (File.Exists(XmlLogPath))
			{
				using (XmlReader reader = XmlTextReader.Create(XmlLogPath))
				{
					if (reader.ReadToFollowing(RESULTS_NODE))
					{
						if (reader.HasAttributes && reader.MoveToFirstAttribute())
						{
							do
							{
								ParseResultAttribute(reader);
							}
							while (reader.MoveToNextAttribute());
						}
					}
				}
			}
		}
		finally
		{
			DeleteFile(XmlLogPath);
		}
	}

	private void ParseResultAttribute(XmlReader reader)
	{
		switch (reader.Name)
		{
			case TOTAL_ATTRIBUTE:
				_executedTests = Int32.Parse(reader.Value);
				break;
			case FAILED_ATTRIBUTE:
				_failedTests = Int32.Parse(reader.Value);
				break;
			case IGNORED_ATTRIBUTE:
				_ignoredTests = Int32.Parse(reader.Value);
				break;
			default:
				break;
		}
	}

	private void DeleteFile(string filePath)
	{
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
	}

	#endregion
}