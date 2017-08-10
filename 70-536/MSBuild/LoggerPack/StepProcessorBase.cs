using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

using Microsoft.Build.Framework;

namespace LoggerPack
{
	class StepProcessorBase : IStepProcessor
	{
		#region Private Constants

		private const string STARTED_ATTRIBUTE = "Started";
		private const string FINISHED_ATTRIBUTE = "Finished";
		private const string SUCCEEDED_ATTRIBUTE = "Succeeded";
		private const string NAME_ATTRIBUTE = "Name";
		private const string MESSAGE_ATTRIBUTE = "Message";

		#endregion

		#region Private Fields

		private XmlElement _node;
		private XmlDocument _xdoc;
		private string _stepName;
		private LoggerVerbosity _verbosity;

		#endregion

		#region IBuildStepProcessor Members

		public virtual void StepStarted(XmlElement parent, BuildEventArgs args)
		{
			CreateChildElement(parent, ExtractTimeStamp(args));
		}

		public virtual void StepFinished(BuildEventArgs args)
		{
			AddXmlAttribute(FINISHED_ATTRIBUTE, ExtractTimeStamp(args));
		}

		public void Initialize(XmlDocument xdoc, string stepName, LoggerVerbosity verbosity)
		{
			if (null == xdoc)
			{
				throw new ArgumentNullException("xdoc");
			}
			if (null == stepName)
			{
				throw new ArgumentNullException("stepName");
			}
			if (!Enum.IsDefined(typeof(LoggerVerbosity), verbosity))
			{
				throw new ArgumentNullException("verbosity");
			}

			_xdoc = xdoc;
			_stepName = stepName;
			_verbosity = verbosity;
		}

		public XmlElement Node
		{
			get 
			{
				return _node;
			}
		}

		#endregion

		#region Implementation

		private void CreateChildElement(XmlElement parent, string timestamp)
		{
			_node = _xdoc.CreateElement(_stepName);
			if (parent != null)
			{
				parent.AppendChild(_node);
			}
			AddXmlAttribute(STARTED_ATTRIBUTE, timestamp);
		}

		protected void AddXmlAttribute(string name, string value)
		{
			if (_node != null)
			{
				XmlAttribute attr = _xdoc.CreateAttribute(name);
				attr.Value = value;
				_node.Attributes.Append(attr);
			}
		}

		protected static string ExtractTimeStamp(BuildEventArgs args)
		{
			return args.Timestamp.ToString("r", CultureInfo.InvariantCulture);
		}

		protected void AddSucceededAttribute(bool value)
		{
			AddXmlAttribute(SUCCEEDED_ATTRIBUTE, value.ToString(CultureInfo.InvariantCulture));
		}

		protected void AddNameAttribute(string value)
		{
			AddXmlAttribute(NAME_ATTRIBUTE, value);
		}

		protected void AddMessageAttribute(string value)
		{
			AddXmlAttribute(MESSAGE_ATTRIBUTE, value);
		}

		protected LoggerVerbosity Verbosity
		{
			get
			{
				return _verbosity;
			}
		}

		#endregion
	}
}
