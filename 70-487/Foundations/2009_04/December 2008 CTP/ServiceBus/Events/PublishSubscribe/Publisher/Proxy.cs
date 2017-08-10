//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
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

class MyEventsProxy : ClientBase<IMyEvents>,IMyEvents
{
   public MyEventsProxy(string endpointName) : base(endpointName)
   {}

   public void OnEvent1()
   {
      Channel.OnEvent1();
   }

   public void OnEvent2(int number)
   {
      Channel.OnEvent2(number);
   }

   public void OnEvent3(int number,string text)
   {
      Channel.OnEvent3(number,text);
   }
}
