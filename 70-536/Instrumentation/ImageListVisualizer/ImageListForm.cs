using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageListVisualizer
{
	public partial class ImageListForm : Form
	{
		public ImageListForm(ImageListStreamer images)
		{
			InitializeComponent();

			imlPictures.Images.Clear();
			imlPictures.ImageStream = images;
		}

		private void ImageListForm_Load(object sender, EventArgs e)
		{
			ImageList.ImageCollection images = imlPictures.Images;
			for (int i = 0, cnt = images.Count; i < cnt; i++)
			{
				lvwData.Items.Add(String.Format("{0} - {1}", i, images.Keys[i]), i);
			}
		}
	}
}
