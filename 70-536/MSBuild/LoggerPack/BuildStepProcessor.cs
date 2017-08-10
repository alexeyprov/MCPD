using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

using Microsoft.Build.Framework;

namespace LoggerPack
{
	class BuildStepProcessor : StepProcessorBase
	{
		#region Private Constants

		private const string VERBOSITY_ATTRIBUTE = "Verbosity";

		#endregion

		#region Overrides

		public override void StepStarted(XmlElement parent, BuildEventArgs args)
		{
			base.StepStarted(parent, args);
			AddXmlAttribute(VERBOSITY_ATTRIBUTE, Verbosity.ToString());
		}

		public override void StepFinished(BuildEventArgs args)
		{
			base.StepFinished(args);

			BuildFinishedEventArgs bfArgs = args as BuildFinishedEventArgs;
			if (bfArgs != null)
			{
				AddSucceededAttribute(bfArgs.Succeeded);
			}
		}

		#endregion
	}
}
