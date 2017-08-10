// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net


partial class LoginForm
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
      System.Windows.Forms.Button loginButton;
      System.Windows.Forms.Label passwordLabel;
      this.m_SolutionLabel = new System.Windows.Forms.Label();
      this.m_PasswordTextBox = new System.Windows.Forms.TextBox();
      loginButton = new System.Windows.Forms.Button();
      passwordLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // loginButton
      // 
      loginButton.Location = new System.Drawing.Point(166,81);
      loginButton.Name = "loginButton";
      loginButton.Size = new System.Drawing.Size(132,23);
      loginButton.TabIndex = 1;
      loginButton.Text = "Log In to Services Bus";
      loginButton.UseVisualStyleBackColor = true;
      loginButton.Click += new System.EventHandler(this.OnLogin);
      // 
      // passwordLabel
      // 
      passwordLabel.AutoSize = true;
      passwordLabel.Location = new System.Drawing.Point(12,65);
      passwordLabel.Name = "passwordLabel";
      passwordLabel.Size = new System.Drawing.Size(56,13);
      passwordLabel.TabIndex = 3;
      passwordLabel.Text = "Password:";
      // 
      // m_SolutionLabel
      // 
      this.m_SolutionLabel.AutoSize = true;
      this.m_SolutionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
      this.m_SolutionLabel.Location = new System.Drawing.Point(12,25);
      this.m_SolutionLabel.Name = "m_SolutionLabel";
      this.m_SolutionLabel.Size = new System.Drawing.Size(61,13);
      this.m_SolutionLabel.TabIndex = 0;
      this.m_SolutionLabel.Text = "Solution: ";
      // 
      // m_PasswordTextBox
      // 
      this.m_PasswordTextBox.Location = new System.Drawing.Point(15,81);
      this.m_PasswordTextBox.Name = "m_PasswordTextBox";
      this.m_PasswordTextBox.PasswordChar = '*';
      this.m_PasswordTextBox.Size = new System.Drawing.Size(123,20);
      this.m_PasswordTextBox.TabIndex = 2;
      this.m_PasswordTextBox.Text = "123@abc";
      // 
      // LoginForm
      // 
      this.AcceptButton = loginButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(308,135);
      this.Controls.Add(passwordLabel);
      this.Controls.Add(this.m_PasswordTextBox);
      this.Controls.Add(loginButton);
      this.Controls.Add(this.m_SolutionLabel);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "LoginForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "Client Login Dialog";
      this.ResumeLayout(false);
      this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.Label m_SolutionLabel;
   private System.Windows.Forms.TextBox m_PasswordTextBox;
}