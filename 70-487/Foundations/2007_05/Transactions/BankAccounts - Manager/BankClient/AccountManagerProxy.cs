//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System.ServiceModel;
using System.Runtime.Serialization;


[DataContract]
class Account
{
   public string m_Name;
   public decimal m_Balance;
   public int m_Number;

   [DataMember]
   public string Name
   {
      get 
      { 
         return m_Name; 
      }
      set 
      { 
         m_Name = value; 
      }
   }
   [DataMember]
   public decimal Balance
   {
      get 
      { 
         return m_Balance; 
      }
      set 
      { 
         m_Balance = value; 
      }
   }
   [DataMember]
   public int Number
   {
      get 
      { 
         return m_Number; 
      }
      set 
      { 
         m_Number = value; 
      }
   }
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


class AccountManagerClient : ClientBase<IAccountManager>,IAccountManager
{
   public AccountManagerClient()
   {}

   public AccountManagerClient(string configurationName) : base(configurationName)
   {}

   public void Transfer(int sourceAccount,int destinationAccount,decimal amount)
   {
      Channel.Transfer(sourceAccount,destinationAccount,amount);
   }

   public Account[] GetAccounts()
   {
      return Channel.GetAccounts();
   }
}
