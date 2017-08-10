//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.ServiceModel;
using System.Runtime.Serialization;
using TransactionsDemo.BankDataSetTableAdapters;
using TransactionsDemo;

[ServiceContract]
interface IAccount
{
   [OperationContract]
   [TransactionFlow(TransactionFlowOption.Mandatory)]
	void Credit(int accountNumber,decimal amount);

   [OperationContract]
   [TransactionFlow(TransactionFlowOption.Mandatory)]
   void Debit(int accountNumber,decimal amount);
}
[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)] 
class AccountService : IAccount
{
   [OperationBehavior(TransactionScopeRequired = true)]
	public void Credit(int accountNumber,decimal amount)
	{
      BankAccountsTableAdapter adapter = new BankAccountsTableAdapter();
      BankDataSet.BankAccountsDataTable accounts = adapter.GetData();

      BankDataSet.BankAccountsRow account = accounts.FindByNumber(accountNumber);
      account.Balance += amount;
      adapter.Update(accounts);
   }
   [OperationBehavior(TransactionScopeRequired = true)]
	public void Debit(int accountNumber,decimal amount)
	{
      BankAccountsTableAdapter adapter = new BankAccountsTableAdapter();
      BankDataSet.BankAccountsDataTable accounts = adapter.GetData();

      BankDataSet.BankAccountsRow account = accounts.FindByNumber(accountNumber);

      if(account.Balance >= amount)
      {
         account.Balance -= amount;
      }
      else
      {
         throw new InvalidOperationException("Debit amount is greater than balance in account #" + accountNumber);

      }
      adapter.Update(accounts);
   }
}
