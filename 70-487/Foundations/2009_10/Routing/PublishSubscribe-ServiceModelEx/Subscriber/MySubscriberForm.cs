//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ServiceModel;
using Microsoft.ServiceBus;
using ServiceModelEx.ServiceBus;


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



partial class MySubscriberForm : Form
{
   enum EventType
   {
      Event1 = 1,
      Event2 = 2,
      Event3 = 4,
      AllEvents = Event1|Event2|Event3
   }

   EventsRouterHost m_Host;

   public MySubscriberForm()
   {
      InitializeComponent();

      string baseAddress = "sb://MySolution.servicebus.windows.net/";

      m_Host = new EventsRouterHost(typeof(MySubscriber),baseAddress);

      m_Host.ConfigureAnonymousMessageSecurity("MyserviceCert");
      m_Host.SetServiceBusPassword("MyPassword");
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
   void OnClosing(object sender,FormClosingEventArgs e)
   {
      m_Host.Unsubscribe();
   }
   void OnSubscribe(object sender,EventArgs e)
   {
      EventType eventToSubscribe = GetSelectedEvent();

      switch(eventToSubscribe)
      {
         case EventType.Event1:
         {
            m_Host.Subscribe(typeof(IMyEvents),"OnEvent1");
            break;
         }
         case EventType.Event2:
         {
            m_Host.Subscribe(typeof(IMyEvents),"OnEvent2");
            break;
         }
         case EventType.Event3:
         {
            m_Host.Subscribe(typeof(IMyEvents),"OnEvent3");
            break;
         }
         case EventType.AllEvents:
         {
            m_Host.Subscribe(typeof(IMyEvents));
            break;
         }
         default:
         {
            throw new InvalidOperationException("Unknown event type");
         }
      }
   }
   void OnUnsubscribe(object sender,EventArgs e)
   {
      EventType eventToSubscribe = GetSelectedEvent();
      switch(eventToSubscribe)
      {
         case EventType.Event1:
         {
            m_Host.Unsubscribe(typeof(IMyEvents),"OnEvent1");
            break;
         }
         case EventType.Event2:
         {
            m_Host.Unsubscribe(typeof(IMyEvents),"OnEvent2");
            break;
         }
         case EventType.Event3:
         {
            m_Host.Unsubscribe(typeof(IMyEvents),"OnEvent3");
            break;
         }
         case EventType.AllEvents:
         {
            m_Host.Unsubscribe(typeof(IMyEvents));
            break;
         }
         default:
         {
            throw new InvalidOperationException("Unknown event type");
         }
      } 
   }
}