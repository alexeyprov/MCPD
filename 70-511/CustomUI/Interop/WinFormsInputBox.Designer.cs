namespace CustomUI.Interop
{
    partial class WinFormsInputBox
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
            this.lblLabel = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.buttonWrapper = new CustomUI.Interop.ButtonWrapper();
            this.SuspendLayout();
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(13, 13);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(0, 13);
            this.lblLabel.TabIndex = 0;
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(13, 30);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(259, 20);
            this.txtText.TabIndex = 1;
            // 
            // elementHost
            // 
            this.elementHost.Location = new System.Drawing.Point(16, 56);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(84, 26);
            this.elementHost.TabIndex = 5;
            this.elementHost.Child = this.buttonWrapper;
            // 
            // WinFormsInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.elementHost);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.lblLabel);
            this.Name = "WinFormsInputBox";
            this.Text = "WinFormsInputBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Integration.ElementHost elementHost;
        private ButtonWrapper buttonWrapper;
    }
}