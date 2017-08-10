using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace APAgent
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			UpdateStatus();
		}

		#region Status Tracking Code
		private void UpdateStatus()
		{
			slbMain.Text = String.Format("{0} is {1}", 
				ctrlFirstService.ServiceName,
				ctrlFirstService.Status);

			btnRunCustomCmd.Enabled = (ServiceControllerStatus.Running == ctrlFirstService.Status);
			btnStop.Enabled = btnRunCustomCmd.Enabled && ctrlFirstService.CanStop;
			btnPause.Enabled = btnStop.Enabled && ctrlFirstService.CanPauseAndContinue;
			btnStart.Enabled = (ServiceControllerStatus.Stopped == ctrlFirstService.Status);
		}

		private void RunCommand(MethodInvoker del)
		{
			this.Cursor = Cursors.WaitCursor;
			UpdateStatus();
			del.BeginInvoke(OnCommandCompleted, del);
		}

		private void OnCommandCompleted(IAsyncResult ar)
		{
			MethodInvoker del = (MethodInvoker) ar.AsyncState;
			try
			{
				del.EndInvoke(ar);
			}
			finally
			{
				RestoreUI();
			}
		}

		private void RestoreUI()
		{
			if (InvokeRequired)
			{
				BeginInvoke(new MethodInvoker(RestoreUI));
				return;
			}

			this.Cursor = Cursors.Default;
			UpdateStatus();
		}
		#endregion

		private void btnStart_Click(object sender, EventArgs e)
		{
			Debug.Assert(ServiceControllerStatus.Stopped == ctrlFirstService.Status);
			RunCommand(ctrlFirstService.Start);
		}

		private void btnPause_Click(object sender, EventArgs e)
		{
			Debug.Assert(ctrlFirstService.CanPauseAndContinue);
			if ("&Pause" == btnPause.Text)
			{
				DoPause();
			}
			else
			{
				DoContinue();
			}
		}

		private void DoContinue()
		{
			Debug.Assert(ServiceControllerStatus.Paused == ctrlFirstService.Status);
			btnPause.Text = "&Pause";
			RunCommand(ctrlFirstService.Continue);
		}

		private void DoPause()
		{
			Debug.Assert(ServiceControllerStatus.Running == ctrlFirstService.Status);
			btnPause.Text = "C&ontinue";
			RunCommand(ctrlFirstService.Pause);
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			Debug.Assert(ServiceControllerStatus.Running == ctrlFirstService.Status);
			RunCommand(ctrlFirstService.Stop);
		}

		private void btnRunCustomCmd_Click(object sender, EventArgs e)
		{
			Debug.Assert(ServiceControllerStatus.Running == ctrlFirstService.Status);
			int cmd = APService.FirstService.COMMIT_COMMAND;
			if (cmbCustomCmd.SelectedIndex != 0)
			{
				cmd = APService.FirstService.GOODBYE_COMMAND;
			}
			ctrlFirstService.ExecuteCommand(cmd);
		}
	}
}