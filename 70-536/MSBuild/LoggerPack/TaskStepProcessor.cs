using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Microsoft.Build.Framework;

namespace LoggerPack
{
	class TaskStepProcessor : StepProcessorBase
	{
		#region Overrides

		public override void StepStarted(XmlElement parent, BuildEventArgs args)
		{
			base.StepStarted(parent, args);

			TaskStartedEventArgs tsArgs = args as TaskStartedEventArgs;
			if (tsArgs != null)
			{
				AddNameAttribute(tsArgs.TaskName);
			}
		}

		#endregion
	}
}
