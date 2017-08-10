// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ServiceModelEx.ServiceBus;


[ServiceContract]
interface ICalculatorResponse
{
   [OperationContract(IsOneWay = true)]
   void OnAddCompleted(int result,ExceptionDetail error);
}

class CalculatorResponseClient : ServiceBufferResponseBase<ICalculatorResponse>,ICalculatorResponse
{
   public void OnAddCompleted(int result,ExceptionDetail error)
   {
      Enqueue(()=>Channel.OnAddCompleted(result,error));
   }
}