//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;

class MySubscriber : IMyEvents
{
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
}