// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;

class Program
{
   static void Main()
   {
      TransportClientEndpointBehavior certCredential = new TransportClientEndpointBehavior();
      certCredential.CredentialType = TransportClientCredentialType.X509Certificate;
      certCredential.Credentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser,StoreName.My,X509FindType.FindBySubjectName,"MyServiceCert");

      ServiceHost host = new ServiceHost(typeof(MyService));

      foreach(ServiceEndpoint endpoint in host.Description.Endpoints)
      {
         endpoint.Behaviors.Add(certCredential);
      }

      host.Open();

      Application.Run(new HostForm());
      
      host.Close();      
   }
}
