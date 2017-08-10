//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace TransportPartner
{
    partial class Launcher
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
            this.partnerOneButton = new System.Windows.Forms.Button();
            this.partnerButtonTwo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // partnerOneButton
            // 
            this.partnerOneButton.Location = new System.Drawing.Point(42, 23);
            this.partnerOneButton.Name = "partnerOneButton";
            this.partnerOneButton.Size = new System.Drawing.Size(252, 21);
            this.partnerOneButton.TabIndex = 0;
            this.partnerOneButton.Text = "Contoso Transport Partner";
            this.partnerOneButton.UseVisualStyleBackColor = true;
            this.partnerOneButton.Click += new System.EventHandler(this.OnPartnerButtonOneClick);
            // 
            // partnerButtonTwo
            // 
            this.partnerButtonTwo.Location = new System.Drawing.Point(42, 64);
            this.partnerButtonTwo.Name = "partnerButtonTwo";
            this.partnerButtonTwo.Size = new System.Drawing.Size(252, 23);
            this.partnerButtonTwo.TabIndex = 1;
            this.partnerButtonTwo.Text = "Fabrikam Transport Partner";
            this.partnerButtonTwo.UseVisualStyleBackColor = true;
            this.partnerButtonTwo.Click += new System.EventHandler(this.OnPartnerTwoButtonTwoClick);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 116);
            this.Controls.Add(this.partnerButtonTwo);
            this.Controls.Add(this.partnerOneButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Launcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Launcher";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button partnerOneButton;
        private System.Windows.Forms.Button partnerButtonTwo;
    }
}