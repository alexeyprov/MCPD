namespace LoggerService
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.svcProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.svcInstaller = new System.ServiceProcess.ServiceInstaller();
			this.svcDebuggerInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// svcProcessInstaller
			// 
			this.svcProcessInstaller.Password = null;
			this.svcProcessInstaller.Username = null;
			// 
			// svcInstaller
			// 
			this.svcInstaller.Description = "Writes log messages to a specified location";
			this.svcInstaller.DisplayName = "Sample Logging Service";
			this.svcInstaller.ServiceName = "LoggerService";
			// 
			// svcDebuggerInstaller
			// 
			this.svcDebuggerInstaller.Description = "Start this service first to facilitate debugging of the Sample Logging Service";
			this.svcDebuggerInstaller.DisplayName = "Sample Logging Service Debugger Helper";
			this.svcDebuggerInstaller.ServiceName = "LoggerDebuggerHelper";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.svcProcessInstaller,
            this.svcInstaller,
            this.svcDebuggerInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller svcProcessInstaller;
		private System.ServiceProcess.ServiceInstaller svcInstaller;
		private System.ServiceProcess.ServiceInstaller svcDebuggerInstaller;
	}
}