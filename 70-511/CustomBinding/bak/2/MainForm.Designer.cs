namespace CustomBinding
{
    partial class MainForm
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
            if (disposing && (components != null))
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
			System.Windows.Forms.Label lblTitle;
			System.Windows.Forms.Label lblAuthor;
			System.Windows.Forms.Label lblISBN;
			System.Windows.Forms.Label lblPageCount;
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.txtAuthor = new System.Windows.Forms.TextBox();
			this.txtIsbn = new System.Windows.Forms.TextBox();
			this.txtPageCount = new System.Windows.Forms.TextBox();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnForward = new System.Windows.Forms.Button();
			this.lblBookNo = new System.Windows.Forms.Label();
			lblTitle = new System.Windows.Forms.Label();
			lblAuthor = new System.Windows.Forms.Label();
			lblISBN = new System.Windows.Forms.Label();
			lblPageCount = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Location = new System.Drawing.Point(12, 15);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(30, 13);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "&Title:";
			// 
			// txtTitle
			// 
			this.txtTitle.Location = new System.Drawing.Point(83, 12);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(197, 20);
			this.txtTitle.TabIndex = 1;
			// 
			// lblAuthor
			// 
			lblAuthor.AutoSize = true;
			lblAuthor.Location = new System.Drawing.Point(12, 41);
			lblAuthor.Name = "lblAuthor";
			lblAuthor.Size = new System.Drawing.Size(41, 13);
			lblAuthor.TabIndex = 0;
			lblAuthor.Text = "&Author:";
			// 
			// txtAuthor
			// 
			this.txtAuthor.Location = new System.Drawing.Point(83, 38);
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.Size = new System.Drawing.Size(197, 20);
			this.txtAuthor.TabIndex = 1;
			// 
			// lblISBN
			// 
			lblISBN.AutoSize = true;
			lblISBN.Location = new System.Drawing.Point(12, 67);
			lblISBN.Name = "lblISBN";
			lblISBN.Size = new System.Drawing.Size(35, 13);
			lblISBN.TabIndex = 0;
			lblISBN.Text = "&ISBN:";
			// 
			// txtIsbn
			// 
			this.txtIsbn.Location = new System.Drawing.Point(83, 64);
			this.txtIsbn.Name = "txtIsbn";
			this.txtIsbn.Size = new System.Drawing.Size(197, 20);
			this.txtIsbn.TabIndex = 1;
			// 
			// lblPageCount
			// 
			lblPageCount.AutoSize = true;
			lblPageCount.Location = new System.Drawing.Point(12, 93);
			lblPageCount.Name = "lblPageCount";
			lblPageCount.Size = new System.Drawing.Size(65, 13);
			lblPageCount.TabIndex = 0;
			lblPageCount.Text = "&Page count:";
			// 
			// txtPageCount
			// 
			this.txtPageCount.Location = new System.Drawing.Point(83, 90);
			this.txtPageCount.Name = "txtPageCount";
			this.txtPageCount.Size = new System.Drawing.Size(197, 20);
			this.txtPageCount.TabIndex = 1;
			// 
			// btnBack
			// 
			this.btnBack.Location = new System.Drawing.Point(12, 118);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(27, 23);
			this.btnBack.TabIndex = 2;
			this.btnBack.Text = "<";
			this.btnBack.UseVisualStyleBackColor = true;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnForward
			// 
			this.btnForward.Location = new System.Drawing.Point(253, 118);
			this.btnForward.Name = "btnForward";
			this.btnForward.Size = new System.Drawing.Size(27, 23);
			this.btnForward.TabIndex = 2;
			this.btnForward.Text = ">";
			this.btnForward.UseVisualStyleBackColor = true;
			this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
			// 
			// lblBookNo
			// 
			this.lblBookNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.lblBookNo.Location = new System.Drawing.Point(45, 123);
			this.lblBookNo.Name = "lblBookNo";
			this.lblBookNo.Size = new System.Drawing.Size(202, 18);
			this.lblBookNo.TabIndex = 3;
			this.lblBookNo.Text = "Book number N";
			this.lblBookNo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 153);
			this.Controls.Add(this.lblBookNo);
			this.Controls.Add(this.btnForward);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.txtPageCount);
			this.Controls.Add(lblPageCount);
			this.Controls.Add(this.txtIsbn);
			this.Controls.Add(lblISBN);
			this.Controls.Add(this.txtAuthor);
			this.Controls.Add(lblAuthor);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(lblTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "Book Info";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtIsbn;
        private System.Windows.Forms.TextBox txtPageCount;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Button btnForward;
		private System.Windows.Forms.Label lblBookNo;
    }
}

