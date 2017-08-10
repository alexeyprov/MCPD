//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System.ServiceModel;

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
