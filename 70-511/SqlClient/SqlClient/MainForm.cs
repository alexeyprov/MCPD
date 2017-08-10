using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SqlClient
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void m_CUSTOMERSBindingNavigatorSaveItem_Click(object sender, EventArgs e)
		{
			this.Validate();
			this.bsCustomers.EndEdit();
			this.tableAdapterManager.UpdateAll(this.dsSampleDatabase);

		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// TODO: This line of code loads data into the 'sampleDatabaseDataSet.Customers' table. You can move, or remove it, as needed.
			this.customersTableAdapter.Fill(this.dsSampleDatabase.Customers);

		}
	}
}
