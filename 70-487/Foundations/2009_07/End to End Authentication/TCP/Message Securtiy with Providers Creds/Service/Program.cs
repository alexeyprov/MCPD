// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using System.Web.Security;

class Program
{
   static void Main()
   {
      string solution = MySolution;
      string password = "MyPassword";

      TransportClientEndpointBehavior userNamePasswordCredential = new TransportClientEndpointBehavior();
      userNamePasswordCredential.CredentialType = TransportClientCredentialType.UserNamePassword;
      userNamePasswordCredential.Credentials.UserName.UserName = solution;
      userNamePasswordCredential.Credentials.UserName.Password = password;

      ServiceHost host = new ServiceHost(typeof(MyService));

      foreach(ServiceEndpoint endpoint in host.Description.Endpoints)
      {
         endpoint.Behaviors.Add(userNamePasswordCredential);
      }

      Membership.ApplicationName = "MyApplication";

      host.Open();

      Console.WriteLine("Press ENTER to shut down service.");
      Console.ReadLine();
      
      host.Close();
   }
}
