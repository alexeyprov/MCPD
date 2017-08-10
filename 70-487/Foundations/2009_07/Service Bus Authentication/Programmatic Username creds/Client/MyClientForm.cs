using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;

partial class MyClientForm : Form
{
   MyContractClient m_Proxy;

   public MyClientForm(string solution,string password)
   {
      InitializeComponent();

      m_Proxy = new MyContractClient();

      TransportClientEndpointBehavior userNamePasswordServiceBusCredential = new TransportClientEndpointBehavior();
      userNamePasswordServiceBusCredential.CredentialType = TransportClientCredentialType.UserNamePassword;
      userNamePasswordServiceBusCredential.Credentials.UserName.UserName = solution;
      userNamePasswordServiceBusCredential.Credentials.UserName.Password = password;

            
      m_Proxy.Endpoint.Behaviors.Add(userNamePasswordServiceBusCredential);
   }

   void OnCallService(object sender,EventArgs e)
   {
      m_Proxy.MyMethod();
   }

   void OnClosed(object sender,FormClosedEventArgs e)
   {
      m_Proxy.Close();
   }
}
