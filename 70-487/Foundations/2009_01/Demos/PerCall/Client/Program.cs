//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Transactions;


static class Program
{
   static void Main()
   {
      MyCounterClient proxy = new MyCounterClient();

      using(TransactionScope scope = new TransactionScope())
      {
         proxy.Increment("MyInstance");
         scope.Complete();
      }    

      //This transaction will abort since the scope is not completed 
      using(TransactionScope scope = new TransactionScope())
      {
         proxy.Increment("MyInstance");
      } 

      using(TransactionScope scope = new TransactionScope())
      {
         proxy.Increment("MyInstance");
         proxy.RemoveCounter("MyInstance");
         scope.Complete();
      }

      proxy.Close();
   }
}