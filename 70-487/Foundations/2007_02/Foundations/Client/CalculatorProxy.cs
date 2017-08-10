//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx;

[ServiceContract]
interface ICalculator
{
   [OperationContract(IsOneWay = true)]
   void Add(int number1,int number2);
}
class CalculatorClient : ResponseClientBase<ICalculator>
{
   static int m_MethodId = 123;

   public CalculatorClient(string responseAddress) : base(responseAddress)
   {}
   public CalculatorClient(string responseAddress,string endpointName) : base(responseAddress,endpointName)
   {}
   public CalculatorClient(string responseAddress,string endpointName,string remoteAddress) : base(responseAddress,endpointName,remoteAddress)
   {}
   public CalculatorClient(string responseAddress,string endpointName,EndpointAddress remoteAddress) : base(responseAddress,endpointName,remoteAddress)
   {}
   public CalculatorClient(string responseAddress,NetMsmqBinding binding,EndpointAddress remoteAddress) : base(responseAddress,binding,remoteAddress)
   {}

   public string Add(int number1,int number2)
   {
      return Enqueue("Add",number1,number2);
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
