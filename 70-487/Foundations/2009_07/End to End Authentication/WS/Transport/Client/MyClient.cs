// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using ServiceModelEx.ServiceBus;


partial class MyClient : Form
{
   public MyClient()
   {
      InitializeComponent();
   }

   void OnCall(object sender,EventArgs e)
   {
      MyContractClient proxy = new MyContractClient();
      proxy.SetServiceBusPassword("MyPassword");
 
      proxy.MyMethod();

      proxy.Close();
   }
}



