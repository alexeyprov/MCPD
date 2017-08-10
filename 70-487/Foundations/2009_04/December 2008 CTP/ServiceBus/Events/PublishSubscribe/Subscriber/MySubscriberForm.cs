//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ServiceModel;
using Microsoft.ServiceBus;


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
   enum EventType
   {
      Event1 = 1,
      Event2 = 2,
      Event3 = 4,
      AllEvents = Event1|Event2|Event3
   }

   ServiceHost m_HostEvent1;
   ServiceHost m_HostEvent2;
   ServiceHost m_HostEvent3;

   public MySubscriberForm()
   {
      InitializeComponent();
   }

   EventType GetSelectedEvent()
   {
      if(m_Event1RadioButton.Checked)
      {
         return EventType.Event1;
      }
      if(m_Event2RadioButton.Checked)
      {
         return EventType.Event2;
      }
      if(m_Event3RadioButton.Checked)
      {
         return EventType.Event3;
      }
      if(m_AllEventsRadioButton.Checked)
      {
         return EventType.AllEvents;
      }
      throw new InvalidOperationException();
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
   void OnClosing(object sender,FormClosingEventArgs e)
   {
      m_AllEventsRadioButton.Checked = true;
      OnUnsubscribe(this,EventArgs.Empty);
   }
   void OnSubscribe(object sender,EventArgs e)
   {
      EventType eventToSubscribe = GetSelectedEvent();

      NetEventRelayBinding binding = new NetEventRelayBinding();
      
      Uri baseAddress = new Uri("sb://MySolution/servicebus.windows.net/IMyEvents/");

      if((eventToSubscribe & EventType.Event1) == EventType.Event1)
      {
         if(m_HostEvent1 == null)
         {
            m_HostEvent1 = new ServiceHost(typeof(MySubscriber),baseAddress);
            m_HostEvent1.AddServiceEndpoint(typeof(IMyEvents),binding,"OnEvent1");
            m_HostEvent1.Open();
         }
      }
      if((eventToSubscribe & EventType.Event2) == EventType.Event2)
      {
         if(m_HostEvent2 == null)
         {
            m_HostEvent2 = new ServiceHost(typeof(MySubscriber),baseAddress);
            m_HostEvent2.AddServiceEndpoint(typeof(IMyEvents),binding,"OnEvent2");
            m_HostEvent2.Open();
         }
      }
      if((eventToSubscribe & EventType.Event3) == EventType.Event3)
      {
         if(m_HostEvent3 == null)
         {
            m_HostEvent3 = new ServiceHost(typeof(MySubscriber),baseAddress);
            m_HostEvent3.AddServiceEndpoint(typeof(IMyEvents),binding,"OnEvent3");
            m_HostEvent3.Open();
         }
      }
   }
   void OnUnsubscribe(object sender,EventArgs e)
   {
      EventType eventToSubscribe = GetSelectedEvent();
      
      if((eventToSubscribe & EventType.Event1) == EventType.Event1)
      {
         if(m_HostEvent1 != null)
         {
            m_HostEvent1.Close();
            m_HostEvent1 = null;
         }
      }
      if((eventToSubscribe & EventType.Event2) == EventType.Event2)
      {
         if(m_HostEvent2 != null)
         {
            m_HostEvent2.Close();
            m_HostEvent2 = null;
         }
      }
      if((eventToSubscribe & EventType.Event3) == EventType.Event3)
      {
         if(m_HostEvent3 != null)
         {
            m_HostEvent3.Close();
            m_HostEvent3 = null;
         }
      }   
   }
}