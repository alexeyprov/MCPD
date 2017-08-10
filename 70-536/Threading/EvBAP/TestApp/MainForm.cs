using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TestApp
{
    public partial class MainForm : Form
    {
        #region Nested Classes

        private enum TaskSubItems
        {
            Progress = 1,
            LastPrime,
            TaskId,
            Result,
            FirstDivisor
        }

        #endregion

        #region Private Constants

        private const string STATUS_NOT_STARTED = "Not Started";
        private const string STATUS_COMPLETED = "Completed";
        private const string STATUS_CANCELLED = "Cancelled";
        
        #endregion

        #region Private Fields

        private readonly Random _rg; 
        
        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            _rg = new Random();
        }

        #endregion

        #region Event Handlers

        private void calcComponent_CalcPrimeCompleted(object sender, CalcComponent.CalcPrimeCompletedEventArgs e)
        {
            ListViewItem lvi = FindItem(e.UserState);
            string status = lvi.SubItems[(int)TaskSubItems.Progress].Text;
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
                    lvi.SubItems[(int)TaskSubItems.LastPrime].Text = lvi.Text;
                    lvi.SubItems[(int)TaskSubItems.FirstDivisor].Text = e.FirstDivisor.ToString();
                    lvi.SubItems[(int)TaskSubItems.Result].Text = e.IsPrime ? "Prime" : "Composite";
                    lvi.SubItems[(int)TaskSubItems.Progress].Text = STATUS_COMPLETED;
                    break;
            }
        }

        private void calcComponent_ProgressChanged(object sender, CalcComponent.CalcPrimeProgressChangedEventArgs e)
        {
            ListViewItem lvi = FindItem(e.UserState);
            string status = lvi.SubItems[(int)TaskSubItems.Progress].Text;
            switch (status)
            {
                case STATUS_COMPLETED:
                    Debug.Fail("Progress event for completed task with ID = " + lvi.Tag);
                    break;
                default: //NOT_STARTED, CANCELLED, x%
                    lvi.SubItems[(int)TaskSubItems.LastPrime].Text = e.LastPrime.ToString();
                    lvi.SubItems[(int)TaskSubItems.Progress].Text = e.ProgressPercentage + "%";
                    break;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Guid taskId = Guid.NewGuid();
            int n = GenerateRandomNumber();
 
            ListViewItem lvi = new ListViewItem(n.ToString())
            {
                Tag = taskId
            };
            lvi.SubItems.AddRange(
                new[] 
                {
                    STATUS_NOT_STARTED,
                    "1",
                    taskId.ToString(),
                    "---",
                    "---"
                });

            lstTasks.Items.Add(lvi);
            calcComponent.CalculatePrimeAsync(n, taskId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lstTasks.SelectedItems)
            {
                string status = lvi.SubItems[(int)TaskSubItems.Progress].Text;
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
                        lvi.SubItems[(int)TaskSubItems.Progress].Text = STATUS_CANCELLED;
                        break;
                }
            }
        }

        #endregion

        #region Implementation

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

        private int GenerateRandomNumber(int attempts = 100)
        {
            int n = 0;

            for (int i = 0; i < attempts; ++i)
            {
                n = _rg.Next(10, Int32.MaxValue);

                if ((n & 0x01) == 0x01)
                {
                    return n;
                }
            }

            return n;
        }

        #endregion
    }
}