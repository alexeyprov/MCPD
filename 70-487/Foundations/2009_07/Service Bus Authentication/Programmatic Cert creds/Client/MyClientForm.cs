using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using System.Security.Cryptography.X509Certificates;

partial class MyClientForm : Form
{
   public MyClientForm()
   {
      InitializeComponent();
   }

   void OnCallService(object sender,EventArgs e)
   {      
      MyContractClient proxy = new MyContractClient();

      TransportClientEndpointBehavior certCredential = new TransportClientEndpointBehavior();
      certCredential.CredentialType = TransportClientCredentialType.X509Certificate;
      certCredential.Credentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser,StoreName.My,X509FindType.FindBySubjectName,"MyServiceCert");

      proxy.Endpoint.Behaviors.Add(certCredential);

      proxy.MyMethod();

      proxy.Close();
   }
}
