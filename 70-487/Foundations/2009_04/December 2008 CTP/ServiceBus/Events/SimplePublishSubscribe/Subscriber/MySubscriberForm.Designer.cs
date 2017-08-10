//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

partial class MySubscriberForm
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
      this.m_SubscribeButtton = new System.Windows.Forms.Button();
      this.m_UnsubscribeButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // m_SubscribeButtton
      // 
      this.m_SubscribeButtton.Location = new System.Drawing.Point(37,22);
      this.m_SubscribeButtton.Name = "m_SubscribeButtton";
      this.m_SubscribeButtton.Size = new System.Drawing.Size(75,23);
      this.m_SubscribeButtton.TabIndex = 2;
      this.m_SubscribeButtton.Text = "Subscribe";
      this.m_SubscribeButtton.UseVisualStyleBackColor = true;
      this.m_SubscribeButtton.Click += new System.EventHandler(this.OnSubscribe);
      // 
      // m_UnsubscribeButton
      // 
      this.m_UnsubscribeButton.Enabled = false;
      this.m_UnsubscribeButton.Location = new System.Drawing.Point(37,55);
      this.m_UnsubscribeButton.Name = "m_UnsubscribeButton";
      this.m_UnsubscribeButton.Size = new System.Drawing.Size(75,23);
      this.m_UnsubscribeButton.TabIndex = 1;
      this.m_UnsubscribeButton.Text = "Unsubscribe";
      this.m_UnsubscribeButton.UseVisualStyleBackColor = true;
      this.m_UnsubscribeButton.Click += new System.EventHandler(this.OnUnsubscribe);
      // 
      // MySubscriber
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(151,100);
      this.Controls.Add(this.m_SubscribeButtton);
      this.Controls.Add(this.m_UnsubscribeButton);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MySubscriber";
      this.Text = "My Subscriber";
      this.ResumeLayout(false);

   }

   #endregion

   private System.Windows.Forms.Button m_UnsubscribeButton;
   private System.Windows.Forms.Button m_SubscribeButtton;
}
