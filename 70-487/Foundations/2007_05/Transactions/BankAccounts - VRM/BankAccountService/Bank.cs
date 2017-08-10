//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using ServiceModelEx.Transactional;
using System.Runtime.Serialization;


[Serializable]
class Account
{
   public Account()
   {}
   public Account(int number,string name,decimal balance)
   {
      Number = number;
      Name = name;
      Balance = balance;
   }
   public string Name;
   public decimal Balance;
   public int Number;
}

static class Bank
{
   static TransactionalDictionary<int,Account> m_Accounts = new TransactionalDictionary<int,Account>();
   static Bank()
   {
      Account account1 = new Account(123,"Juval",1000);
      Account account2 = new Account(456,"Brian",1000);

      m_Accounts.Add(123,account1);
      m_Accounts.Add(456,account2);
   }
   public static Account GetAccount(int number)
   {
      return m_Accounts[number];
   }
   public static Account[] GetAccounts()
   {
      return Collection.ToArray(m_Accounts.Values);
   }
   public static void Credit(int number,decimal ammount)
   {
      m_Accounts[number].Balance += ammount;
   }
   public static void Debit(int number,decimal ammount)
   {
      m_Accounts[number].Balance -= ammount;
   }
}
