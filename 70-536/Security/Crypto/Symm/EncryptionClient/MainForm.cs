using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PwdEncryption;

namespace EncryptionClient
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void btnInputBrowse_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == dlgInputFile.ShowDialog(this))
			{
				txtInputFile.Text = dlgInputFile.FileName;
			}
		}

		private void btnOutputBrowse_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == dlgOutputFile.ShowDialog(this))
			{
				txtOutputFile.Text = dlgOutputFile.FileName;
			}
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			using (PasswordForm frm = new PasswordForm())
			{
				if (DialogResult.OK == frm.ShowDialog(this))
				{
					if (rbEncrypt.Checked)
					{
						CryptoUtility.Encrypt(txtInputFile.Text,
							txtOutputFile.Text,
							frm.Password);
					}
					else
					{
						try
						{
							CryptoUtility.Decrypt(txtInputFile.Text,
								txtOutputFile.Text,
								frm.Password);
						}
						catch (WrongPasswordException wpe)
						{
							MessageBox.Show(this, wpe.Message, "Decryption Error",
								MessageBoxButtons.OK, 
								MessageBoxIcon.Warning);
						}
					}
				}
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OnFileNameChanged(object sender, EventArgs e)
		{
			btnRun.Enabled = (txtInputFile.Text.Length > 0 &&
				txtOutputFile.Text.Length > 0);
		}
	}
}