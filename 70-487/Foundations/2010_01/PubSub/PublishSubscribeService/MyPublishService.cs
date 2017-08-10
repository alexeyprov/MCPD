// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx;


class MyPublishService : DiscoveryPublishService<IMyEvents>,IMyEvents
{
   public void OnEvent1()
   {
      FireEvent();
   }
   public void OnEvent2(int number)
   {
      FireEvent(number);
   }
   public void OnEvent3(int number,string text)
   {
      FireEvent(number,text);
   }
}