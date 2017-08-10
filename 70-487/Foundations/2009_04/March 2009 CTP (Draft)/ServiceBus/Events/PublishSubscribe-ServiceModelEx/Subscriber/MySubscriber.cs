//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;

class MySubscriber : IMyEvents
{
   MySubscriberForm m_Subscriber = Application.OpenForms[0] as MySubscriberForm;

   public void OnEvent1()
   {
      m_Subscriber.OnEvent1();
   }

   public void OnEvent2(int number)
   {
      m_Subscriber.OnEvent2(number);
   }

   public void OnEvent3(int number,string text)
   {
      m_Subscriber.OnEvent3(number,text);
   }
}