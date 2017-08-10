using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LoggerPack
{
	public class XmlLogger : FileLoggerBase
	{
		#region Public Constants

		public const string LOG_ELEMENT = "Log";

		public const string BUILD_STEP = "Build";
		public const string PROJECT_STEP = "Project";
		public const string TARGET_STEP = "Target";
		public const string TASK_STEP = "Task";

		#endregion

		#region Private Fields

		private XmlDocument _xdoc = new XmlDocument();
		private Dictionary<string, IStepProcessor> _stepProcessors = new Dictionary<string,IStepProcessor>();

		#endregion

		#region Overrides

		public override void Initialize(IEventSource eventSource)
		{
			//Debugger.Break();
			base.Initialize(eventSource);

			if (null == eventSource)
			{
				throw new ArgumentNullException("eventSource");
			}

			if (this.Append)
			{
				_xdoc.Load(this.FileName);
			}
			else
			{
				_xdoc.AppendChild(_xdoc.CreateElement(LOG_ELEMENT));
			}

			InitializeStepProcessors();

			eventSource.BuildStarted += new BuildStartedEventHandler(EventSource_BuildStarted);
			eventSource.BuildFinished += new BuildFinishedEventHandler(EventSource_BuildFinished);
			eventSource.ProjectStarted += new ProjectStartedEventHandler(EventSource_ProjectStarted);
			eventSource.ProjectFinished += new ProjectFinishedEventHandler(EventSource_ProjectFinished);
			eventSource.TargetStarted += new TargetStartedEventHandler(EventSource_TargetStarted);
			eventSource.TargetFinished += new TargetFinishedEventHandler(EventSource_TargetFinished);
			eventSource.TaskStarted += new TaskStartedEventHandler(EventSource_TaskStarted);
			eventSource.TaskFinished += new TaskFinishedEventHandler(EventSource_TaskFinished);
		}

		public override void Shutdown()
		{
			base.Shutdown();
			_xdoc.Save(this.FileName);
		}

		#endregion

		#region Event Handlers

		void EventSource_TaskFinished(object sender, TaskFinishedEventArgs e)
		{
			try
			{
				_stepProcessors[TASK_STEP].StepFinished(e);
			}
			catch (Exception ex)
			{
				throw new LoggerException(ex.Message, ex);
			}
		}

		void EventSource_TaskStarted(object sender, TaskStartedEventArgs e)
		{
			try
			{
				_stepProcessors[TASK_STEP].StepStarted(_stepProcessors[TARGET_STEP].Node, e);
			}
			catch (Exception ex)
			{
				throw new LoggerException(ex.Message, ex);
			}
		}

		void EventSource_TargetFinished(object sender, TargetFinishedEventArgs e)
		{
			try
			{
				_stepProcessors[TARGET_STEP].StepFinished(e);
			}
			catch (Exception ex)
			{
				throw new LoggerException(ex.Message, ex);
			}
		}

		void EventSource_TargetStarted(object sender, TargetStartedEventArgs e)
		{
			try
			{
				_stepProcessors[TARGET_STEP].StepStarted(_stepProcessors[PROJECT_STEP].Node, e);
			}
			catch (Exception ex)
			{
				throw new LoggerException(ex.Message, ex);
			}
		}

		void EventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
		{
			try
			{
				_stepProcessors[PROJECT_STEP].StepFinished(e);
			}
			catch (Exception ex)
			{
				throw new LoggerException(ex.Message, ex);
			}
		}

		void EventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
		{
			try
			{
				_stepProcessors[PROJECT_STEP].StepStarted(_stepProcessors[BUILD_STEP].Node, e);
			}
			catch (Exception ex)
			{
				throw new LoggerException(ex.Message, ex);
			}
		}
		
		void EventSource_BuildFinished(object sender, BuildFinishedEventArgs e)
		{
			try
			{
				_stepProcessors[BUILD_STEP].StepFinished(e);
			}
			catch (Exception ex)
			{
				throw new LoggerException(ex.Message, ex);
			}
		}

		void EventSource_BuildStarted(object sender, BuildStartedEventArgs e)
		{
			try
			{
				_stepProcessors[BUILD_STEP].StepStarted(_xdoc.DocumentElement, e);
			}
			catch (Exception ex)
			{
				throw new LoggerException(ex.Message, ex);
			}
		}

		#endregion

		#region Implementation

		private void InitializeStepProcessors()
		{
			_stepProcessors.Clear();
			CreateStepProcessor<BuildStepProcessor>(BUILD_STEP);
			CreateStepProcessor<ProjectStepProcessor>(PROJECT_STEP);
			CreateStepProcessor<TargetStepProcessor>(TARGET_STEP);
			CreateStepProcessor<TaskStepProcessor>(TASK_STEP);
		}

		private void CreateStepProcessor<T>(string stepName) where T : IStepProcessor, new()
		{
			T processor = new T();
			processor.Initialize(_xdoc, stepName, Verbosity);
			_stepProcessors[stepName] = processor;
		}

		#endregion
	}
}
