using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Transactions;

namespace TransactionDemo
{
   public partial class BankClientForm : Form
   {
      public BankClientForm()
      {
         InitializeComponent();
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
            using(AccountClient account1 = new AccountClient("TCP"))
            using(AccountClient account2 = new AccountClient("HTTP"))
            using(TransactionScope scope = new TransactionScope())
            {
               account1.Credit(destinationAccount,amount);
               account2.Debit(sourceAccount,amount);
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