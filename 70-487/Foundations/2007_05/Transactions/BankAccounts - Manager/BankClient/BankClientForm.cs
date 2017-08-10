using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using System.Threading;

namespace TransactionDemo
{
   public partial class BankClientForm : Form
   {
      public BankClientForm()
      {
         InitializeComponent();
         Thread.Sleep(3000);
      }
      void OnTransfer(object sender,EventArgs e)
      {
         int sourceAccount;
         int destinationAccount;
         decimal amount;

         destinationAccount = Convert.ToInt32(m_DestBox.Text);
         sourceAccount = Convert.ToInt32(m_SourceBox.Text);
         amount = Convert.ToDecimal(m_AmountBox.Text);

         try
         {
            using(AccountManagerClient accountManager = new AccountManagerClient())
            using(TransactionScope scope = new TransactionScope())
            {
               accountManager.Transfer(sourceAccount,destinationAccount,amount);
               scope.Complete();
            }
         }
         catch(Exception exception)
         {
            MessageBox.Show("Some error occurred: " + exception.Message,"Bank Client");
         }
         finally
         {
            RefreshGrid();
         }
      }
      void RefreshGrid()
      {
         AccountManagerClient accountManager = new AccountManagerClient();
         m_AccountsBindingSource.DataSource = accountManager.GetAccounts();
         accountManager.Close();
      }
      void OnFormLoad(object sender,EventArgs e)
      {
         RefreshGrid();
      }
   }
}