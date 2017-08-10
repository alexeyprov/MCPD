//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

namespace ServiceModelEx
{
   partial class QueueViewControl
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
         if(disposing &&(components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueueViewControl));
         System.Windows.Forms.Timer dirtyTimer;
         System.Windows.Forms.GroupBox QueueGroupBox;
         System.Windows.Forms.Label maxReadersLabel;
         System.Windows.Forms.Label renewLabel;
         System.Windows.Forms.Label dequeueLabel;
         System.Windows.Forms.Label queueLengthLabel;
         System.Windows.Forms.Label overflowLabel;
         System.Windows.Forms.Button deleteButton;
         this.m_MaxReadersTextBox = new System.Windows.Forms.TextBox();
         this.m_ExpirationTimePicker = new System.Windows.Forms.DateTimePicker();
         this.m_PurgeButton = new System.Windows.Forms.Button();
         this.m_DequeueRetriesTextBox = new System.Windows.Forms.TextBox();
         this.m_QueueLengthTextBox = new System.Windows.Forms.TextBox();
         this.m_OverflowComboBox = new System.Windows.Forms.ComboBox();
         this.m_ResetButton = new System.Windows.Forms.Button();
         this.m_UpdateButton = new System.Windows.Forms.Button();
         dirtyTimer = new System.Windows.Forms.Timer(this.components);
         QueueGroupBox = new System.Windows.Forms.GroupBox();
         maxReadersLabel = new System.Windows.Forms.Label();
         renewLabel = new System.Windows.Forms.Label();
         dequeueLabel = new System.Windows.Forms.Label();
         queueLengthLabel = new System.Windows.Forms.Label();
         overflowLabel = new System.Windows.Forms.Label();
         deleteButton = new System.Windows.Forms.Button();
         ((System.ComponentModel.ISupportInitialize)(this.m_ControlPictureBox)).BeginInit();
         QueueGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_AddressLabel
         // 
         this.m_AddressLabel.Visible = true;
         // 
         // m_ItemNameLabel
         // 
         this.m_ItemNameLabel.Size = new System.Drawing.Size(107,24);
         this.m_ItemNameLabel.Text = "My Queue";
         this.m_ItemNameLabel.Visible = true;
         // 
         // m_ControlPictureBox
         // 
         this.m_ControlPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("m_ControlPictureBox.Image")));
         this.m_ControlPictureBox.Size = new System.Drawing.Size(59,60);
         this.m_ControlPictureBox.Visible = true;
         // 
         // m_ControlAddressCaptionLabel
         // 
         this.m_ControlAddressCaptionLabel.Size = new System.Drawing.Size(97,13);
         this.m_ControlAddressCaptionLabel.Text = "Queue Address:";
         this.m_ControlAddressCaptionLabel.Visible = true;
         // 
         // m_CopyButton
         // 
         this.m_CopyButton.Visible = true;
         // 
         // dirtyTimer
         // 
         dirtyTimer.Enabled = true;
         dirtyTimer.Interval = 250;
         dirtyTimer.Tick += new System.EventHandler(this.OnTimerTick);
         // 
         // QueueGroupBox
         // 
         QueueGroupBox.Controls.Add(this.m_MaxReadersTextBox);
         QueueGroupBox.Controls.Add(maxReadersLabel);
         QueueGroupBox.Controls.Add(this.m_ExpirationTimePicker);
         QueueGroupBox.Controls.Add(this.m_PurgeButton);
         QueueGroupBox.Controls.Add(renewLabel);
         QueueGroupBox.Controls.Add(dequeueLabel);
         QueueGroupBox.Controls.Add(this.m_DequeueRetriesTextBox);
         QueueGroupBox.Controls.Add(queueLengthLabel);
         QueueGroupBox.Controls.Add(overflowLabel);
         QueueGroupBox.Controls.Add(this.m_QueueLengthTextBox);
         QueueGroupBox.Controls.Add(this.m_OverflowComboBox);
         QueueGroupBox.Location = new System.Drawing.Point(10,110);
         QueueGroupBox.Name = "QueueGroupBox";
         QueueGroupBox.Size = new System.Drawing.Size(363,131);
         QueueGroupBox.TabIndex = 26;
         QueueGroupBox.TabStop = false;
         QueueGroupBox.Text = "Queue";
         // 
         // m_MaxReadersTextBox
         // 
         this.m_MaxReadersTextBox.Location = new System.Drawing.Point(137,41);
         this.m_MaxReadersTextBox.Name = "m_MaxReadersTextBox";
         this.m_MaxReadersTextBox.Size = new System.Drawing.Size(96,20);
         this.m_MaxReadersTextBox.TabIndex = 3;
         // 
         // maxReadersLabel
         // 
         maxReadersLabel.AutoSize = true;
         maxReadersLabel.Location = new System.Drawing.Point(134,25);
         maxReadersLabel.Name = "maxReadersLabel";
         maxReadersLabel.Size = new System.Drawing.Size(73,13);
         maxReadersLabel.TabIndex = 2;
         maxReadersLabel.Text = "Max Readers:";
         // 
         // m_ExpirationTimePicker
         // 
         this.m_ExpirationTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
         this.m_ExpirationTimePicker.Location = new System.Drawing.Point(246,40);
         this.m_ExpirationTimePicker.Name = "m_ExpirationTimePicker";
         this.m_ExpirationTimePicker.Size = new System.Drawing.Size(106,20);
         this.m_ExpirationTimePicker.TabIndex = 19;
         // 
         // m_PurgeButton
         // 
         this.m_PurgeButton.Location = new System.Drawing.Point(276,89);
         this.m_PurgeButton.Name = "m_PurgeButton";
         this.m_PurgeButton.Size = new System.Drawing.Size(76,23);
         this.m_PurgeButton.TabIndex = 11;
         this.m_PurgeButton.Text = "Purge All";
         this.m_PurgeButton.UseVisualStyleBackColor = true;
         this.m_PurgeButton.Click += new System.EventHandler(this.OnPurge);
         // 
         // renewLabel
         // 
         renewLabel.AutoSize = true;
         renewLabel.Location = new System.Drawing.Point(243,23);
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
         dequeueLabel.Size = new System.Drawing.Size(90,13);
         dequeueLabel.TabIndex = 15;
         dequeueLabel.Text = "Dequeue Retries:";
         // 
         // m_DequeueRetriesTextBox
         // 
         this.m_DequeueRetriesTextBox.Location = new System.Drawing.Point(11,41);
         this.m_DequeueRetriesTextBox.Name = "m_DequeueRetriesTextBox";
         this.m_DequeueRetriesTextBox.Size = new System.Drawing.Size(103,20);
         this.m_DequeueRetriesTextBox.TabIndex = 16;
         // 
         // queueLengthLabel
         // 
         queueLengthLabel.AutoSize = true;
         queueLengthLabel.Location = new System.Drawing.Point(8,73);
         queueLengthLabel.Name = "queueLengthLabel";
         queueLengthLabel.Size = new System.Drawing.Size(78,13);
         queueLengthLabel.TabIndex = 0;
         queueLengthLabel.Text = "Queue Length:";
         // 
         // overflowLabel
         // 
         overflowLabel.AutoSize = true;
         overflowLabel.Location = new System.Drawing.Point(133,73);
         overflowLabel.Name = "overflowLabel";
         overflowLabel.Size = new System.Drawing.Size(83,13);
         overflowLabel.TabIndex = 14;
         overflowLabel.Text = "Overflow Policy:";
         // 
         // m_QueueLengthTextBox
         // 
         this.m_QueueLengthTextBox.Location = new System.Drawing.Point(11,89);
         this.m_QueueLengthTextBox.Name = "m_QueueLengthTextBox";
         this.m_QueueLengthTextBox.Size = new System.Drawing.Size(103,20);
         this.m_QueueLengthTextBox.TabIndex = 1;
         // 
         // m_OverflowComboBox
         // 
         this.m_OverflowComboBox.FormattingEnabled = true;
         this.m_OverflowComboBox.Items.AddRange(new object[] {
            "Reject",
            "Discard Incoming",
            "Discard Existing"});
         this.m_OverflowComboBox.Location = new System.Drawing.Point(136,89);
         this.m_OverflowComboBox.Name = "m_OverflowComboBox";
         this.m_OverflowComboBox.Size = new System.Drawing.Size(97,21);
         this.m_OverflowComboBox.TabIndex = 13;
         // 
         // deleteButton
         // 
         deleteButton.Location = new System.Drawing.Point(172,302);
         deleteButton.Name = "deleteButton";
         deleteButton.Size = new System.Drawing.Size(75,23);
         deleteButton.TabIndex = 23;
         deleteButton.Text = "Delete";
         deleteButton.UseVisualStyleBackColor = true;
         deleteButton.Click += new System.EventHandler(this.OnDelete);
         // 
         // m_ResetButton
         // 
         this.m_ResetButton.Location = new System.Drawing.Point(91,302);
         this.m_ResetButton.Name = "m_ResetButton";
         this.m_ResetButton.Size = new System.Drawing.Size(75,23);
         this.m_ResetButton.TabIndex = 25;
         this.m_ResetButton.Text = "Reset";
         this.m_ResetButton.UseVisualStyleBackColor = true;
         this.m_ResetButton.Click += new System.EventHandler(this.OnReset);
         // 
         // m_UpdateButton
         // 
         this.m_UpdateButton.Enabled = false;
         this.m_UpdateButton.Location = new System.Drawing.Point(10,302);
         this.m_UpdateButton.Name = "m_UpdateButton";
         this.m_UpdateButton.Size = new System.Drawing.Size(75,23);
         this.m_UpdateButton.TabIndex = 24;
         this.m_UpdateButton.Text = "Update";
         this.m_UpdateButton.UseVisualStyleBackColor = true;
         this.m_UpdateButton.Click += new System.EventHandler(this.OnUpdate);
         // 
         // QueueViewControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
         this.BackColor = System.Drawing.SystemColors.Control;
         this.Controls.Add(QueueGroupBox);
         this.Controls.Add(this.m_ResetButton);
         this.Controls.Add(this.m_UpdateButton);
         this.Controls.Add(deleteButton);
         this.Name = "QueueViewControl";
         this.Controls.SetChildIndex(this.m_ControlAddressCaptionLabel,0);
         this.Controls.SetChildIndex(this.m_CopyButton,0);
         this.Controls.SetChildIndex(this.m_ItemNameLabel,0);
         this.Controls.SetChildIndex(this.m_AddressLabel,0);
         this.Controls.SetChildIndex(this.m_ControlPictureBox,0);
         this.Controls.SetChildIndex(deleteButton,0);
         this.Controls.SetChildIndex(this.m_UpdateButton,0);
         this.Controls.SetChildIndex(this.m_ResetButton,0);
         this.Controls.SetChildIndex(QueueGroupBox,0);
         ((System.ComponentModel.ISupportInitialize)(this.m_ControlPictureBox)).EndInit();
         QueueGroupBox.ResumeLayout(false);
         QueueGroupBox.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }
      #endregion

      private System.Windows.Forms.DateTimePicker m_ExpirationTimePicker;
      private System.Windows.Forms.TextBox m_DequeueRetriesTextBox;
      private System.Windows.Forms.ComboBox m_OverflowComboBox;
      private System.Windows.Forms.TextBox m_QueueLengthTextBox;
      private System.Windows.Forms.TextBox m_MaxReadersTextBox;
      private System.Windows.Forms.Button m_PurgeButton;
      private System.Windows.Forms.Button m_ResetButton;
      private System.Windows.Forms.Button m_UpdateButton;
   }
}
