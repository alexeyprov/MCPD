using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
	public partial class MainForm : Form
	{
		enum TaskSubItems
		{
			Progress = 1,
			LastPrime,
			TaskId,
			Result,
			FirstDivisor
		}
		
		Random _rg;
		const string STATUS_NOT_STARTED = "Not Started";
		const string STATUS_COMPLETED = "Completed";
		const string STATUS_CANCELLED = "Cancelled";
	
		public MainForm()
		{
			InitializeComponent();
			_rg = new Random();
		}

		private void calcComponent_CalcPrimeCompleted(object sender, CalcComponent.CalcPrimeCompletedEventArgs e)
		{
			ListViewItem lvi = FindItem(e.UserState);
			string status = lvi.SubItems[(int) TaskSubItems.Progress].Text;
			switch (status)
			{
			case STATUS_CANCELLED:
				Debug.Assert(e.Cancelled);
				lstTasks.Items.Remove(lvi);					
				break;
			case STATUS_COMPLETED:
				Debug.Fail("Repeated completion event for task with ID = " + lvi.Tag);
				break;
			default: //NOT_STARTED, x%
				lvi.SubItems[(int) TaskSubItems.LastPrime].Text = lvi.Text;
				lvi.SubItems[(int) TaskSubItems.FirstDivisor].Text = e.FirstDivisor.ToString();
				lvi.SubItems[(int) TaskSubItems.Result].Text = e.IsPrime ? "Prime" : "Composite";
				lvi.SubItems[(int) TaskSubItems.Progress].Text = STATUS_COMPLETED;
				break;
			}
		}

		private void calcComponent_ProgressChanged(object sender, CalcComponent.CalcPrimeProgressChangedEventArgs e)
		{
			ListViewItem lvi = FindItem(e.UserState);
			string status = lvi.SubItems[(int) TaskSubItems.Progress].Text;
			switch (status)
			{
			case STATUS_COMPLETED:
				Debug.Fail("Progress event for completed task with ID = " + lvi.Tag);
				break;
			default: //NOT_STARTED, CANCELLED, x%
				lvi.SubItems[(int) TaskSubItems.LastPrime].Text = e.LastPrime.ToString();
				lvi.SubItems[(int) TaskSubItems.Progress].Text = e.ProgressPercentage + "%";
				break;
			}
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			Guid taskId = Guid.NewGuid();
			int n = _rg.Next(10, Int32.MaxValue);
			ListViewItem lvi = new ListViewItem(n.ToString());
			lvi.Tag = taskId;
			lvi.SubItems.Add(STATUS_NOT_STARTED);
			lvi.SubItems.Add("1");
			lvi.SubItems.Add(taskId.ToString());
			lvi.SubItems.Add("---");
			lvi.SubItems.Add("---");
			lstTasks.Items.Add(lvi);
			calcComponent.CalculatePrimeAsync(n, taskId);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in lstTasks.SelectedItems)
			{
				string status = lvi.SubItems[(int) TaskSubItems.Progress].Text;
				switch (status)
				{
					case STATUS_CANCELLED:
						// Cancelling already requested, do nothing
						break;
					case STATUS_COMPLETED:
						// Already completed, safe to remove
						lstTasks.Items.Remove(lvi);
						break;
					default: // NOT_STARTED, x%
						// Request cancelling with component
						calcComponent.CancelAsync(lvi.Tag);
						lvi.SubItems[(int) TaskSubItems.Progress].Text = STATUS_CANCELLED;
						break;
				}
			}
		}
		
		private ListViewItem FindItem(object taskId)
		{
			if (null == taskId)
			{
				throw new ArgumentNullException("taskId");
			}
			
			foreach (ListViewItem lvi in lstTasks.Items)
			{
				if (taskId.Equals(lvi.Tag))
				{
					return lvi;
				}
			}
			
			throw new ArgumentException("No task found with this id", "taskId");
		}
	}
}