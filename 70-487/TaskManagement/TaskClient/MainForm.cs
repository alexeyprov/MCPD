#define WINDOWS_AUTH

using System;
using System.Diagnostics;
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

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string owner = cmbUserName.Text;

                if (!string.IsNullOrEmpty(owner))
                {
                    TaskServiceClient taskService = CreateServiceClient();
                    using (taskService.AsDisposable())
                    {
                        gvTasks.DataSource = await taskService.GetTasksByOwnerAsync(owner);
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

        private async void btnAddTask_Click(object sender, EventArgs e)
        {
            using (TaskForm form = new TaskForm())
            {
                if (DialogResult.OK == form.ShowDialog(this))
                {
                    try
                    {
                        TaskServiceClient taskService = CreateServiceClient();
                        using (taskService.AsDisposable())
                        {
                            await taskService.AddTaskAsync(form.GetTask(cmbUserName.Text));
                        }
                    }
                    catch (FaultException<FaultInfo> ex)
                    {
                        MessageBox.Show(this, ex.Detail.ErrorMessage);
                    }
                }
            }
        }

        private async void btnCompleteTask_Click(object sender, EventArgs e)
        {
            TaskServiceClient taskService = CreateServiceClient();
            using (taskService.AsDisposable())
            {
                await taskService.MarkTaskCompletedAsync(SelectedTaskNumber);
            }
        }

        private async void btnReassignTask_Click(object sender, EventArgs e)
        {
            string newOwner;

            using (InputBoxForm inputBox = new InputBoxForm
                {
                    Caption = "Reassign to:"
                })
            {
                if (inputBox.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                newOwner = inputBox.Input;
            }

            try
            {
                TaskServiceClient taskService = CreateServiceClient();
                using (taskService.AsDisposable())
                {
                    AssignTaskResponse r = await taskService.AssignTaskAsync(
                        new AssignTaskRequest
                        {
                            taskNumber = SelectedTaskNumber,
                            owner = newOwner
                        });

                    Debug.Assert(r.previousOwner != newOwner);
                }
            }
            catch (FaultException<FaultInfo> ex)
            {
                MessageBox.Show(this, ex.Detail.ErrorMessage);
            }
        }

        private int SelectedTaskNumber
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = gvTasks.SelectedRows;

                if (selectedRows.Count == 0)
                {
                    return 0;
                }

                return ((Task)selectedRows[0].DataBoundItem).TaskNumber;
            }
        }

        private static TaskServiceClient CreateServiceClient()
        {
            TaskServiceClient client;

#if BASIC_AUTH
            client = new TaskServiceClient("BasicHttpBinding_TaskService_BasicAuth");

            client.ClientCredentials.UserName.UserName = "TODO";
            client.ClientCredentials.UserName.Password = "TODO"; 
#elif WINDOWS_AUTH
            client = new TaskServiceClient("BasicHttpBinding_TaskService_WindowsAuth");
#else
            client = new TaskServiceClient();
#endif
            return client;
        }
    }
}
