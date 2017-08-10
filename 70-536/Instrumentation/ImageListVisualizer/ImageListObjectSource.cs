using Microsoft.VisualStudio.DebuggerVisualizers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Text;

namespace ImageListVisualizer
{
	public class ImageListObjectSource : VisualizerObjectSource
	{
		public override void GetData(object target, Stream outgoingData)
		{
			ImageList iml = (ImageList)target;

			BinaryFormatter fmt = new BinaryFormatter();
			fmt.Serialize(outgoingData, iml.ImageStream);
		}
	}
}
