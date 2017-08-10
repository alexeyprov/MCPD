// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using System.Windows.Forms;
using ServiceModelEx.ServiceBus;

class Program
{
   static void Main()
   {
      string solution = ServiceBusHelper.ExtractSolutionFromConfig(typeof(MyService));

      LoginForm loginDialog = new LoginForm(solution);
      loginDialog.ShowDialog();


      TransportClientEndpointBehavior behavior = new TransportClientEndpointBehavior();
      behavior.CredentialType = TransportClientCredentialType.UserNamePassword;
      behavior.Credentials.UserName.UserName = MySolution;
      behavior.Credentials.UserName.Password = loginDialog.Password;

      ServiceHost host = new ServiceHost(typeof(MyService));

      foreach(ServiceEndpoint endpoint in host.Description.Endpoints)
      {
         endpoint.Behaviors.Add(behavior);
      }


      CallsCounterForm form = new CallsCounterForm();

      host.Open();

      Application.Run(form);
       
      host.Close();
   }
}
