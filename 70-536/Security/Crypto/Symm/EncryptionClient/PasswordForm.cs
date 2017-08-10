using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EncryptionClient
{
	public partial class PasswordForm : Form
	{
		public PasswordForm()
		{
			InitializeComponent();
		}

		public string Password
		{
			get
			{
				return txtPassword.Text;
			}
		}

		private void txtPassword_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = (txtPassword.Text.Length > 0);
		}
	}
}