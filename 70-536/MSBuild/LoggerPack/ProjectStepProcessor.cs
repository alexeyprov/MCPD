using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Microsoft.Build.Framework;

namespace LoggerPack
{
	class ProjectStepProcessor : StepProcessorBase
	{
		#region Overrides

		public override void StepStarted(XmlElement parent, BuildEventArgs args)
		{
			base.StepStarted(parent, args);

			ProjectStartedEventArgs psArgs = args as ProjectStartedEventArgs;
			if (psArgs != null)
			{
				AddNameAttribute(psArgs.ProjectFile);
				AddMessageAttribute(psArgs.Message);
			}
		}

		#endregion
	}
}
