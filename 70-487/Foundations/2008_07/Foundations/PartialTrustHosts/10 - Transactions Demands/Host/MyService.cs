//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Transactions;

[ServiceContract]
interface IMyContract
{
   [OperationContract]
   [TransactionFlow(TransactionFlowOption.Mandatory)]
   void MyMethod();
}

class MyService : IMyContract
{
   [OperationBehavior(TransactionScopeRequired = true)]
   public void MyMethod()
   {
      //Transaction had to come from client
      Debug.Assert(Transaction.Current.TransactionInformation.DistributedIdentifier != Guid.Empty);

      Trace.WriteLine("MyService.MyMethod()");
   }
}
