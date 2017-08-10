//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Transactions;
using System.ServiceModel;
using System.Runtime.Serialization;
using ServiceModelEx;
using TransactionsDemo;

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
      Bank.Credit(destinationAccount,amount);
      Bank.Debit(sourceAccount,amount);
   }
   public Account[] GetAccounts()
   {
      return Bank.GetAccounts();
   }
}
