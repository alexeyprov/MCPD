// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;

[ServiceContract]
interface ICalculator
{
   [OperationContract(IsOneWay = true)]
   void Add(int number1,int number2);
}

class CalculatorClient : ClientBufferResponseBase<ICalculator>,ICalculator
{
   static int m_MethodId = 123;

   public CalculatorClient(string secret,Uri responseAddress) : base(secret,responseAddress)
   {}

   public void Add(int number1,int number2)
   {
      Enqueue(()=>Channel.Add(number1,number2));
   }
   protected override string GenerateMethodId()
   {
      lock(typeof(CalculatorClient))
      {
         int id = ++m_MethodId;
         return id.ToString();
      }
   }
}
