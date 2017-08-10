using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

using BusinessEntities;

namespace CustomBinding
{
    public partial class MainForm : Form
	{
		#region Construction

		public MainForm()
        {
            InitializeComponent();
		}

		#endregion

		#region Event Handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			bsBooks.DataSource = ObjectFactory.GetListOfBooks();
		}

		private void btnAddPage_Click(object sender, EventArgs e)
		{
			((BookInfo)bsBooks.Current).PageCount++;
		}

		private void btnRemovePage_Click(object sender, EventArgs e)
		{
			((BookInfo)bsBooks.Current).PageCount--;
		}

		#endregion
	}
}