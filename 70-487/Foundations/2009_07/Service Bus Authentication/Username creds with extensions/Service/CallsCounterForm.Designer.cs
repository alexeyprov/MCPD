
partial class CallsCounterForm
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
      System.Windows.Forms.Label callsLabel;
      this.m_CounterLabel = new System.Windows.Forms.Label();
      callsLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // callsLabel
      // 
      callsLabel.AutoSize = true;
      callsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
      callsLabel.Location = new System.Drawing.Point(25,32);
      callsLabel.Name = "callsLabel";
      callsLabel.Size = new System.Drawing.Size(38,13);
      callsLabel.TabIndex = 0;
      callsLabel.Text = "Calls:";
      // 
      // m_CounterLabel
      // 
      this.m_CounterLabel.AutoSize = true;
      this.m_CounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif",32F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
      this.m_CounterLabel.ForeColor = System.Drawing.Color.Red;
      this.m_CounterLabel.Location = new System.Drawing.Point(17,54);
      this.m_CounterLabel.Name = "m_CounterLabel";
      this.m_CounterLabel.Size = new System.Drawing.Size(46,51);
      this.m_CounterLabel.TabIndex = 2;
      this.m_CounterLabel.Text = "0";
      // 
      // CallsCounterForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(169,114);
      this.Controls.Add(this.m_CounterLabel);
      this.Controls.Add(callsLabel);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CallsCounterForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "Calls Counter Form";
      this.ResumeLayout(false);
      this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.Label m_CounterLabel;
}