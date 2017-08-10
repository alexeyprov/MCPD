using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

public class HelloTask : Task
{
	private ITaskItem _assembly;
	private string _helloOutput;

	[Required]
	public ITaskItem TheAssembly
	{
		get
		{
			return _assembly;
		}
		set
		{
			_assembly = value;
		}
	}

	[Output]
        public string HelloOutput
	{
		get
		{
			return _helloOutput;
		}
		set
		{
			_helloOutput = value;
		}
	}

	public override bool Execute()
	{
		HelloOutput = "HelloTask: " + TheAssembly;
		return true;
	}
}