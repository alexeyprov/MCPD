// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using ServiceModelEx.ServiceBus;


partial class MyClientForm : Form
{
   public MyClientForm()
   {
      InitializeComponent();
   }

   void OnCall(object sender,EventArgs e)
   {
      MyContractClient proxy = new MyContractClient();
      proxy.SetServiceBusPassword("MyPassword");
      proxy.SetServiceCertificate("MyServiceCert");

      proxy.MyMethod();

      proxy.Close();
   }
}



