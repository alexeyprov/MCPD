//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;
using ServiceModelEx;

[ServiceContract]
interface ICalculatorResponse
{
   [OperationContract(IsOneWay = true)]
   void OnAddCompleted(int result,ExceptionDetail error);
}

[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)] 
class MyCalculatorResponse : ICalculatorResponse
{
   public static event GenericEventHandler<string,int> AddCompleted = delegate{};
   public static event GenericEventHandler<string> AddError = delegate{};

   public MyCalculatorResponse()
   {
      MessageBox.Show("MyCalculatorResponse()","MyCalculatorResponse");
   }

   [OperationBehavior(TransactionScopeRequired = true)]
   public void OnAddCompleted(int result,ExceptionDetail error)
   {
      MessageBox.Show("result =  " + result,"MyCalculatorResponse");
      string methodID = ResponseContext.Current.MethodId;

      if(error == null)
      {
         AddCompleted(methodID,result);
      }
      else
      {
         AddError(methodID);
      }
   }
}

