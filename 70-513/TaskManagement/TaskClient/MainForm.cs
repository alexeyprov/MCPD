using System;
using System.ServiceModel;
using System.Windows.Forms;

using TaskClient.TaskWcfService;

namespace TaskClient
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			try
			{
				string owner = cmbUserName.Text;

				if (!String.IsNullOrEmpty(owner))
				{
					using (TaskServiceClient taskService = new TaskServiceClient())
					{
						gvTasks.DataSource = taskService.GetTasksByOwner(owner);
					}

					if (!cmbUserName.Items.Contains(owner))
					{
						cmbUserName.Items.Add(owner);
					}
				}
			}
			catch (FaultException<FaultInfo> ex)
			{
				MessageBox.Show(this, ex.Detail.ErrorMessage);
			}
		}

		private void btnAddTask_Click(object sender, EventArgs e)
		{
			using (TaskForm form = new TaskForm())
			{
				if (DialogResult.OK == form.ShowDialog(this))
				{
					try
					{
						using (TaskServiceClient taskService = new TaskServiceClient())
						{
							taskService.AddTask(form.GetTask(cmbUserName.Text));
						}
					}
					catch (FaultException<FaultInfo> ex)
					{
						MessageBox.Show(this, ex.Detail.ErrorMessage);
					}
				}
			}
		}

		private void btnCompleteTask_Click(object sender, EventArgs e)
		{
			try
			{
				using (TaskServiceClient taskService = new TaskServiceClient())
				{
					taskService.MarkTaskCompleted(((Task) gvTasks.SelectedRows[0].DataBoundItem).TaskNumber);
				}
			}
			catch (FaultException<FaultInfo> ex)
			{
				MessageBox.Show(this, ex.Detail.ErrorMessage);
			}
		}
	}
}
