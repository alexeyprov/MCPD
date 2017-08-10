namespace SecHostApp
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
			this.gbEventLog = new System.Windows.Forms.GroupBox();
			this.lvwEventLog = new System.Windows.Forms.ListView();
			this.colEvent = new System.Windows.Forms.ColumnHeader();
			this.colResult = new System.Windows.Forms.ColumnHeader();
			this.chkCustomPerm = new System.Windows.Forms.CheckBox();
			this.btnTrusted = new System.Windows.Forms.Button();
			this.btnUntrusted = new System.Windows.Forms.Button();
			this.gbEventLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbEventLog
			// 
			this.gbEventLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbEventLog.Controls.Add(this.lvwEventLog);
			this.gbEventLog.Location = new System.Drawing.Point(12, 12);
			this.gbEventLog.Name = "gbEventLog";
			this.gbEventLog.Size = new System.Drawing.Size(201, 242);
			this.gbEventLog.TabIndex = 0;
			this.gbEventLog.TabStop = false;
			this.gbEventLog.Text = "&Event Log";
			// 
			// lvwEventLog
			// 
			this.lvwEventLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEvent,
            this.colResult});
			this.lvwEventLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwEventLog.Location = new System.Drawing.Point(3, 16);
			this.lvwEventLog.Name = "lvwEventLog";
			this.lvwEventLog.Size = new System.Drawing.Size(195, 223);
			this.lvwEventLog.TabIndex = 0;
			this.lvwEventLog.UseCompatibleStateImageBehavior = false;
			this.lvwEventLog.View = System.Windows.Forms.View.Details;
			// 
			// colEvent
			// 
			this.colEvent.Text = "Event";
			this.colEvent.Width = 130;
			// 
			// colResult
			// 
			this.colResult.Text = "Result";
			// 
			// chkCustomPerm
			// 
			this.chkCustomPerm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkCustomPerm.AutoSize = true;
			this.chkCustomPerm.Location = new System.Drawing.Point(217, 236);
			this.chkCustomPerm.Name = "chkCustomPerm";
			this.chkCustomPerm.Size = new System.Drawing.Size(113, 17);
			this.chkCustomPerm.TabIndex = 1;
			this.chkCustomPerm.Text = "&Custom permission";
			this.chkCustomPerm.UseVisualStyleBackColor = true;
			// 
			// btnTrusted
			// 
			this.btnTrusted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTrusted.Location = new System.Drawing.Point(235, 28);
			this.btnTrusted.Name = "btnTrusted";
			this.btnTrusted.Size = new System.Drawing.Size(95, 23);
			this.btnTrusted.TabIndex = 2;
			this.btnTrusted.Text = "&Trusted Code";
			this.btnTrusted.UseVisualStyleBackColor = true;
			this.btnTrusted.Click += new System.EventHandler(this.btnTrusted_Click);
			// 
			// btnUntrusted
			// 
			this.btnUntrusted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUntrusted.Location = new System.Drawing.Point(235, 57);
			this.btnUntrusted.Name = "btnUntrusted";
			this.btnUntrusted.Size = new System.Drawing.Size(95, 23);
			this.btnUntrusted.TabIndex = 2;
			this.btnUntrusted.Text = "&Untrusted Code";
			this.btnUntrusted.UseVisualStyleBackColor = true;
			this.btnUntrusted.Click += new System.EventHandler(this.btnUntrusted_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(343, 266);
			this.Controls.Add(this.btnUntrusted);
			this.Controls.Add(this.btnTrusted);
			this.Controls.Add(this.chkCustomPerm);
			this.Controls.Add(this.gbEventLog);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(351, 300);
			this.Name = "MainForm";
			this.Text = "Security Test Application";
			this.gbEventLog.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gbEventLog;
		private System.Windows.Forms.ListView lvwEventLog;
		private System.Windows.Forms.ColumnHeader colEvent;
		private System.Windows.Forms.CheckBox chkCustomPerm;
		private System.Windows.Forms.ColumnHeader colResult;
		private System.Windows.Forms.Button btnTrusted;
		private System.Windows.Forms.Button btnUntrusted;
	}
}

