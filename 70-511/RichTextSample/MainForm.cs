using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RichTextSample
{
	public partial class MainForm : Form
	{
		#region Private Constants

		private const uint WM_USER = 0x0400;
		private const uint EM_AUTOURLDETECT = WM_USER + 91;

		#endregion

		#region External Functions

		[DllImport("user32.dll")]
		private static extern int SendMessage(int hWnd, uint Msg, long wParam, long lParam);

		#endregion

		#region Construction

		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Event Handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			txtGreeting.Text = "Welcome to my web site!" +
				Environment.NewLine +
				@"\\angel.onecallmedical.com\phoenix_project";
			SendMessage(txtGreeting.Handle.ToInt32(), EM_AUTOURLDETECT, 0, 0);
		}

		private void txtGreeting_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}

		#endregion
	}
}
