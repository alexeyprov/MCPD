namespace APAgent
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.sbMain = new System.Windows.Forms.StatusStrip();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnPause = new System.Windows.Forms.Button();
			this.ctrlFirstService = new System.ServiceProcess.ServiceController();
			this.lblCustomCmd = new System.Windows.Forms.Label();
			this.cmbCustomCmd = new System.Windows.Forms.ComboBox();
			this.btnRunCustomCmd = new System.Windows.Forms.Button();
			this.slbMain = new System.Windows.Forms.ToolStripStatusLabel();
			this.sbMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// sbMain
			// 
			this.sbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slbMain});
			this.sbMain.Location = new System.Drawing.Point(0, 93);
			this.sbMain.Name = "sbMain";
			this.sbMain.Size = new System.Drawing.Size(312, 22);
			this.sbMain.TabIndex = 0;
			this.sbMain.Text = "Ready";
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(23, 20);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(58, 23);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "&Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(233, 20);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(58, 23);
			this.btnStop.TabIndex = 1;
			this.btnStop.Text = "S&top";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnPause
			// 
			this.btnPause.Location = new System.Drawing.Point(129, 20);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(58, 23);
			this.btnPause.TabIndex = 1;
			this.btnPause.Text = "&Pause";
			this.btnPause.UseVisualStyleBackColor = true;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// ctrlFirstService
			// 
			this.ctrlFirstService.ServiceName = "APFirstService";
			// 
			// lblCustomCmd
			// 
			this.lblCustomCmd.AutoSize = true;
			this.lblCustomCmd.Location = new System.Drawing.Point(21, 58);
			this.lblCustomCmd.Name = "lblCustomCmd";
			this.lblCustomCmd.Size = new System.Drawing.Size(116, 13);
			this.lblCustomCmd.TabIndex = 2;
			this.lblCustomCmd.Text = "Run &custom command:";
			// 
			// cmbCustomCmd
			// 
			this.cmbCustomCmd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCustomCmd.FormattingEnabled = true;
			this.cmbCustomCmd.Items.AddRange(new object[] {
            "Hello",
            "Goodbye"});
			this.cmbCustomCmd.Location = new System.Drawing.Point(143, 54);
			this.cmbCustomCmd.Name = "cmbCustomCmd";
			this.cmbCustomCmd.Size = new System.Drawing.Size(84, 21);
			this.cmbCustomCmd.TabIndex = 3;
			// 
			// btnRunCustomCmd
			// 
			this.btnRunCustomCmd.Location = new System.Drawing.Point(233, 53);
			this.btnRunCustomCmd.Name = "btnRunCustomCmd";
			this.btnRunCustomCmd.Size = new System.Drawing.Size(58, 23);
			this.btnRunCustomCmd.TabIndex = 4;
			this.btnRunCustomCmd.Text = "&Run!";
			this.btnRunCustomCmd.UseVisualStyleBackColor = true;
			this.btnRunCustomCmd.Click += new System.EventHandler(this.btnRunCustomCmd_Click);
			// 
			// slbMain
			// 
			this.slbMain.Name = "slbMain";
			this.slbMain.Size = new System.Drawing.Size(266, 17);
			this.slbMain.Spring = true;
			this.slbMain.Text = "Ready";
			this.slbMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(312, 115);
			this.Controls.Add(this.btnRunCustomCmd);
			this.Controls.Add(this.cmbCustomCmd);
			this.Controls.Add(this.lblCustomCmd);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.sbMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "APService Agent";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.sbMain.ResumeLayout(false);
			this.sbMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip sbMain;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnPause;
		private System.ServiceProcess.ServiceController ctrlFirstService;
		private System.Windows.Forms.Label lblCustomCmd;
		private System.Windows.Forms.ComboBox cmbCustomCmd;
		private System.Windows.Forms.Button btnRunCustomCmd;
		private System.Windows.Forms.ToolStripStatusLabel slbMain;
	}
}

