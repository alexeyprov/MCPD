//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Transactions;
using ServiceModelEx;

static class Program
{
   static void Main()
   {
      IMyCounter proxy = InProcFactory.CreateInstance<MyService,IMyCounter>();

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
         scope.Complete();
      }

      InProcFactory.CloseProxy(proxy);}
}