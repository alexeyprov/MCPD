// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using ServiceModelEx.ServiceBus;

partial class MyClient : Form
{
   public MyClient()
   {
      InitializeComponent();
   }

   void OnCall(object sender,EventArgs e)
   {
      string solutionPassword = "MyPassword";

      MyContractClient proxy = new MyContractClient(m_UserNameTextbox.Text,m_PasswordTextBox.Text);
      proxy.SetServiceBusPassword(solutionPassword);
      proxy.SetServiceCertificate("MyServiceCert");

      proxy.MyMethod();

      proxy.Close();
   }
}



