//2011 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using MyNamespace;
using System.ServiceModel.Description;

partial class MyClient : Form
{
   public MyClient()
   {
      InitializeComponent();  
   }

   void OnCall(object sender,EventArgs e)
   {
      ContactManagerClient proxy = new ContactManagerClient();

      foreach(OperationDescription operation in proxy.Endpoint.Contract.Operations)
      {
         DataContractSerializerOperationBehavior behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
         behavior.DataContractResolver = new CustomerResolver();
      }

      Customer customer = new Customer();
      customer.FirstName = "Juval";
      customer.LastName = "Lowy";
      customer.CustomerNumber = 123;

      proxy.AddContact(customer);
      proxy.AddContact(customer);
      proxy.AddContact(customer);

      Contact[] contacts = proxy.GetContacts();

      proxy.Close();
   }
}



