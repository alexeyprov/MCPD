// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using ServiceModelEx.ServiceBus;

partial class MyClientForm : Form
{
   public MyClientForm()
   {
      InitializeComponent();
   }

   void OnCall(object sender,EventArgs e)
   {
      string solutionPassword = "MyPassword";

      MyContractClient proxy = new MyContractClient();
      proxy.SetServiceBusPassword(solutionPassword);

      proxy.MyMethod();

      proxy.Close();
   }
}



