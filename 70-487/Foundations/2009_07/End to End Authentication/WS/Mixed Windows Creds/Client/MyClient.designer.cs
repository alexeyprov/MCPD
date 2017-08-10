// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

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
      callButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // callButton
      // 
      callButton.Location = new System.Drawing.Point(75,42);
      callButton.Name = "callButton";
      callButton.Size = new System.Drawing.Size(75,23);
      callButton.TabIndex = 0;
      callButton.Text = "Call Service";
      callButton.Click += new System.EventHandler(this.OnCall);
      // 
      // MyClient
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(235,121);
      this.Controls.Add(callButton);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MyClient";
      this.Text = "Mixed with Windows Creds";
      this.ResumeLayout(false);

   }

   #endregion


}

