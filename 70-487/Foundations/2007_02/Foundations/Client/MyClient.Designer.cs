//2007 IDesign Inc. 
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
         this.components = new System.ComponentModel.Container();
         System.Windows.Forms.Button callButton;
         System.Windows.Forms.Label number1Label;
         System.Windows.Forms.Label number2Label;
         System.Windows.Forms.Label resultLabel;
         System.Windows.Forms.Button getResultButton;
         System.Windows.Forms.Label methodIDLable;
         this.m_Number1TextBox = new System.Windows.Forms.TextBox();
         this.m_Number2TextBox = new System.Windows.Forms.TextBox();
         this.m_ResultTextBox = new System.Windows.Forms.TextBox();
         this.m_MethodIDComboBox = new System.Windows.Forms.ComboBox();
         this.m_UpdateTimer = new System.Windows.Forms.Timer(this.components);
         this.m_TimerCheckbox = new System.Windows.Forms.CheckBox();
         callButton = new System.Windows.Forms.Button();
         number1Label = new System.Windows.Forms.Label();
         number2Label = new System.Windows.Forms.Label();
         resultLabel = new System.Windows.Forms.Label();
         getResultButton = new System.Windows.Forms.Button();
         methodIDLable = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // callButton
         // 
         callButton.Location = new System.Drawing.Point(149,25);
         callButton.Name = "callButton";
         callButton.Size = new System.Drawing.Size(91,23);
         callButton.TabIndex = 0;
         callButton.Text = "Queued Add";
         callButton.Click += new System.EventHandler(this.OnAdd);
         // 
         // number1Label
         // 
         number1Label.AutoSize = true;
         number1Label.Location = new System.Drawing.Point(12,9);
         number1Label.Name = "number1Label";
         number1Label.Size = new System.Drawing.Size(56,13);
         number1Label.TabIndex = 1;
         number1Label.Text = "Number 1:";
         // 
         // number2Label
         // 
         number2Label.AutoSize = true;
         number2Label.Location = new System.Drawing.Point(12,63);
         number2Label.Name = "number2Label";
         number2Label.Size = new System.Drawing.Size(56,13);
         number2Label.TabIndex = 3;
         number2Label.Text = "Number 2:";
         // 
         // resultLabel
         // 
         resultLabel.AutoSize = true;
         resultLabel.Location = new System.Drawing.Point(12,119);
         resultLabel.Name = "resultLabel";
         resultLabel.Size = new System.Drawing.Size(40,13);
         resultLabel.TabIndex = 5;
         resultLabel.Text = "Result:";
         // 
         // getResultButton
         // 
         getResultButton.Location = new System.Drawing.Point(149,135);
         getResultButton.Name = "getResultButton";
         getResultButton.Size = new System.Drawing.Size(91,23);
         getResultButton.TabIndex = 7;
         getResultButton.Text = "Get Result";
         getResultButton.UseVisualStyleBackColor = true;
         getResultButton.Click += new System.EventHandler(this.OnGetResult);
         // 
         // methodIDLable
         // 
         methodIDLable.AutoSize = true;
         methodIDLable.Location = new System.Drawing.Point(146,174);
         methodIDLable.Name = "methodIDLable";
         methodIDLable.Size = new System.Drawing.Size(60,13);
         methodIDLable.TabIndex = 9;
         methodIDLable.Text = "Method ID:";
         // 
         // m_Number1TextBox
         // 
         this.m_Number1TextBox.Location = new System.Drawing.Point(15,25);
         this.m_Number1TextBox.Name = "m_Number1TextBox";
         this.m_Number1TextBox.Size = new System.Drawing.Size(100,20);
         this.m_Number1TextBox.TabIndex = 2;
         this.m_Number1TextBox.Text = "2";
         // 
         // m_Number2TextBox
         // 
         this.m_Number2TextBox.Location = new System.Drawing.Point(15,79);
         this.m_Number2TextBox.Name = "m_Number2TextBox";
         this.m_Number2TextBox.Size = new System.Drawing.Size(100,20);
         this.m_Number2TextBox.TabIndex = 4;
         this.m_Number2TextBox.Text = "3";
         // 
         // m_ResultTextBox
         // 
         this.m_ResultTextBox.Location = new System.Drawing.Point(15,135);
         this.m_ResultTextBox.Name = "m_ResultTextBox";
         this.m_ResultTextBox.Size = new System.Drawing.Size(100,20);
         this.m_ResultTextBox.TabIndex = 6;
         this.m_ResultTextBox.Text = "0";
         // 
         // m_MethodIDComboBox
         // 
         this.m_MethodIDComboBox.FormattingEnabled = true;
         this.m_MethodIDComboBox.Location = new System.Drawing.Point(149,190);
         this.m_MethodIDComboBox.Name = "m_MethodIDComboBox";
         this.m_MethodIDComboBox.Size = new System.Drawing.Size(91,21);
         this.m_MethodIDComboBox.TabIndex = 8;
         // 
         // m_UpdateTimer
         // 
         this.m_UpdateTimer.Tick += new System.EventHandler(this.OnGetResult);
         // 
         // m_TimerCheckbox
         // 
         this.m_TimerCheckbox.AutoSize = true;
         this.m_TimerCheckbox.Location = new System.Drawing.Point(15,194);
         this.m_TimerCheckbox.Name = "m_TimerCheckbox";
         this.m_TimerCheckbox.Size = new System.Drawing.Size(52,17);
         this.m_TimerCheckbox.TabIndex = 10;
         this.m_TimerCheckbox.Text = "Timer";
         this.m_TimerCheckbox.UseVisualStyleBackColor = true;
         this.m_TimerCheckbox.CheckedChanged += new System.EventHandler(this.OnTimerChecked);
         // 
         // MyClient
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(252,223);
         this.Controls.Add(this.m_TimerCheckbox);
         this.Controls.Add(methodIDLable);
         this.Controls.Add(this.m_MethodIDComboBox);
         this.Controls.Add(getResultButton);
         this.Controls.Add(this.m_ResultTextBox);
         this.Controls.Add(resultLabel);
         this.Controls.Add(this.m_Number2TextBox);
         this.Controls.Add(number2Label);
         this.Controls.Add(this.m_Number1TextBox);
         this.Controls.Add(number1Label);
         this.Controls.Add(callButton);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "MyClient";
         this.Text = "Response Service Demo";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox m_Number2TextBox;
      private System.Windows.Forms.TextBox m_ResultTextBox;
      private System.Windows.Forms.TextBox m_Number1TextBox;
      private System.Windows.Forms.ComboBox m_MethodIDComboBox;
      private System.Windows.Forms.Timer m_UpdateTimer;
      private System.Windows.Forms.CheckBox m_TimerCheckbox;

   }
}

