//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Transactions;
using System.ServiceModel;
using System.Runtime.Serialization;
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
      Bank.Credit(accountNumber,amount);
   }
   [OperationBehavior(TransactionScopeRequired = true)]
	public void Debit(int accountNumber,decimal amount)
	{
      Account account = Bank.GetAccount(accountNumber);
      if(account.Balance >= amount)
      {
         account.Balance -= amount;
      }
      else
      {
         throw new InvalidOperationException("Debit amount is greater than balance in account #" + accountNumber);
      }
   }
}
