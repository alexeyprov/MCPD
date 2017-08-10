//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System.ServiceModel;

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

partial class AccountClient : ClientBase<IAccount>,IAccount
{
   public AccountClient()
   {}

   public AccountClient(string configurationName) : base(configurationName)
   {}

   public void Credit(int accountNumber,decimal amount)
   {
      Channel.Credit(accountNumber,amount);
   }

   public void Debit(int accountNumber,decimal amount)
   {
     Channel.Debit(accountNumber,amount);
   }
}
