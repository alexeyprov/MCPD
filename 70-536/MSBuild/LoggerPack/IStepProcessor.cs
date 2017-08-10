using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Microsoft.Build.Framework;

namespace LoggerPack
{
	interface IStepProcessor
	{
		void Initialize(XmlDocument xdoc, string stepName, LoggerVerbosity verbosity);
		XmlElement Node { get; }
		void StepStarted(XmlElement parent, BuildEventArgs args);
		void StepFinished(BuildEventArgs args);
	}
}
