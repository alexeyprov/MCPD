//2006 IDesign Inc.  
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
         System.Windows.Forms.Button button1;
         callButton = new System.Windows.Forms.Button();
         button1 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // callButton
         // 
         callButton.Location = new System.Drawing.Point(58,24);
         callButton.Name = "callButton";
         callButton.Size = new System.Drawing.Size(138,23);
         callButton.TabIndex = 0;
         callButton.Text = "Call First Endpoint";
         callButton.Click += new System.EventHandler(this.OnCallFirstEndpoint);
         // 
         // button1
         // 
         button1.Location = new System.Drawing.Point(58,64);
         button1.Name = "button1";
         button1.Size = new System.Drawing.Size(138,23);
         button1.TabIndex = 1;
         button1.Text = "Call Second Endpoint";
         button1.Click += new System.EventHandler(this.OnCallSecondEndpoint);
         // 
         // MyClient
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(253,108);
         this.Controls.Add(button1);
         this.Controls.Add(callButton);
         this.Name = "MyClient";
         this.Text = "Singleton Demo";
         this.ResumeLayout(false);

      }

      #endregion

   }
}

