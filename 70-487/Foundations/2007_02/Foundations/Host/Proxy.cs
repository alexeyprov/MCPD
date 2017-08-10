//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;


[ServiceContract]
public interface ICalculatorResponse
{
   [OperationContract(IsOneWay = true)]
   void OnAddCompleted(int result,ExceptionDetail error);
}