using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;
using System.ServiceModel.Channels;

partial class MyClientForm : Form
{
   MyContractClient m_Proxy;

   public MyClientForm()
   {
      InitializeComponent();

      m_Proxy = new MyContractClient();
   }

   void OnCallService(object sender,EventArgs e)
   {
      m_Proxy.MyMethod();
   }

   void OnClosed(object sender,FormClosedEventArgs e)
   {
      m_Proxy.Close();
   }
}
