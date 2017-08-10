using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;
using System.ServiceModel.Channels;

partial class MyClientForm : Form
{
   MyContractClient m_Proxy;
   int m_Counter = 1;

   public MyClientForm()
   {
      InitializeComponent();

      m_Proxy = new MyContractClient("**********  Enter Your Secret Here  **********");
   }

   void OnCallService(object sender,EventArgs e)
   {
      m_Proxy.MyMethod(m_Counter++);
   }

   void OnClosed(object sender,FormClosedEventArgs e)
   {
      m_Proxy.Close();
   }
}
