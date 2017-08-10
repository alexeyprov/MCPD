using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlowLayoutPanel
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void wrapContentsCheckBox_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			this.FlowLayoutPanel1.WrapContents =
				this.wrapContentsCheckBox.Checked;
		}

		private void flowTopDownBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			this.FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
		}

		private void flowBottomUpBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			this.FlowLayoutPanel1.FlowDirection = FlowDirection.BottomUp;
		}

		private void flowLeftToRight_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			this.FlowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
		}

		private void flowRightToLeftBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			this.FlowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
		}

		private void cbButtonVisible_CheckedChanged(object sender, EventArgs e)
		{
			Button2.Visible = cbButtonVisible.Checked;
		}
	}
}
