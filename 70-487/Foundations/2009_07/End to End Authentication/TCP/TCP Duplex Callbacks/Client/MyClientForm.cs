using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;

[CallbackBehavior(UseSynchronizationContext = false)]
partial class MyClientForm : Form,IMyContractCallback
{
   MyContractClient m_Proxy;

   public MyClientForm()
   {
      InitializeComponent();
      m_Proxy = new MyContractClient(this);
      m_Proxy.SetServiceBusPassword("MyPassword");
   }

   void OnCallService(object sender,EventArgs e)
   {
      m_Proxy.MyMethod();
   }

   void OnClosed(object sender,FormClosedEventArgs e)
   {
      m_Proxy.Close();
   }

   public void OnCallback()
   {
      MessageBox.Show("OnCallback()","MyClient");
   }
}
