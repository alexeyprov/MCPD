//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ServiceModel;

[ServiceContract]
interface IMyEvents
{
   [OperationContract(IsOneWay = true)]
   void OnEvent1();

   [OperationContract(IsOneWay = true)]
   void OnEvent2(int number);

   [OperationContract(IsOneWay = true)]
   void OnEvent3(int number,string text);
}

partial class MySubscriberForm : Form,IMyEvents
{
   ServiceHost m_Host;

   public MySubscriberForm()
   {
      InitializeComponent();
   }
   public void OnEvent1()
   {
      MessageBox.Show("OnEvent1()","MySubscriber");
   }
   public void OnEvent2(int number)
   {
      MessageBox.Show("OnEvent2()","MySubscriber");
   }
   public void OnEvent3(int number,string text)
   {
      MessageBox.Show("OnEvent3()","MySubscriber");
   }

   void OnSubscribe(object sender,EventArgs e)
   {
      m_Host = new ServiceHost(typeof(MySubscriber));

      m_Host.Open();

      m_UnsubscribeButton.Enabled = true;
      m_SubscribeButtton.Enabled = false;
   }
   void OnUnsubscribe(object sender,EventArgs e)
   {
      m_Host.Close();

      m_UnsubscribeButton.Enabled = false;
      m_SubscribeButtton.Enabled = true;
   }
}