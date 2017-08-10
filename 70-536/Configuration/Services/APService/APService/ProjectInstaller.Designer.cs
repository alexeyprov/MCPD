namespace APService
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
			this._perfCtrMineInstaller = new System.Diagnostics.PerformanceCounterInstaller();
			this._evtLogInstaller = new System.Diagnostics.EventLogInstaller();
			this._processInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this._firstServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// _perfCtrMineInstaller
			// 
			this._perfCtrMineInstaller.CategoryHelp = "AlexeyP\'s test counters.";
			this._perfCtrMineInstaller.CategoryName = "My Counters";
			this._perfCtrMineInstaller.CategoryType = System.Diagnostics.PerformanceCounterCategoryType.SingleInstance;
			this._perfCtrMineInstaller.Counters.AddRange(new System.Diagnostics.CounterCreationData[] {
            new System.Diagnostics.CounterCreationData("Transactions Committed", "Count of successfully commited transactions since service start.", System.Diagnostics.PerformanceCounterType.NumberOfItems32)});
			// 
			// _evtLogInstaller
			// 
			this._evtLogInstaller.CategoryCount = 0;
			this._evtLogInstaller.CategoryResourceFile = null;
			this._evtLogInstaller.Log = "AP Services Log";
			this._evtLogInstaller.MessageResourceFile = null;
			this._evtLogInstaller.ParameterResourceFile = null;
			this._evtLogInstaller.Source = "APFirstService2";
			// 
			// _processInstaller
			// 
			this._processInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this._processInstaller.Password = null;
			this._processInstaller.Username = null;
			// 
			// _firstServiceInstaller
			// 
			this._firstServiceInstaller.Description = "This is my first service. To be deleted/";
			this._firstServiceInstaller.DisplayName = "AlexeyP\'s first .NET service";
			this._firstServiceInstaller.ServiceName = "APFirstService";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this._perfCtrMineInstaller,
            this._evtLogInstaller,
            this._processInstaller,
            this._firstServiceInstaller});

		}

		#endregion

		private System.Diagnostics.PerformanceCounterInstaller _perfCtrMineInstaller;
		private System.Diagnostics.EventLogInstaller _evtLogInstaller;
		private System.ServiceProcess.ServiceProcessInstaller _processInstaller;
		private System.ServiceProcess.ServiceInstaller _firstServiceInstaller;
	}
}