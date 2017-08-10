// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Windows.Forms;
using ServiceModelEx.ServiceBus;


class Program
{
   static void Main(string[] args)
   {
      string solution = ServiceBusHelper.ExtractSolutionFromConfig("MyEndpoint");

      LoginForm loginDialog = new LoginForm(solution);
      loginDialog.ShowDialog();

      MyClientForm form = new MyClientForm(solution,loginDialog.Password);

      Application.Run(form);
   }
}
