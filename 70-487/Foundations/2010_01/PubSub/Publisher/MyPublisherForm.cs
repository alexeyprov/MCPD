// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;
using ServiceModelEx;

partial class MyPublisherForm : Form
{
   IMyEvents m_PublishService;

   public MyPublisherForm()
   {
      InitializeComponent();

      m_PublishService = DiscoveryPublishService<IMyEvents>.CreateChannel();
   }
   void OnFireEvent(object sender,EventArgs e)
   {
      if(m_Event1RadioButton.Checked)
      {
         m_PublishService.OnEvent1();
      }

      if(m_Event2RadioButton.Checked)
      {
         m_PublishService.OnEvent2(42);
      }

      if(m_Event3RadioButton.Checked)
      {
         m_PublishService.OnEvent3(42,"Hello");
      }
   }

   void OnFormClosed(object sender,FormClosedEventArgs e)
   {
      (m_PublishService as ICommunicationObject).Close();
   }
}