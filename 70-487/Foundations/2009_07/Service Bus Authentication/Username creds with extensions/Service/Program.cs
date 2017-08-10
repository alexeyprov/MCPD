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

      ServiceHost host = new ServiceHost(typeof(MyService));
      host.SetServiceBusPassword(loginDialog.Password);
      
      CallsCounterForm form = new CallsCounterForm();

      host.Open();

      Application.Run(form);
       
      host.Close();
   }
}
