using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleBufferExample
{
	public partial class Example : Form
	{
		public Example()
		{
			InitializeComponent();

            PaintMethodSelector.SelectedIndex = 0;
            TestMethodSelector.SelectedIndex = 0;
		}
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            DoubleBufferControl1.Invalidate();
        }

		private void PaintMethodSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			DoubleBufferControl1.PaintMethod = 
				(DoubleBufferControl.DoubleBufferMethod) PaintMethodSelector.SelectedIndex;
		}

        private void TestMethodSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoubleBufferControl1.GraphicTest =
                (DoubleBufferControl.GraphicTestMethods)TestMethodSelector.SelectedIndex;
        }

		private void RefreshInterval_ValueChanged(object sender, EventArgs e)
		{
			RefreshTimer.Interval = (int) RefreshInterval.Value;
		}

        private void StartBtn_Click(object sender, EventArgs e)
        {
            RefreshTimer.Enabled = true;
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            RefreshTimer.Enabled = false;
        }
	}
}