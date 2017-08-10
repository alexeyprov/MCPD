using Microsoft.VisualStudio.DebuggerVisualizers;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Text;

[assembly:DebuggerVisualizer(typeof(ImageListVisualizer.ImageListDebuggerVisualizer),
								typeof(ImageListVisualizer.ImageListObjectSource),
								Target=typeof(ImageList),
								Description="View images...")]

namespace ImageListVisualizer
{
	public class ImageListDebuggerVisualizer : DialogDebuggerVisualizer
	{
		protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
		{
			ImageListStreamer images = (ImageListStreamer) objectProvider.GetObject();
			using (ImageListForm frm = new ImageListForm(images))
			{
				windowService.ShowDialog(frm);
			}
		}

		public static void TestShowVisualizer(object objectToVisualize)
		{
			VisualizerDevelopmentHost myHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(ImageListDebuggerVisualizer));
			myHost.ShowVisualizer();
		}
	}
}
