using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Microsoft.Build.Framework;

namespace LoggerPack
{
	class TargetStepProcessor : StepProcessorBase
	{
		#region Overrides

		public override void StepStarted(XmlElement parent, BuildEventArgs args)
		{
			base.StepStarted(parent, args);

			TargetStartedEventArgs tsArgs = args as TargetStartedEventArgs;
			if (tsArgs != null)
			{
				AddNameAttribute(tsArgs.TargetName);
				AddMessageAttribute(tsArgs.Message);
			}
		}

		public override void StepFinished(BuildEventArgs args)
		{
			base.StepFinished(args);

			TargetFinishedEventArgs tfArgs = args as TargetFinishedEventArgs;
			if (tfArgs != null)
			{
				AddSucceededAttribute(tfArgs.Succeeded);
			}
		}

		#endregion
	}
}
