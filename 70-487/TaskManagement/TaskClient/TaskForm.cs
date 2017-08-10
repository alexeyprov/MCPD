using System.Windows.Forms;

using TaskClient.TaskWcfService;

namespace TaskClient
{
	public partial class TaskForm : Form
	{
		public TaskForm()
		{
			InitializeComponent();
		}

		public Task GetTask(string owner)
		{
			return new Task()
			{
				Status = TaskStatus.Assigned,
				AssignedTo = owner,
				Description = txtDescription.Text,
				DueDate = dtDueDate.Value
			};
		}
	}
}
