//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Transactions;

namespace Client
{
   static class Program
   {
      static void Main()
      {
         MyCounterClient proxy = new MyCounterClient();

         using(TransactionScope scope = new TransactionScope())
         {
            proxy.Increment();
            scope.Complete();
         }    

         //This transaction will abort since the scope is not completed 
         using(TransactionScope scope = new TransactionScope())
         {
            proxy.Increment();
         } 

         using(TransactionScope scope = new TransactionScope())
         {
            proxy.Increment();
            proxy.RemoveCounter();
            scope.Complete();
         }

         proxy.Close();
      }
   }
}