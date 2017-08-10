namespace EncryptionClient
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
			this.gbOperation = new System.Windows.Forms.GroupBox();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.rbEncrypt = new System.Windows.Forms.RadioButton();
			this.gbFiles = new System.Windows.Forms.GroupBox();
			this.btnOutputBrowse = new System.Windows.Forms.Button();
			this.btnInputBrowse = new System.Windows.Forms.Button();
			this.txtOutputFile = new System.Windows.Forms.TextBox();
			this.lblOutputFile = new System.Windows.Forms.Label();
			this.txtInputFile = new System.Windows.Forms.TextBox();
			this.lblInputFile = new System.Windows.Forms.Label();
			this.dlgInputFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgOutputFile = new System.Windows.Forms.SaveFileDialog();
			this.btnRun = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.gbOperation.SuspendLayout();
			this.gbFiles.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbOperation
			// 
			this.gbOperation.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbOperation.Controls.Add(this.radioButton1);
			this.gbOperation.Controls.Add(this.rbEncrypt);
			this.gbOperation.Location = new System.Drawing.Point(12, 12);
			this.gbOperation.Name = "gbOperation";
			this.gbOperation.Size = new System.Drawing.Size(268, 72);
			this.gbOperation.TabIndex = 0;
			this.gbOperation.TabStop = false;
			this.gbOperation.Text = "What do you want to do?";
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(7, 44);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(87, 17);
			this.radioButton1.TabIndex = 1;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "&Decrypt a file";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// rbEncrypt
			// 
			this.rbEncrypt.AutoSize = true;
			this.rbEncrypt.Checked = true;
			this.rbEncrypt.Location = new System.Drawing.Point(7, 20);
			this.rbEncrypt.Name = "rbEncrypt";
			this.rbEncrypt.Size = new System.Drawing.Size(86, 17);
			this.rbEncrypt.TabIndex = 0;
			this.rbEncrypt.TabStop = true;
			this.rbEncrypt.Text = "&Encrypt a file";
			this.rbEncrypt.UseVisualStyleBackColor = true;
			// 
			// gbFiles
			// 
			this.gbFiles.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbFiles.Controls.Add(this.btnOutputBrowse);
			this.gbFiles.Controls.Add(this.btnInputBrowse);
			this.gbFiles.Controls.Add(this.txtOutputFile);
			this.gbFiles.Controls.Add(this.lblOutputFile);
			this.gbFiles.Controls.Add(this.txtInputFile);
			this.gbFiles.Controls.Add(this.lblInputFile);
			this.gbFiles.Location = new System.Drawing.Point(12, 91);
			this.gbFiles.Name = "gbFiles";
			this.gbFiles.Size = new System.Drawing.Size(268, 114);
			this.gbFiles.TabIndex = 1;
			this.gbFiles.TabStop = false;
			this.gbFiles.Text = "File locations";
			// 
			// btnOutputBrowse
			// 
			this.btnOutputBrowse.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOutputBrowse.Location = new System.Drawing.Point(230, 77);
			this.btnOutputBrowse.Name = "btnOutputBrowse";
			this.btnOutputBrowse.Size = new System.Drawing.Size(32, 23);
			this.btnOutputBrowse.TabIndex = 5;
			this.btnOutputBrowse.Text = "...";
			this.btnOutputBrowse.UseVisualStyleBackColor = true;
			this.btnOutputBrowse.Click += new System.EventHandler(this.btnOutputBrowse_Click);
			// 
			// btnInputBrowse
			// 
			this.btnInputBrowse.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInputBrowse.Location = new System.Drawing.Point(230, 36);
			this.btnInputBrowse.Name = "btnInputBrowse";
			this.btnInputBrowse.Size = new System.Drawing.Size(32, 23);
			this.btnInputBrowse.TabIndex = 2;
			this.btnInputBrowse.Text = "...";
			this.btnInputBrowse.UseVisualStyleBackColor = true;
			this.btnInputBrowse.Click += new System.EventHandler(this.btnInputBrowse_Click);
			// 
			// txtOutputFile
			// 
			this.txtOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutputFile.Location = new System.Drawing.Point(10, 78);
			this.txtOutputFile.Name = "txtOutputFile";
			this.txtOutputFile.Size = new System.Drawing.Size(214, 20);
			this.txtOutputFile.TabIndex = 4;
			this.txtOutputFile.TextChanged += new System.EventHandler(this.OnFileNameChanged);
			// 
			// lblOutputFile
			// 
			this.lblOutputFile.AutoSize = true;
			this.lblOutputFile.Location = new System.Drawing.Point(7, 61);
			this.lblOutputFile.Name = "lblOutputFile";
			this.lblOutputFile.Size = new System.Drawing.Size(58, 13);
			this.lblOutputFile.TabIndex = 3;
			this.lblOutputFile.Text = "&Output file:";
			// 
			// txtInputFile
			// 
			this.txtInputFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInputFile.Location = new System.Drawing.Point(10, 37);
			this.txtInputFile.Name = "txtInputFile";
			this.txtInputFile.Size = new System.Drawing.Size(214, 20);
			this.txtInputFile.TabIndex = 1;
			this.txtInputFile.TextChanged += new System.EventHandler(this.OnFileNameChanged);
			// 
			// lblInputFile
			// 
			this.lblInputFile.AutoSize = true;
			this.lblInputFile.Location = new System.Drawing.Point(7, 20);
			this.lblInputFile.Name = "lblInputFile";
			this.lblInputFile.Size = new System.Drawing.Size(50, 13);
			this.lblInputFile.TabIndex = 0;
			this.lblInputFile.Text = "&Input file:";
			// 
			// dlgInputFile
			// 
			this.dlgInputFile.AddExtension = false;
			this.dlgInputFile.Filter = "All files|*.*";
			// 
			// dlgOutputFile
			// 
			this.dlgOutputFile.AddExtension = false;
			// 
			// btnRun
			// 
			this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRun.Enabled = false;
			this.btnRun.Location = new System.Drawing.Point(13, 212);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(75, 23);
			this.btnRun.TabIndex = 2;
			this.btnRun.Text = "&Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(204, 211);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "&Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// MainForm
			// 
			this.AcceptButton = this.btnRun;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(292, 245);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnRun);
			this.Controls.Add(this.gbFiles);
			this.Controls.Add(this.gbOperation);
			this.MinimumSize = new System.Drawing.Size(300, 279);
			this.Name = "MainForm";
			this.Text = "Encryption Client";
			this.gbOperation.ResumeLayout(false);
			this.gbOperation.PerformLayout();
			this.gbFiles.ResumeLayout(false);
			this.gbFiles.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbOperation;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton rbEncrypt;
		private System.Windows.Forms.GroupBox gbFiles;
		private System.Windows.Forms.Label lblInputFile;
		private System.Windows.Forms.Button btnOutputBrowse;
		private System.Windows.Forms.Button btnInputBrowse;
		private System.Windows.Forms.TextBox txtOutputFile;
		private System.Windows.Forms.Label lblOutputFile;
		private System.Windows.Forms.TextBox txtInputFile;
		private System.Windows.Forms.OpenFileDialog dlgInputFile;
		private System.Windows.Forms.SaveFileDialog dlgOutputFile;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.Button btnClose;
	}
}

