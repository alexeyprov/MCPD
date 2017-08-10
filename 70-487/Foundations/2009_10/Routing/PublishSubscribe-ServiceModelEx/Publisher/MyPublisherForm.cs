//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using ServiceModelEx.ServiceBus;

partial class MyPublisherForm : Form
{
   MyEventsProxy m_Proxy;

   public MyPublisherForm()
   {
      InitializeComponent();


      //Can encapsulate in the constructor of MyEventsProxy as well:
      string password = "MyPassword";
      string serviceCertificate = "MyServiceCert";
      string baseAddress = "sb://MySolution.servicebus.windows.net/";

      m_Proxy = new MyEventsProxy(baseAddress);         
      m_Proxy.SetServiceBusPassword(password);
      m_Proxy.SetServiceCertificate(serviceCertificate);
   }
   void OnFireEvent(object sender,EventArgs e)
   {
      if(m_Event1RadioButton.Checked)
      {
         m_Proxy.OnEvent1();
      }

      if(m_Event2RadioButton.Checked)
      {
         m_Proxy.OnEvent2(42);
      }

      if(m_Event3RadioButton.Checked)
      {
         m_Proxy.OnEvent3(42,"Hello");
      }
   }

   void OnClosed(object sender,FormClosedEventArgs e)
   {
      m_Proxy.Close();
   }
}
