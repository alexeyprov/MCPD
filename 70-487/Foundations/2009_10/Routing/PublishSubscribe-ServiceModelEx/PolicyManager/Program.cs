using System;
using Microsoft.ServiceBus;
using System.Threading;
using System.ServiceModel;
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


class Program
{
   static void Main()
   {
      Console.WriteLine("Creating routers.......");

      string passowrd = "MyPassword";

      ServiceBusHelper.CreateEventRouters(@"sb://MySolution.servicebus.windows.net/",typeof(IMyEvents),passowrd);
      
      Console.WriteLine("Routers are ready");

      Console.ReadLine();
   }
}