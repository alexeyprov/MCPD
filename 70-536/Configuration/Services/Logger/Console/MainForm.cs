using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Forms;

using LoggerService.Facade;

namespace Console
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			ConfigureRemoting();
		}

		private void ConfigureRemoting()
		{
			Configuration c = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			RemotingConfiguration.Configure(c.FilePath, false);
		}

		private void btnLog_Click(object sender, EventArgs e)
		{
			Logger l = new Logger();
			l.WriteMessage(txtMessage.Text);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.Save();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
