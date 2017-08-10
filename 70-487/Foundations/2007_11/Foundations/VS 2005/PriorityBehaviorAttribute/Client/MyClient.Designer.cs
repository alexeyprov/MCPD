//2008 IDesign Inc.   
//Questions? Comments? go to 
//http://www.idesign.net

namespace Client
{
   partial class MyClient
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
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.m_LowRadioButton = new System.Windows.Forms.RadioButton();
         this.m_HighRadioButton = new System.Windows.Forms.RadioButton();
         this.m_NormalRadioButton = new System.Windows.Forms.RadioButton();
         callButton = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // callButton
         // 
         callButton.Location = new System.Drawing.Point(111,18);
         callButton.Name = "callButton";
         callButton.Size = new System.Drawing.Size(103,23);
         callButton.TabIndex = 0;
         callButton.Text = "Call Service";
         callButton.Click += new System.EventHandler(this.OnCall);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.m_NormalRadioButton);
         this.groupBox1.Controls.Add(this.m_HighRadioButton);
         this.groupBox1.Controls.Add(this.m_LowRadioButton);
         this.groupBox1.Location = new System.Drawing.Point(12,12);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(83,94);
         this.groupBox1.TabIndex = 1;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Call Priority";
         // 
         // m_LowRadioButton
         // 
         this.m_LowRadioButton.AutoSize = true;
         this.m_LowRadioButton.Location = new System.Drawing.Point(6,19);
         this.m_LowRadioButton.Name = "m_LowRadioButton";
         this.m_LowRadioButton.Size = new System.Drawing.Size(45,17);
         this.m_LowRadioButton.TabIndex = 0;
         this.m_LowRadioButton.Text = "Low";
         this.m_LowRadioButton.UseVisualStyleBackColor = true;
         this.m_LowRadioButton.CheckedChanged += new System.EventHandler(this.OnChecked);
         // 
         // m_HighRadioButton
         // 
         this.m_HighRadioButton.AutoSize = true;
         this.m_HighRadioButton.Location = new System.Drawing.Point(6,65);
         this.m_HighRadioButton.Name = "m_HighRadioButton";
         this.m_HighRadioButton.Size = new System.Drawing.Size(47,17);
         this.m_HighRadioButton.TabIndex = 1;
         this.m_HighRadioButton.Text = "High";
         this.m_HighRadioButton.UseVisualStyleBackColor = true;
         this.m_HighRadioButton.CheckedChanged += new System.EventHandler(this.OnChecked);
         // 
         // m_NormalRadioButton
         // 
         this.m_NormalRadioButton.AutoSize = true;
         this.m_NormalRadioButton.Checked = true;
         this.m_NormalRadioButton.Location = new System.Drawing.Point(6,42);
         this.m_NormalRadioButton.Name = "m_NormalRadioButton";
         this.m_NormalRadioButton.Size = new System.Drawing.Size(58,17);
         this.m_NormalRadioButton.TabIndex = 2;
         this.m_NormalRadioButton.TabStop = true;
         this.m_NormalRadioButton.Text = "Normal";
         this.m_NormalRadioButton.UseVisualStyleBackColor = true;
         this.m_NormalRadioButton.CheckedChanged += new System.EventHandler(this.OnChecked);
         // 
         // MyClient
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(219,127);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(callButton);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "MyClient";
         this.Text = "Priority Demo";
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.RadioButton m_NormalRadioButton;
      private System.Windows.Forms.RadioButton m_HighRadioButton;
      private System.Windows.Forms.RadioButton m_LowRadioButton;

   }
}

