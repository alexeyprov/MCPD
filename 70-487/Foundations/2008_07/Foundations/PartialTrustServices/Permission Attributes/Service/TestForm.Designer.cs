
partial class TestForm
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
      System.Windows.Forms.Label label;
      label = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label
      // 
      label.AutoSize = true;
      label.Font = new System.Drawing.Font("Microsoft Sans Serif",16F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
      label.Location = new System.Drawing.Point(47,44);
      label.Name = "label";
      label.Size = new System.Drawing.Size(260,26);
      label.TabIndex = 0;
      label.Text = "Close Form to Continue...";
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(425,126);
      this.Controls.Add(label);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "TestForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "Partial Trust Service - Attributes";
      this.ResumeLayout(false);
      this.PerformLayout();

   }

   #endregion

}
