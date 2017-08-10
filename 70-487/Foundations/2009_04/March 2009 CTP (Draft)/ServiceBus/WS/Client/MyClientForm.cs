//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;

partial class MyClientForm : Form
{
   MyContractClient m_Proxy;

   public MyClientForm()
   {
      InitializeComponent();
      m_Proxy = new MyContractClient();
   }

   void OnCall(object sender,EventArgs e)
   {
      m_Proxy.MyMethod();
   }

   void OnClosed(object sender,FormClosedEventArgs e)
   {
      m_Proxy.Close();
   }
}



