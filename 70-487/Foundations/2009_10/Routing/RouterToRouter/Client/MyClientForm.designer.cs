
partial class MyClientForm
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
      System.Windows.Forms.Button callButton;
      this.pictureBox = new System.Windows.Forms.PictureBox();
      callButton = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // callButton
      // 
      callButton.Location = new System.Drawing.Point(420,16);
      callButton.Name = "callButton";
      callButton.Size = new System.Drawing.Size(80,23);
      callButton.TabIndex = 0;
      callButton.Text = "Call Services";
      callButton.UseVisualStyleBackColor = true;
      callButton.Click += new System.EventHandler(this.OnCallService);
      // 
      // pictureBox
      // 
      this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pictureBox.Image = global::MyNamespace.Properties.Resources.Routers;
      this.pictureBox.Location = new System.Drawing.Point(12,16);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(386,205);
      this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox.TabIndex = 1;
      this.pictureBox.TabStop = false;
      // 
      // MyClientForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(507,233);
      this.Controls.Add(this.pictureBox);
      this.Controls.Add(callButton);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MyClientForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "Router to Router Demo";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.ResumeLayout(false);

   }

   #endregion

   private System.Windows.Forms.PictureBox pictureBox;
}