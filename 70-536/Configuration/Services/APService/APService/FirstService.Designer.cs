namespace APService
{
	partial class FirstService
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
			this.evtLog = new System.Diagnostics.EventLog();
			this.perfCtrMine = new System.Diagnostics.PerformanceCounter();
			((System.ComponentModel.ISupportInitialize)(this.evtLog)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.perfCtrMine)).BeginInit();
			// 
			// evtLog
			// 
			this.evtLog.Log = "AP Services Log";
			this.evtLog.Source = "APFirstService2";
			// 
			// perfCtrMine
			// 
			this.perfCtrMine.CategoryName = "My Counters";
			this.perfCtrMine.CounterName = "Transactions Committed";
			this.perfCtrMine.ReadOnly = false;
			// 
			// FirstService
			// 
			this.AutoLog = false;
			this.ServiceName = "APFirstService";
			((System.ComponentModel.ISupportInitialize)(this.evtLog)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.perfCtrMine)).EndInit();

		}

		#endregion

		private System.Diagnostics.EventLog evtLog;
		private System.Diagnostics.PerformanceCounter perfCtrMine;
	}
}
