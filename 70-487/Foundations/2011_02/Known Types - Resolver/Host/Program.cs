//2011 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;

static class Program
{
   static void Main()
   {
      ServiceHost host = new ServiceHost(typeof(ContactManager));

      foreach(ServiceEndpoint endpoint in host.Description.Endpoints)
      {
         foreach(OperationDescription operation in endpoint.Contract.Operations)
         {
            DataContractSerializerOperationBehavior behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
            behavior.DataContractResolver = new CustomerResolver();
         }
      }
      host.Open();

      Application.Run(new HostForm());

      host.Close();
   }
}



