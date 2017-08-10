using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using Microsoft.Build.Framework;
using NUnit.Framework;

namespace LoggerPack.UnitTests
{
	[TestFixture()]
	public class LoggerTest : IEventSource
	{
		#region Private Constants and Fields

		private const string TEST_PARAMETERS = "showSummary=false;append=false;logfile=TestXmlLogger.xml";
		private const string FILE_NAME = "TestXmlLogger.xml";

		private XmlLogger _logger;

		#endregion

		#region IEventSource Members

		public event AnyEventHandler AnyEventRaised;

		public event BuildFinishedEventHandler BuildFinished;

		public event BuildStartedEventHandler BuildStarted;

		public event CustomBuildEventHandler CustomEventRaised;

		public event BuildErrorEventHandler ErrorRaised;

		public event BuildMessageEventHandler MessageRaised;

		public event ProjectFinishedEventHandler ProjectFinished;

		public event ProjectStartedEventHandler ProjectStarted;

		public event BuildStatusEventHandler StatusEventRaised;

		public event TargetFinishedEventHandler TargetFinished;

		public event TargetStartedEventHandler TargetStarted;

		public event TaskFinishedEventHandler TaskFinished;

		public event TaskStartedEventHandler TaskStarted;

		public event BuildWarningEventHandler WarningRaised;

		#endregion

		#region Tests

		[TestFixtureSetUp()]
		public void Prepare()
		{
			_logger = new XmlLogger();
			_logger.Parameters = TEST_PARAMETERS;
			_logger.Verbosity = LoggerVerbosity.Diagnostic;
			_logger.Initialize(this);
			RunBuildEvents();
			_logger.Shutdown();
		}

		[Test()]
		public void TestParameters()
		{
			Assert.AreEqual(false, _logger.ShowSummary);
			Assert.AreEqual(false, _logger.Append);
			Assert.AreEqual(FILE_NAME, _logger.FileName);
		}

		[Test()]
		public void TestOutput()
		{
			Assert.IsTrue(File.Exists(FILE_NAME));
			using (XmlReader reader = XmlTextReader.Create(FILE_NAME))
			{
				ParseXml(reader);
			}
			Assert.IsTrue(false);
		}

		[TestFixtureTearDown()]
		public void CleanUp()
		{
			if (File.Exists(FILE_NAME))
			{
				File.Delete(FILE_NAME);
			}
		}

		#endregion

		#region Implementation

		private void RunBuildEvents()
		{
			const string MESSAGE = "Test";
			const string TARGET = "Rebuild";
			const string PROJECT = "Test.proj";
			const string TASK = "MESSAGE";

 			if (BuildStarted != null)
			{
				BuildStarted(this, new BuildStartedEventArgs(MESSAGE, null));
			}

 			if (ProjectStarted != null)
			{
				ProjectStarted(this, new ProjectStartedEventArgs(MESSAGE, null,
					PROJECT, TARGET, null, null));
			}

 			if (TargetStarted != null)
			{
				TargetStarted(this, new TargetStartedEventArgs(MESSAGE, null, 
					TARGET, PROJECT, null));
			}

 			if (TaskStarted != null)
			{
				TaskStarted(this, new TaskStartedEventArgs(MESSAGE, null,
					PROJECT, null, TASK));
			}

			if (TaskFinished != null)
			{
				TaskFinished(this, new TaskFinishedEventArgs(MESSAGE, null,
					PROJECT, null, TASK, true));
			}

			if (TargetFinished != null)
			{
				TargetFinished(this, new TargetFinishedEventArgs(MESSAGE, null, 
					TARGET,	PROJECT, null, true));
			}

			if (ProjectFinished != null)
			{
				ProjectFinished(this, new ProjectFinishedEventArgs(MESSAGE, null,
					PROJECT, false)); 
			}

			if (BuildFinished != null)
			{
				BuildFinished(this, new BuildFinishedEventArgs(MESSAGE, null, false));
			}
		}

		private void ParseXml(XmlReader reader)
		{
			reader.ReadStartElement(XmlLogger.LOG_ELEMENT);
			reader.ReadStartElement(XmlLogger.BUILD_STEP);
			reader.ReadStartElement(XmlLogger.PROJECT_STEP);
			reader.ReadStartElement(XmlLogger.TARGET_STEP);
			reader.ReadStartElement(XmlLogger.TASK_STEP);
			reader.ReadEndElement(); //</Target>
			reader.ReadEndElement(); //</Project>
			reader.ReadEndElement(); //</Build>
			reader.ReadEndElement(); //</Log>
		}

		#endregion
	}
}
