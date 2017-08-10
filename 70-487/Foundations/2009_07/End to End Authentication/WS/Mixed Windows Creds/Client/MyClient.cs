// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;

partial class MyClient : Form
{
   public MyClient()
   {
      InitializeComponent();
   }

   void OnCall(object sender,EventArgs e)
   {
      string solutionName = MySolution;
      string solutionPassword = "MyPassword";

      MyContractClient proxy = new MyContractClient();

      TransportClientEndpointBehavior userNamePasswordServiceBusCredential = new TransportClientEndpointBehavior();
      userNamePasswordServiceBusCredential.CredentialType = TransportClientCredentialType.UserNamePassword;
      userNamePasswordServiceBusCredential.Credentials.UserName.UserName = solutionName;
      userNamePasswordServiceBusCredential.Credentials.UserName.Password = solutionPassword;

      proxy.Endpoint.Behaviors.Add(userNamePasswordServiceBusCredential);
 
      proxy.MyMethod();

      proxy.Close();
   }
}



