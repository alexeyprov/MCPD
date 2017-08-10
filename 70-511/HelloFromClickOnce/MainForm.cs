using System;
using System.Deployment.Application;
using System.Reflection;
using System.Windows.Forms;

using HelperLibrary;

namespace HelloFromClickOnce
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            lblMessage.Text = (new GreetingsComponent()).SayHello();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxForm aboutDialog = new AboutBoxForm(ApplicationDeployment.CurrentDeployment.CurrentVersion);
            aboutDialog.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pluginOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if FULL_TRUST
            lblPluginMessage.Text = PluginOne.MainClass.Info;
#endif
        }

        private void environmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Assembly a = Assembly.GetExecutingAssembly();

            MessageBox.Show("Processor architecture: " + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
        }
    }
}
