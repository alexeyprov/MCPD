namespace TransactionDemo
{
   partial class BankClientForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      System.ComponentModel.IContainer components = null;

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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.m_AccountsGrid = new System.Windows.Forms.DataGridView();
         this.m_AccountsBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.m_SourceBox = new System.Windows.Forms.TextBox();
         this.m_SourceLabel = new System.Windows.Forms.Label();
         this.m_TransferButton = new System.Windows.Forms.Button();
         this.m_PictureBox = new System.Windows.Forms.PictureBox();
         this.m_DestLabel = new System.Windows.Forms.Label();
         this.m_DestBox = new System.Windows.Forms.TextBox();
         this.m_AmountLabel = new System.Windows.Forms.Label();
         this.m_AmountBox = new System.Windows.Forms.TextBox();
         this.m_NumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.m_BalanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.m_NameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         ((System.ComponentModel.ISupportInitialize)(this.m_AccountsGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_AccountsBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).BeginInit();
         this.SuspendLayout();
         // 
         // m_AccountsGrid
         // 
         this.m_AccountsGrid.AutoGenerateColumns = false;
         this.m_AccountsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.m_NumberDataGridViewTextBoxColumn,
            this.m_BalanceDataGridViewTextBoxColumn,
            this.m_NameDataGridViewTextBoxColumn});
         this.m_AccountsGrid.DataSource = this.m_AccountsBindingSource;
         this.m_AccountsGrid.Location = new System.Drawing.Point(12,101);
         this.m_AccountsGrid.Name = "m_AccountsGrid";
         this.m_AccountsGrid.ReadOnly = true;
         this.m_AccountsGrid.Size = new System.Drawing.Size(343,128);
         this.m_AccountsGrid.TabIndex = 18;
         // 
         // m_AccountsBindingSource
         // 
         this.m_AccountsBindingSource.DataSource = typeof(Account[]);
         // 
         // m_SourceBox
         // 
         this.m_SourceBox.Location = new System.Drawing.Point(11,37);
         this.m_SourceBox.Name = "m_SourceBox";
         this.m_SourceBox.Size = new System.Drawing.Size(100,20);
         this.m_SourceBox.TabIndex = 14;
         this.m_SourceBox.Text = "123";
         // 
         // m_SourceLabel
         // 
         this.m_SourceLabel.Location = new System.Drawing.Point(11,13);
         this.m_SourceLabel.Name = "m_SourceLabel";
         this.m_SourceLabel.Size = new System.Drawing.Size(100,23);
         this.m_SourceLabel.TabIndex = 16;
         this.m_SourceLabel.Text = "Source Acount:";
         // 
         // m_TransferButton
         // 
         this.m_TransferButton.Location = new System.Drawing.Point(430,37);
         this.m_TransferButton.Name = "m_TransferButton";
         this.m_TransferButton.Size = new System.Drawing.Size(75,23);
         this.m_TransferButton.TabIndex = 11;
         this.m_TransferButton.Text = "Transfer";
         this.m_TransferButton.Click += new System.EventHandler(this.OnTransfer);
         // 
         // m_PictureBox
         // 
         this.m_PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.m_PictureBox.Image = global::TransactionDemo.Properties.Resources.Money;
         this.m_PictureBox.Location = new System.Drawing.Point(361,101);
         this.m_PictureBox.Name = "m_PictureBox";
         this.m_PictureBox.Size = new System.Drawing.Size(144,128);
         this.m_PictureBox.TabIndex = 10;
         this.m_PictureBox.TabStop = false;
         // 
         // m_DestLabel
         // 
         this.m_DestLabel.Location = new System.Drawing.Point(147,13);
         this.m_DestLabel.Name = "m_DestLabel";
         this.m_DestLabel.Size = new System.Drawing.Size(104,23);
         this.m_DestLabel.TabIndex = 17;
         this.m_DestLabel.Text = "Destination Acount:";
         // 
         // m_DestBox
         // 
         this.m_DestBox.Location = new System.Drawing.Point(147,37);
         this.m_DestBox.Name = "m_DestBox";
         this.m_DestBox.Size = new System.Drawing.Size(100,20);
         this.m_DestBox.TabIndex = 13;
         this.m_DestBox.Text = "456";
         // 
         // m_AmountLabel
         // 
         this.m_AmountLabel.Location = new System.Drawing.Point(275,13);
         this.m_AmountLabel.Name = "m_AmountLabel";
         this.m_AmountLabel.Size = new System.Drawing.Size(192,23);
         this.m_AmountLabel.TabIndex = 15;
         this.m_AmountLabel.Text = "Amount:";
         // 
         // m_AmountBox
         // 
         this.m_AmountBox.Location = new System.Drawing.Point(275,37);
         this.m_AmountBox.Name = "m_AmountBox";
         this.m_AmountBox.Size = new System.Drawing.Size(80,20);
         this.m_AmountBox.TabIndex = 12;
         this.m_AmountBox.Text = "100";
         // 
         // m_NumberDataGridViewTextBoxColumn
         // 
         this.m_NumberDataGridViewTextBoxColumn.DataPropertyName = "Number";
         this.m_NumberDataGridViewTextBoxColumn.HeaderText = "Number";
         this.m_NumberDataGridViewTextBoxColumn.Name = "m_NumberDataGridViewTextBoxColumn";
         this.m_NumberDataGridViewTextBoxColumn.ReadOnly = true;
         // 
         // m_BalanceDataGridViewTextBoxColumn
         // 
         this.m_BalanceDataGridViewTextBoxColumn.DataPropertyName = "Balance";
         this.m_BalanceDataGridViewTextBoxColumn.HeaderText = "Balance";
         this.m_BalanceDataGridViewTextBoxColumn.Name = "m_BalanceDataGridViewTextBoxColumn";
         this.m_BalanceDataGridViewTextBoxColumn.ReadOnly = true;
         // 
         // m_NameDataGridViewTextBoxColumn
         // 
         this.m_NameDataGridViewTextBoxColumn.DataPropertyName = "Name";
         this.m_NameDataGridViewTextBoxColumn.HeaderText = "Name";
         this.m_NameDataGridViewTextBoxColumn.Name = "m_NameDataGridViewTextBoxColumn";
         this.m_NameDataGridViewTextBoxColumn.ReadOnly = true;
         // 
         // BankClientForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(515,242);
         this.Controls.Add(this.m_AccountsGrid);
         this.Controls.Add(this.m_SourceBox);
         this.Controls.Add(this.m_SourceLabel);
         this.Controls.Add(this.m_TransferButton);
         this.Controls.Add(this.m_PictureBox);
         this.Controls.Add(this.m_DestLabel);
         this.Controls.Add(this.m_DestBox);
         this.Controls.Add(this.m_AmountLabel);
         this.Controls.Add(this.m_AmountBox);
         this.Name = "BankClientForm";
         this.Text = "Bank Client";
         this.Load += new System.EventHandler(this.OnFormLoad);
         ((System.ComponentModel.ISupportInitialize)(this.m_AccountsGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_AccountsBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      System.Windows.Forms.DataGridView m_AccountsGrid;
      System.Windows.Forms.TextBox m_SourceBox;
      System.Windows.Forms.Label m_SourceLabel;
      System.Windows.Forms.Button m_TransferButton;
      System.Windows.Forms.PictureBox m_PictureBox;
      System.Windows.Forms.Label m_DestLabel;
      System.Windows.Forms.TextBox m_DestBox;
      System.Windows.Forms.Label m_AmountLabel;
      System.Windows.Forms.TextBox m_AmountBox;
      System.Windows.Forms.BindingSource m_AccountsBindingSource;
      System.Windows.Forms.DataGridViewTextBoxColumn m_NumberDataGridViewTextBoxColumn;
      System.Windows.Forms.DataGridViewTextBoxColumn m_BalanceDataGridViewTextBoxColumn;
      System.Windows.Forms.DataGridViewTextBoxColumn m_NameDataGridViewTextBoxColumn;
   }
}