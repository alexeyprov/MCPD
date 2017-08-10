//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

namespace ServiceModelEx
{
   partial class NewQueueDialog
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
         System.Windows.Forms.Label addressLabel;
         System.Windows.Forms.GroupBox queueGroupBox;
         System.Windows.Forms.Label maxReadersLabel;
         System.Windows.Forms.Label renewLabel;
         System.Windows.Forms.Label dequeueLabel;
         System.Windows.Forms.Label overflowLabel;
         System.Windows.Forms.Label queueLengthLabel;
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewQueueDialog));
         this.m_MaxReadersTextBox = new System.Windows.Forms.TextBox();
         this.m_ExpirationTimePicker = new System.Windows.Forms.DateTimePicker();
         this.m_DequeueRetriesTextBox = new System.Windows.Forms.TextBox();
         this.m_OverflowComboBox = new System.Windows.Forms.ComboBox();
         this.m_QueueLengthTextBox = new System.Windows.Forms.TextBox();
         this.m_CreateButton = new System.Windows.Forms.Button();
         this.m_AddressTextBox = new System.Windows.Forms.TextBox();
         addressLabel = new System.Windows.Forms.Label();
         queueGroupBox = new System.Windows.Forms.GroupBox();
         maxReadersLabel = new System.Windows.Forms.Label();
         renewLabel = new System.Windows.Forms.Label();
         dequeueLabel = new System.Windows.Forms.Label();
         overflowLabel = new System.Windows.Forms.Label();
         queueLengthLabel = new System.Windows.Forms.Label();
         queueGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // addressLabel
         // 
         addressLabel.AutoSize = true;
         addressLabel.Location = new System.Drawing.Point(5,9);
         addressLabel.Name = "addressLabel";
         addressLabel.Size = new System.Drawing.Size(83,13);
         addressLabel.TabIndex = 0;
         addressLabel.Text = "Queue Address:";
         // 
         // queueGroupBox
         // 
         queueGroupBox.Controls.Add(this.m_MaxReadersTextBox);
         queueGroupBox.Controls.Add(maxReadersLabel);
         queueGroupBox.Controls.Add(this.m_ExpirationTimePicker);
         queueGroupBox.Controls.Add(renewLabel);
         queueGroupBox.Controls.Add(dequeueLabel);
         queueGroupBox.Controls.Add(this.m_DequeueRetriesTextBox);
         queueGroupBox.Controls.Add(overflowLabel);
         queueGroupBox.Controls.Add(this.m_OverflowComboBox);
         queueGroupBox.Controls.Add(queueLengthLabel);
         queueGroupBox.Controls.Add(this.m_QueueLengthTextBox);
         queueGroupBox.Location = new System.Drawing.Point(8,51);
         queueGroupBox.Name = "queueGroupBox";
         queueGroupBox.Size = new System.Drawing.Size(361,125);
         queueGroupBox.TabIndex = 14;
         queueGroupBox.TabStop = false;
         queueGroupBox.Text = "Queue";
         // 
         // m_MaxReadersTextBox
         // 
         this.m_MaxReadersTextBox.Location = new System.Drawing.Point(123,41);
         this.m_MaxReadersTextBox.Name = "m_MaxReadersTextBox";
         this.m_MaxReadersTextBox.Size = new System.Drawing.Size(106,20);
         this.m_MaxReadersTextBox.TabIndex = 21;
         // 
         // maxReadersLabel
         // 
         maxReadersLabel.AutoSize = true;
         maxReadersLabel.Location = new System.Drawing.Point(120,25);
         maxReadersLabel.Name = "maxReadersLabel";
         maxReadersLabel.Size = new System.Drawing.Size(73,13);
         maxReadersLabel.TabIndex = 20;
         maxReadersLabel.Text = "Max Readers:";
         // 
         // m_ExpirationTimePicker
         // 
         this.m_ExpirationTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
         this.m_ExpirationTimePicker.Location = new System.Drawing.Point(240,41);
         this.m_ExpirationTimePicker.Name = "m_ExpirationTimePicker";
         this.m_ExpirationTimePicker.Size = new System.Drawing.Size(111,20);
         this.m_ExpirationTimePicker.TabIndex = 19;
         // 
         // renewLabel
         // 
         renewLabel.AutoSize = true;
         renewLabel.Location = new System.Drawing.Point(237,25);
         renewLabel.Name = "renewLabel";
         renewLabel.Size = new System.Drawing.Size(56,13);
         renewLabel.TabIndex = 18;
         renewLabel.Text = "Expiration:";
         // 
         // dequeueLabel
         // 
         dequeueLabel.AutoSize = true;
         dequeueLabel.Location = new System.Drawing.Point(8,25);
         dequeueLabel.Name = "dequeueLabel";
         dequeueLabel.Size = new System.Drawing.Size(84,13);
         dequeueLabel.TabIndex = 15;
         dequeueLabel.Text = "Delivery Retries:";
         // 
         // m_DequeueRetriesTextBox
         // 
         this.m_DequeueRetriesTextBox.Location = new System.Drawing.Point(11,41);
         this.m_DequeueRetriesTextBox.Name = "m_DequeueRetriesTextBox";
         this.m_DequeueRetriesTextBox.Size = new System.Drawing.Size(103,20);
         this.m_DequeueRetriesTextBox.TabIndex = 16;
         // 
         // overflowLabel
         // 
         overflowLabel.AutoSize = true;
         overflowLabel.Location = new System.Drawing.Point(120,73);
         overflowLabel.Name = "overflowLabel";
         overflowLabel.Size = new System.Drawing.Size(83,13);
         overflowLabel.TabIndex = 14;
         overflowLabel.Text = "Overflow Policy:";
         // 
         // m_OverflowComboBox
         // 
         this.m_OverflowComboBox.FormattingEnabled = true;
         this.m_OverflowComboBox.Items.AddRange(new object[] {
            "Reject",
            "Discard Incoming",
            "Discard Existing"});
         this.m_OverflowComboBox.Location = new System.Drawing.Point(123,89);
         this.m_OverflowComboBox.Name = "m_OverflowComboBox";
         this.m_OverflowComboBox.Size = new System.Drawing.Size(106,21);
         this.m_OverflowComboBox.TabIndex = 13;
         // 
         // queueLengthLabel
         // 
         queueLengthLabel.AutoSize = true;
         queueLengthLabel.Location = new System.Drawing.Point(8,73);
         queueLengthLabel.Name = "queueLengthLabel";
         queueLengthLabel.Size = new System.Drawing.Size(74,13);
         queueLengthLabel.TabIndex = 0;
         queueLengthLabel.Text = "Queue length:";
         // 
         // m_QueueLengthTextBox
         // 
         this.m_QueueLengthTextBox.Location = new System.Drawing.Point(11,89);
         this.m_QueueLengthTextBox.Name = "m_QueueLengthTextBox";
         this.m_QueueLengthTextBox.Size = new System.Drawing.Size(103,20);
         this.m_QueueLengthTextBox.TabIndex = 1;
         // 
         // m_CreateButton
         // 
         this.m_CreateButton.Enabled = false;
         this.m_CreateButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
         this.m_CreateButton.Location = new System.Drawing.Point(291,195);
         this.m_CreateButton.Name = "m_CreateButton";
         this.m_CreateButton.Size = new System.Drawing.Size(78,23);
         this.m_CreateButton.TabIndex = 7;
         this.m_CreateButton.Text = "Create";
         this.m_CreateButton.UseVisualStyleBackColor = true;
         this.m_CreateButton.Click += new System.EventHandler(this.OnCreate);
         // 
         // m_AddressTextBox
         // 
         this.m_AddressTextBox.Location = new System.Drawing.Point(7,25);
         this.m_AddressTextBox.Name = "m_AddressTextBox";
         this.m_AddressTextBox.Size = new System.Drawing.Size(352,20);
         this.m_AddressTextBox.TabIndex = 1;
         this.m_AddressTextBox.TextChanged += new System.EventHandler(this.OnTextChanged);
         // 
         // NewQueueDialog
         // 
         this.AcceptButton = this.m_CreateButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(377,230);
         this.Controls.Add(queueGroupBox);
         this.Controls.Add(this.m_CreateButton);
         this.Controls.Add(this.m_AddressTextBox);
         this.Controls.Add(addressLabel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "NewQueueDialog";
         this.Text = "Create New Queue";
         queueGroupBox.ResumeLayout(false);
         queueGroupBox.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button m_CreateButton;
      private System.Windows.Forms.TextBox m_AddressTextBox;
      private System.Windows.Forms.DateTimePicker m_ExpirationTimePicker;
      private System.Windows.Forms.TextBox m_DequeueRetriesTextBox;
      private System.Windows.Forms.ComboBox m_OverflowComboBox;
      private System.Windows.Forms.TextBox m_QueueLengthTextBox;
      private System.Windows.Forms.TextBox m_MaxReadersTextBox;

   }
}