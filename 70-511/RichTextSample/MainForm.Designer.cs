namespace RichTextSample
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
			this.txtGreeting = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// txtGreeting
			// 
			this.txtGreeting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtGreeting.Location = new System.Drawing.Point(0, 0);
			this.txtGreeting.Name = "txtGreeting";
			this.txtGreeting.ReadOnly = true;
			this.txtGreeting.Size = new System.Drawing.Size(292, 275);
			this.txtGreeting.TabIndex = 0;
			this.txtGreeting.Text = "";
			this.txtGreeting.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtGreeting_LinkClicked);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 275);
			this.Controls.Add(this.txtGreeting);
			this.Name = "MainForm";
			this.Text = "Rich Edit Sample";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtGreeting;
	}
}

