using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using ServiceModelEx.ServiceBus;

partial class MyClientForm : Form
{
   MyContractClient m_Proxy;

   public MyClientForm(string solution,string password)
   {
      InitializeComponent();

      m_Proxy = new MyContractClient();

      m_Proxy.SetServiceBusPassword(solution,password);
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
