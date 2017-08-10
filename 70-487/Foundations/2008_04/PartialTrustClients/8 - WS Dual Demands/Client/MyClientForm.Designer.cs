//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

partial class MyClientForm
{
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
      callButton.Location = new System.Drawing.Point(182,40);
      callButton.Name = "callButton";
      callButton.Size = new System.Drawing.Size(75,23);
      callButton.TabIndex = 0;
      callButton.Text = "Call Service";
      callButton.Click += new System.EventHandler(this.OnCall);
      // 
      // MyClientForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(419,108);
      this.Controls.Add(callButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MyClientForm";
      this.Text = "WS Dual Demands";
      this.ResumeLayout(false);

   }

   #endregion
}

