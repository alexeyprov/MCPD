using System;
using System.Text;

namespace ControlExtensions.Helpers
{
	public class ScriptBuilder
	{
		private const string PREFIX = "<script type=\"text/javascript\">\n<!--\n";
		private const string SUFFIX = "//-->\n</script>\n";
		private const char FILL_CHARACTER = '\t';

		private readonly StringBuilder _builder;
		private int _fillCount;
		private string _filling;

		public ScriptBuilder()
		{
			_builder = new StringBuilder(PREFIX);
			Indent();
		}

		public void Indent()
		{
			_fillCount++;
			UpdateFilling();
		}

		public void Outdent()
		{
			_fillCount--;
			UpdateFilling();
		}

		public void Append(string s)
		{
			_builder.Append(_filling);
			_builder.Append(s);
		}

		public void AppendLine(string s)
		{
			_builder.Append(_filling);
			_builder.AppendLine(s);
		}

		public void AppendFormat(string format, params object[] args)
		{
			_builder.Append(_filling);
			_builder.AppendFormat(format, args);
		}

		public override string ToString()
		{
			Outdent();
			_builder.Append(SUFFIX);

			return _builder.ToString();
		}

		private void UpdateFilling()
		{
			_filling = new String(FILL_CHARACTER, _fillCount);
		}
	}
}
