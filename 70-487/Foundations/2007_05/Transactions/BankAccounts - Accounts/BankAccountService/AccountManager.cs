//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.ServiceModel;
using System.Runtime.Serialization;
using ServiceModelEx;
using TransactionsDemo.BankDataSetTableAdapters;
using TransactionsDemo;


[DataContract]
class Account
{
   [DataMember]
   public string Name;

   [DataMember]
   public decimal Balance;

   [DataMember]
   public int Number;
}

[ServiceContract]
interface IAccountManager
{
   [OperationContract]
   [TransactionFlow(TransactionFlowOption.Allowed)]
	void Transfer(int sourceAccount,int destinationAccount,decimal amount);

   [OperationContract]
   [TransactionFlow(TransactionFlowOption.Allowed)]
   Account[] GetAccounts();
}
[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)] 
class AccountManager : IAccountManager
{
   [OperationBehavior(TransactionScopeRequired = true)]
	public void Transfer(int sourceAccount,int destinationAccount,decimal amount)
	{
      AccountClient account1 = new AccountClient();
      AccountClient account2 = new AccountClient();

      using(account1)
      using(account2)
      {
         account1.Credit(destinationAccount,amount);
         account2.Debit(sourceAccount,amount);
      }
   }
   public Account[] GetAccounts()
   {
      BankAccountsTableAdapter adapter = new BankAccountsTableAdapter();
      BankDataSet.BankAccountsDataTable accounts = adapter.GetData();
      Converter<BankDataSet.BankAccountsRow,Account> converter =  delegate(BankDataSet.BankAccountsRow row)
                                                                  {
                                                                     Account account = new Account();
                                                                     account.Number = row.Number;
                                                                     account.Name = row.Name;
                                                                     account.Balance = row.Balance;
                                                                     return account;
                                                                  };
      return DataTableHelper.ToArray(accounts,converter);
   }
}
