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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Label lblTitle;
			System.Windows.Forms.Label lblAuthor;
			System.Windows.Forms.Label lblISBN;
			System.Windows.Forms.Label lblPageCount;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.txtAuthor = new System.Windows.Forms.TextBox();
			this.txtIsbn = new System.Windows.Forms.TextBox();
			this.txtPageCount = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.navBooks = new System.Windows.Forms.BindingNavigator(this.components);
			this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
			this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
			this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
			this.grdBooks = new System.Windows.Forms.DataGridView();
			this.bsBooks = new System.Windows.Forms.BindingSource(this.components);
			lblTitle = new System.Windows.Forms.Label();
			lblAuthor = new System.Windows.Forms.Label();
			lblISBN = new System.Windows.Forms.Label();
			lblPageCount = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.navBooks)).BeginInit();
			this.navBooks.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.grdBooks)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.bsBooks)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Location = new System.Drawing.Point(17, 27);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(30, 13);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "&Title:";
			// 
			// lblAuthor
			// 
			lblAuthor.AutoSize = true;
			lblAuthor.Location = new System.Drawing.Point(17, 53);
			lblAuthor.Name = "lblAuthor";
			lblAuthor.Size = new System.Drawing.Size(41, 13);
			lblAuthor.TabIndex = 0;
			lblAuthor.Text = "&Author:";
			// 
			// lblISBN
			// 
			lblISBN.AutoSize = true;
			lblISBN.Location = new System.Drawing.Point(17, 79);
			lblISBN.Name = "lblISBN";
			lblISBN.Size = new System.Drawing.Size(35, 13);
			lblISBN.TabIndex = 0;
			lblISBN.Text = "&ISBN:";
			// 
			// lblPageCount
			// 
			lblPageCount.AutoSize = true;
			lblPageCount.Location = new System.Drawing.Point(17, 105);
			lblPageCount.Name = "lblPageCount";
			lblPageCount.Size = new System.Drawing.Size(65, 13);
			lblPageCount.TabIndex = 0;
			lblPageCount.Text = "&Page count:";
			// 
			// txtTitle
			// 
			this.txtTitle.Location = new System.Drawing.Point(88, 24);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(329, 20);
			this.txtTitle.TabIndex = 1;
			// 
			// txtAuthor
			// 
			this.txtAuthor.Location = new System.Drawing.Point(88, 50);
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.Size = new System.Drawing.Size(329, 20);
			this.txtAuthor.TabIndex = 1;
			// 
			// txtIsbn
			// 
			this.txtIsbn.Location = new System.Drawing.Point(88, 76);
			this.txtIsbn.Name = "txtIsbn";
			this.txtIsbn.Size = new System.Drawing.Size(329, 20);
			this.txtIsbn.TabIndex = 1;
			// 
			// txtPageCount
			// 
			this.txtPageCount.Location = new System.Drawing.Point(88, 102);
			this.txtPageCount.Name = "txtPageCount";
			this.txtPageCount.Size = new System.Drawing.Size(329, 20);
			this.txtPageCount.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtIsbn);
			this.groupBox1.Controls.Add(lblTitle);
			this.groupBox1.Controls.Add(this.txtTitle);
			this.groupBox1.Controls.Add(lblAuthor);
			this.groupBox1.Controls.Add(this.txtPageCount);
			this.groupBox1.Controls.Add(this.txtAuthor);
			this.groupBox1.Controls.Add(lblPageCount);
			this.groupBox1.Controls.Add(lblISBN);
			this.groupBox1.Location = new System.Drawing.Point(12, 192);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(450, 135);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Current Book";
			// 
			// navBooks
			// 
			this.navBooks.AddNewItem = this.bindingNavigatorAddNewItem;
			this.navBooks.BindingSource = this.bsBooks;
			this.navBooks.CountItem = this.bindingNavigatorCountItem;
			this.navBooks.DeleteItem = this.bindingNavigatorDeleteItem;
			this.navBooks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
			this.navBooks.Location = new System.Drawing.Point(0, 0);
			this.navBooks.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
			this.navBooks.MoveLastItem = this.bindingNavigatorMoveLastItem;
			this.navBooks.MoveNextItem = this.bindingNavigatorMoveNextItem;
			this.navBooks.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
			this.navBooks.Name = "navBooks";
			this.navBooks.PositionItem = this.bindingNavigatorPositionItem;
			this.navBooks.Size = new System.Drawing.Size(474, 25);
			this.navBooks.TabIndex = 5;
			// 
			// bindingNavigatorMoveFirstItem
			// 
			this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image) (resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
			this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
			this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveFirstItem.Text = "Move first";
			// 
			// bindingNavigatorMovePreviousItem
			// 
			this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image) (resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
			this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
			this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMovePreviousItem.Text = "Move previous";
			// 
			// bindingNavigatorSeparator
			// 
			this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
			this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorPositionItem
			// 
			this.bindingNavigatorPositionItem.AccessibleName = "Position";
			this.bindingNavigatorPositionItem.AutoSize = false;
			this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
			this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
			this.bindingNavigatorPositionItem.Text = "0";
			this.bindingNavigatorPositionItem.ToolTipText = "Current position";
			// 
			// bindingNavigatorCountItem
			// 
			this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
			this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
			this.bindingNavigatorCountItem.Text = "of {0}";
			this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
			// 
			// bindingNavigatorSeparator1
			// 
			this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
			this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorMoveNextItem
			// 
			this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image) (resources.GetObject("bindingNavigatorMoveNextItem.Image")));
			this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
			this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveNextItem.Text = "Move next";
			// 
			// bindingNavigatorMoveLastItem
			// 
			this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image) (resources.GetObject("bindingNavigatorMoveLastItem.Image")));
			this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
			this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveLastItem.Text = "Move last";
			// 
			// bindingNavigatorSeparator2
			// 
			this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
			this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorAddNewItem
			// 
			this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image) (resources.GetObject("bindingNavigatorAddNewItem.Image")));
			this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
			this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorAddNewItem.Text = "Add new";
			// 
			// bindingNavigatorDeleteItem
			// 
			this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image) (resources.GetObject("bindingNavigatorDeleteItem.Image")));
			this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
			this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorDeleteItem.Text = "Delete";
			// 
			// grdBooks
			// 
			this.grdBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdBooks.Location = new System.Drawing.Point(13, 29);
			this.grdBooks.Name = "grdBooks";
			this.grdBooks.Size = new System.Drawing.Size(449, 157);
			this.grdBooks.TabIndex = 6;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 339);
			this.Controls.Add(this.grdBooks);
			this.Controls.Add(this.navBooks);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "Book Info";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.navBooks)).EndInit();
			this.navBooks.ResumeLayout(false);
			this.navBooks.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.grdBooks)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.bsBooks)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtIsbn;
		private System.Windows.Forms.TextBox txtPageCount;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.BindingNavigator navBooks;
		private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
		private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
		private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
		private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
		private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
		private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
		private System.Windows.Forms.DataGridView grdBooks;
		private System.Windows.Forms.BindingSource bsBooks;
    }
}

