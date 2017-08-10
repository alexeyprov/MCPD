using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using System.ServiceModel;

partial class MyClientForm : Form
{
   public MyClientForm()
   {
      InitializeComponent();
   }

   void OnCallService(object sender,EventArgs e)
   {   
      MyContractClient proxy = new MyContractClient();
      proxy.MyMethod();
      proxy.Close();
   }
}
