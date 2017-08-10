//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System.Windows.Forms;
namespace Client
{
   partial class MyClient : Form
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
         System.Windows.Forms.Button callServiceAButton;
         System.Windows.Forms.Button callServiceBButton;
         System.Windows.Forms.PictureBox pictureBox;
         callServiceAButton = new System.Windows.Forms.Button();
         callServiceBButton = new System.Windows.Forms.Button();
         pictureBox = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(pictureBox)).BeginInit();
         this.SuspendLayout();
         // 
         // callServiceAButton
         // 
         callServiceAButton.Location = new System.Drawing.Point(12,27);
         callServiceAButton.Name = "callServiceAButton";
         callServiceAButton.Size = new System.Drawing.Size(159,23);
         callServiceAButton.TabIndex = 0;
         callServiceAButton.Text = "Call MyService";
         callServiceAButton.Click += new System.EventHandler(this.OnCallMyService);
         // 
         // callServiceBButton
         // 
         callServiceBButton.Location = new System.Drawing.Point(229,27);
         callServiceBButton.Name = "callServiceBButton";
         callServiceBButton.Size = new System.Drawing.Size(220,23);
         callServiceBButton.TabIndex = 2;
         callServiceBButton.Text = "Pass Reference to MyOtherService";
         callServiceBButton.Click += new System.EventHandler(this.OnPassReference);
         // 
         // pictureBox
         // 
         pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         pictureBox.Image = Client.Properties.Resources.Diagram;
         pictureBox.Location = new System.Drawing.Point(12,68);
         pictureBox.Name = "pictureBox";
         pictureBox.Size = new System.Drawing.Size(437,196);
         pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         pictureBox.TabIndex = 1;
         pictureBox.TabStop = false;
         // 
         // MyClient
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.SystemColors.Control;
         this.ClientSize = new System.Drawing.Size(458,280);
         this.Controls.Add(callServiceBButton);
         this.Controls.Add(pictureBox);
         this.Controls.Add(callServiceAButton);
         this.Name = "MyClient";
         this.Text = "Service Reference Demo";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
         ((System.ComponentModel.ISupportInitialize)(pictureBox)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion


   }
}

