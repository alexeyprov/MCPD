// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;

class DiscoveredSubscriber : IMyEvents
{
   public void OnEvent1()
   {
      MessageBox.Show("OnEvent1()","DiscoveredSubscriber");
   }
   public void OnEvent2(int number)
   {
      MessageBox.Show("OnEvent2()","DiscoveredSubscriber");
   }
   public void OnEvent3(int number,string text)
   {
      MessageBox.Show("OnEvent3()","DiscoveredSubscriber");
   }
}