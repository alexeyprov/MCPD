using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SecureModule
{
	public partial class ChildForm : Form
	{
		public ChildForm()
		{
			InitializeComponent();
		}

		public void RunTest(MethodInvoker callback)
		{
			_callback = callback;
			ShowDialog();
		}

	// Event Handlers
		private void btnCallback_Click(object sender, EventArgs e)
		{
			if (_callback != null)
			{
				_callback();
			}
		}

	// Data Members
		MethodInvoker _callback;
	}
}