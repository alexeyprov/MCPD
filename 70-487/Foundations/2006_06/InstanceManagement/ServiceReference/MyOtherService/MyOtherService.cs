//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyNamespace
{
   [ServiceContract]
   interface ISomeContract
   {
      [OperationContract]
      void PassReference(EndpointAddress10 serviceAddress);
   }
   class MyOtherService : ISomeContract
   {
      public MyOtherService()
      {
         MessageBox.Show("MyOtherService()","MyOtherService");
      }
      public void PassReference(EndpointAddress10 serviceAddress)
      {
         MessageBox.Show("SomeMethod()","MyOtherService");

         MyContractProxy proxy = new MyContractProxy();
         proxy.Endpoint.Address = serviceAddress.ToEndpointAddress();
         proxy.MyMethod();
         proxy.Close();
      }
   }
}
