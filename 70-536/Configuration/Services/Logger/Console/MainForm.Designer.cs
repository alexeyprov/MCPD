namespace Console
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
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnLog = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(12, 9);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(97, 13);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "Enter log &message:";
			// 
			// btnLog
			// 
			this.btnLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLog.Location = new System.Drawing.Point(13, 54);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(75, 23);
			this.btnLog.TabIndex = 2;
			this.btnLog.Text = "&Log";
			this.btnLog.UseVisualStyleBackColor = true;
			this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(207, 54);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "&Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Console.Properties.Settings.Default, "LogMessage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtMessage.Location = new System.Drawing.Point(13, 26);
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(269, 20);
			this.txtMessage.TabIndex = 1;
			this.txtMessage.Text = global::Console.Properties.Settings.Default.LogMessage;
			// 
			// MainForm
			// 
			this.AcceptButton = this.btnLog;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(292, 83);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnLog);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.lblMessage);
			this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Console.Properties.Settings.Default, "MainFormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Location = global::Console.Properties.Settings.Default.MainFormLocation;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(298, 108);
			this.Name = "MainForm";
			this.Text = "Logger Console";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Button btnLog;
		private System.Windows.Forms.Button btnClose;
	}
}

