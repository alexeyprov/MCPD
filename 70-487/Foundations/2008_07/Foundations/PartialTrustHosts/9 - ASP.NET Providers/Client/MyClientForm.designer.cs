//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

partial class MyClientForm
{
   /// <summary>
   /// Required designer variable.
   /// </summary>
   private System.ComponentModel.IContainer components = null;

   /// <summary>
   /// Clean up any resources being used.
   /// </summary>
   /// <param name="disposing">true if managed resources should be disposed; otherwise,false.</param>
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
      System.Windows.Forms.Button callButton;
      System.Windows.Forms.Label userNameLabel;
      System.Windows.Forms.Label passwordLabel;
      this.m_UserNameTextbox = new System.Windows.Forms.TextBox();
      this.m_PasswordTextBox = new System.Windows.Forms.TextBox();
      callButton = new System.Windows.Forms.Button();
      userNameLabel = new System.Windows.Forms.Label();
      passwordLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // callButton
      // 
      callButton.Location = new System.Drawing.Point(147,35);
      callButton.Name = "callButton";
      callButton.Size = new System.Drawing.Size(75,23);
      callButton.TabIndex = 0;
      callButton.Text = "Call Service";
      callButton.Click += new System.EventHandler(this.OnCall);
      // 
      // userNameLabel
      // 
      userNameLabel.AutoSize = true;
      userNameLabel.Location = new System.Drawing.Point(9,20);
      userNameLabel.Name = "userNameLabel";
      userNameLabel.Size = new System.Drawing.Size(61,13);
      userNameLabel.TabIndex = 3;
      userNameLabel.Text = "User name:";
      // 
      // passwordLabel
      // 
      passwordLabel.AutoSize = true;
      passwordLabel.Location = new System.Drawing.Point(9,71);
      passwordLabel.Name = "passwordLabel";
      passwordLabel.Size = new System.Drawing.Size(53,13);
      passwordLabel.TabIndex = 5;
      passwordLabel.Text = "Password";
      // 
      // m_UserNameTextbox
      // 
      this.m_UserNameTextbox.Location = new System.Drawing.Point(12,37);
      this.m_UserNameTextbox.Name = "m_UserNameTextbox";
      this.m_UserNameTextbox.Size = new System.Drawing.Size(100,20);
      this.m_UserNameTextbox.TabIndex = 4;
      this.m_UserNameTextbox.Text = "MyClient";
      // 
      // m_PasswordTextBox
      // 
      this.m_PasswordTextBox.Location = new System.Drawing.Point(12,88);
      this.m_PasswordTextBox.Name = "m_PasswordTextBox";
      this.m_PasswordTextBox.PasswordChar = '*';
      this.m_PasswordTextBox.Size = new System.Drawing.Size(100,20);
      this.m_PasswordTextBox.TabIndex = 6;
      this.m_PasswordTextBox.Text = "123@abc";
      // 
      // MyClientForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(235,121);
      this.Controls.Add(this.m_PasswordTextBox);
      this.Controls.Add(passwordLabel);
      this.Controls.Add(this.m_UserNameTextbox);
      this.Controls.Add(userNameLabel);
      this.Controls.Add(callButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MyClientForm";
      this.Text = "ASP.NET Providers Demands";
      this.ResumeLayout(false);
      this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.TextBox m_UserNameTextbox;
   private System.Windows.Forms.TextBox m_PasswordTextBox;

}

