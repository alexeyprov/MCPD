//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using Microsoft.ServiceBus;

partial class MyPublisherForm : Form
{
   public MyPublisherForm()
   {
      InitializeComponent();
   }
   void OnFireEvent(object sender,EventArgs e)
   {  
      if(m_Event1RadioButton.Checked)
      {         
         MyEventsProxy proxy = new MyEventsProxy("OnEvent1");
         proxy.OnEvent1();
         proxy.Close();
      }

      if(m_Event2RadioButton.Checked)
      {
         MyEventsProxy proxy = new MyEventsProxy("OnEvent2");
         proxy.OnEvent2(42);
         proxy.Close();
      }

      if(m_Event3RadioButton.Checked)
      {
         MyEventsProxy proxy = new MyEventsProxy("OnEvent3");
         proxy.OnEvent3(42,"Hello");
         proxy.Close();
      }
   }
}
