namespace ServiceModelEx
{
   partial class LogonDialog
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
         if(disposing && (components != null))
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
         System.Windows.Forms.GroupBox credsTypeGroupBox;
         System.Windows.Forms.Label storeNameLabel;
         System.Windows.Forms.GroupBox usernamePasswordGroubBox;
         System.Windows.Forms.Label passwordLabel;
         System.Windows.Forms.Label solutionLabel;
         System.Windows.Forms.GroupBox certificateGroupBox;
         System.Windows.Forms.Label storeLable;
         System.Windows.Forms.Label findValueLabel;
         System.Windows.Forms.Label certValueLabel;
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonDialog));
         this.m_CertRadioButton = new System.Windows.Forms.RadioButton();
         this.m_PasswordRadioButton = new System.Windows.Forms.RadioButton();
         this.m_CardSpaceRadioButton = new System.Windows.Forms.RadioButton();
         this.m_PasswordTextBox = new System.Windows.Forms.TextBox();
         this.m_SolutionTextBox = new System.Windows.Forms.TextBox();
         this.m_StoreNameComboBox = new System.Windows.Forms.ComboBox();
         this.m_StoreLoctionComboBox = new System.Windows.Forms.ComboBox();
         this.m_FindValueComboBox = new System.Windows.Forms.ComboBox();
         this.m_CertNValueTextBox = new System.Windows.Forms.TextBox();
         this.m_LogonButton = new System.Windows.Forms.Button();
         credsTypeGroupBox = new System.Windows.Forms.GroupBox();
         storeNameLabel = new System.Windows.Forms.Label();
         usernamePasswordGroubBox = new System.Windows.Forms.GroupBox();
         passwordLabel = new System.Windows.Forms.Label();
         solutionLabel = new System.Windows.Forms.Label();
         certificateGroupBox = new System.Windows.Forms.GroupBox();
         storeLable = new System.Windows.Forms.Label();
         findValueLabel = new System.Windows.Forms.Label();
         certValueLabel = new System.Windows.Forms.Label();
         credsTypeGroupBox.SuspendLayout();
         usernamePasswordGroubBox.SuspendLayout();
         certificateGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // credsTypeGroupBox
         // 
         credsTypeGroupBox.Controls.Add(this.m_CertRadioButton);
         credsTypeGroupBox.Controls.Add(this.m_PasswordRadioButton);
         credsTypeGroupBox.Controls.Add(this.m_CardSpaceRadioButton);
         credsTypeGroupBox.Enabled = false;
         credsTypeGroupBox.Location = new System.Drawing.Point(17,17);
         credsTypeGroupBox.Name = "credsTypeGroupBox";
         credsTypeGroupBox.Size = new System.Drawing.Size(180,89);
         credsTypeGroupBox.TabIndex = 8;
         credsTypeGroupBox.TabStop = false;
         credsTypeGroupBox.Text = "Credentials Type";
         // 
         // m_CertRadioButton
         // 
         this.m_CertRadioButton.AutoSize = true;
         this.m_CertRadioButton.Enabled = false;
         this.m_CertRadioButton.Location = new System.Drawing.Point(6,65);
         this.m_CertRadioButton.Name = "m_CertRadioButton";
         this.m_CertRadioButton.Size = new System.Drawing.Size(72,17);
         this.m_CertRadioButton.TabIndex = 7;
         this.m_CertRadioButton.Text = "Certificate";
         this.m_CertRadioButton.UseVisualStyleBackColor = true;
         // 
         // m_PasswordRadioButton
         // 
         this.m_PasswordRadioButton.AutoSize = true;
         this.m_PasswordRadioButton.Checked = true;
         this.m_PasswordRadioButton.Enabled = false;
         this.m_PasswordRadioButton.Location = new System.Drawing.Point(6,42);
         this.m_PasswordRadioButton.Name = "m_PasswordRadioButton";
         this.m_PasswordRadioButton.Size = new System.Drawing.Size(71,17);
         this.m_PasswordRadioButton.TabIndex = 6;
         this.m_PasswordRadioButton.TabStop = true;
         this.m_PasswordRadioButton.Text = "Password";
         this.m_PasswordRadioButton.UseVisualStyleBackColor = true;
         // 
         // m_CardSpaceRadioButton
         // 
         this.m_CardSpaceRadioButton.AutoSize = true;
         this.m_CardSpaceRadioButton.Enabled = false;
         this.m_CardSpaceRadioButton.Location = new System.Drawing.Point(6,19);
         this.m_CardSpaceRadioButton.Name = "m_CardSpaceRadioButton";
         this.m_CardSpaceRadioButton.Size = new System.Drawing.Size(121,17);
         this.m_CardSpaceRadioButton.TabIndex = 5;
         this.m_CardSpaceRadioButton.Text = "CardSpace (Default)";
         this.m_CardSpaceRadioButton.UseVisualStyleBackColor = true;
         // 
         // storeNameLabel
         // 
         storeNameLabel.AutoSize = true;
         storeNameLabel.Location = new System.Drawing.Point(6,156);
         storeNameLabel.Name = "storeNameLabel";
         storeNameLabel.Size = new System.Drawing.Size(66,13);
         storeNameLabel.TabIndex = 6;
         storeNameLabel.Text = "Store Name:";
         // 
         // usernamePasswordGroubBox
         // 
         usernamePasswordGroubBox.Controls.Add(this.m_PasswordTextBox);
         usernamePasswordGroubBox.Controls.Add(passwordLabel);
         usernamePasswordGroubBox.Controls.Add(this.m_SolutionTextBox);
         usernamePasswordGroubBox.Controls.Add(solutionLabel);
         usernamePasswordGroubBox.Location = new System.Drawing.Point(17,112);
         usernamePasswordGroubBox.Name = "usernamePasswordGroubBox";
         usernamePasswordGroubBox.Size = new System.Drawing.Size(180,116);
         usernamePasswordGroubBox.TabIndex = 6;
         usernamePasswordGroubBox.TabStop = false;
         usernamePasswordGroubBox.Text = "Password Credentials";
         // 
         // m_PasswordTextBox
         // 
         this.m_PasswordTextBox.Location = new System.Drawing.Point(6,78);
         this.m_PasswordTextBox.Name = "m_PasswordTextBox";
         this.m_PasswordTextBox.PasswordChar = '*';
         this.m_PasswordTextBox.Size = new System.Drawing.Size(157,20);
         this.m_PasswordTextBox.TabIndex = 3;
         this.m_PasswordTextBox.Text = "MyPassword";
         this.m_PasswordTextBox.UseSystemPasswordChar = true;
         this.m_PasswordTextBox.TextChanged += new System.EventHandler(this.OnTextChanged);
         // 
         // passwordLabel
         // 
         passwordLabel.AutoSize = true;
         passwordLabel.Location = new System.Drawing.Point(3,62);
         passwordLabel.Name = "passwordLabel";
         passwordLabel.Size = new System.Drawing.Size(56,13);
         passwordLabel.TabIndex = 2;
         passwordLabel.Text = "Password:";
         // 
         // m_SolutionTextBox
         // 
         this.m_SolutionTextBox.Enabled = false;
         this.m_SolutionTextBox.Location = new System.Drawing.Point(6,35);
         this.m_SolutionTextBox.Name = "m_SolutionTextBox";
         this.m_SolutionTextBox.Size = new System.Drawing.Size(157,20);
         this.m_SolutionTextBox.TabIndex = 1;
         // 
         // solutionLabel
         // 
         solutionLabel.AutoSize = true;
         solutionLabel.Location = new System.Drawing.Point(3,19);
         solutionLabel.Name = "solutionLabel";
         solutionLabel.Size = new System.Drawing.Size(48,13);
         solutionLabel.TabIndex = 0;
         solutionLabel.Text = "Solution:";
         // 
         // certificateGroupBox
         // 
         certificateGroupBox.Controls.Add(this.m_StoreNameComboBox);
         certificateGroupBox.Controls.Add(storeNameLabel);
         certificateGroupBox.Controls.Add(this.m_StoreLoctionComboBox);
         certificateGroupBox.Controls.Add(storeLable);
         certificateGroupBox.Controls.Add(this.m_FindValueComboBox);
         certificateGroupBox.Controls.Add(findValueLabel);
         certificateGroupBox.Controls.Add(this.m_CertNValueTextBox);
         certificateGroupBox.Controls.Add(certValueLabel);
         certificateGroupBox.Enabled = false;
         certificateGroupBox.Location = new System.Drawing.Point(216,17);
         certificateGroupBox.Name = "certificateGroupBox";
         certificateGroupBox.Size = new System.Drawing.Size(143,211);
         certificateGroupBox.TabIndex = 5;
         certificateGroupBox.TabStop = false;
         certificateGroupBox.Text = "Certificate:";
         // 
         // m_StoreNameComboBox
         // 
         this.m_StoreNameComboBox.Enabled = false;
         this.m_StoreNameComboBox.FormattingEnabled = true;
         this.m_StoreNameComboBox.Items.AddRange(new object[] {
            "My"});
         this.m_StoreNameComboBox.Location = new System.Drawing.Point(6,172);
         this.m_StoreNameComboBox.Name = "m_StoreNameComboBox";
         this.m_StoreNameComboBox.Size = new System.Drawing.Size(121,21);
         this.m_StoreNameComboBox.TabIndex = 7;
         // 
         // m_StoreLoctionComboBox
         // 
         this.m_StoreLoctionComboBox.Enabled = false;
         this.m_StoreLoctionComboBox.FormattingEnabled = true;
         this.m_StoreLoctionComboBox.Items.AddRange(new object[] {
            "Local Machine"});
         this.m_StoreLoctionComboBox.Location = new System.Drawing.Point(6,129);
         this.m_StoreLoctionComboBox.Name = "m_StoreLoctionComboBox";
         this.m_StoreLoctionComboBox.Size = new System.Drawing.Size(121,21);
         this.m_StoreLoctionComboBox.TabIndex = 5;
         // 
         // storeLable
         // 
         storeLable.AutoSize = true;
         storeLable.Location = new System.Drawing.Point(6,113);
         storeLable.Name = "storeLable";
         storeLable.Size = new System.Drawing.Size(79,13);
         storeLable.TabIndex = 4;
         storeLable.Text = "Store Location:";
         // 
         // m_FindValueComboBox
         // 
         this.m_FindValueComboBox.Enabled = false;
         this.m_FindValueComboBox.FormattingEnabled = true;
         this.m_FindValueComboBox.Items.AddRange(new object[] {
            "By Subject Name"});
         this.m_FindValueComboBox.Location = new System.Drawing.Point(6,89);
         this.m_FindValueComboBox.Name = "m_FindValueComboBox";
         this.m_FindValueComboBox.Size = new System.Drawing.Size(121,21);
         this.m_FindValueComboBox.TabIndex = 3;
         // 
         // findValueLabel
         // 
         findValueLabel.AutoSize = true;
         findValueLabel.Location = new System.Drawing.Point(3,73);
         findValueLabel.Name = "findValueLabel";
         findValueLabel.Size = new System.Drawing.Size(75,13);
         findValueLabel.TabIndex = 2;
         findValueLabel.Text = "Find Value By:";
         // 
         // m_CertNValueTextBox
         // 
         this.m_CertNValueTextBox.Enabled = false;
         this.m_CertNValueTextBox.Location = new System.Drawing.Point(6,35);
         this.m_CertNValueTextBox.Name = "m_CertNValueTextBox";
         this.m_CertNValueTextBox.Size = new System.Drawing.Size(124,20);
         this.m_CertNValueTextBox.TabIndex = 1;
         // 
         // certValueLabel
         // 
         certValueLabel.AutoSize = true;
         certValueLabel.Location = new System.Drawing.Point(3,16);
         certValueLabel.Name = "certValueLabel";
         certValueLabel.Size = new System.Drawing.Size(87,13);
         certValueLabel.TabIndex = 0;
         certValueLabel.Text = "Certificate Value:";
         // 
         // m_LogonButton
         // 
         this.m_LogonButton.Enabled = false;
         this.m_LogonButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
         this.m_LogonButton.Location = new System.Drawing.Point(281,247);
         this.m_LogonButton.Name = "m_LogonButton";
         this.m_LogonButton.Size = new System.Drawing.Size(78,23);
         this.m_LogonButton.TabIndex = 7;
         this.m_LogonButton.Text = "Login";
         this.m_LogonButton.UseVisualStyleBackColor = true;
         this.m_LogonButton.Click += new System.EventHandler(this.OnLogon);
         // 
         // LogonDialog
         // 
         this.AcceptButton = this.m_LogonButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(377,287);
         this.Controls.Add(credsTypeGroupBox);
         this.Controls.Add(this.m_LogonButton);
         this.Controls.Add(usernamePasswordGroubBox);
         this.Controls.Add(certificateGroupBox);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "LogonDialog";
         this.Text = "Logon to the .NET Service Bus";
         credsTypeGroupBox.ResumeLayout(false);
         credsTypeGroupBox.PerformLayout();
         usernamePasswordGroubBox.ResumeLayout(false);
         usernamePasswordGroubBox.PerformLayout();
         certificateGroupBox.ResumeLayout(false);
         certificateGroupBox.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.RadioButton m_CertRadioButton;
      private System.Windows.Forms.RadioButton m_PasswordRadioButton;
      private System.Windows.Forms.RadioButton m_CardSpaceRadioButton;
      private System.Windows.Forms.ComboBox m_StoreNameComboBox;
      private System.Windows.Forms.TextBox m_PasswordTextBox;
      private System.Windows.Forms.TextBox m_SolutionTextBox;
      private System.Windows.Forms.ComboBox m_StoreLoctionComboBox;
      private System.Windows.Forms.ComboBox m_FindValueComboBox;
      private System.Windows.Forms.TextBox m_CertNValueTextBox;
      private System.Windows.Forms.Button m_LogonButton;

   }
}