//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;
using ServiceModelEx;


[ServiceContract] 
interface ICalculator
{
   [OperationContract(IsOneWay = true)]
   void Add(int number1,int number2);
}

[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)] 
class MyCalculator : ICalculator,IDisposable
{
   public MyCalculator()
   {
      MessageBox.Show("MyCalculator()","MyCalculator");
   }
   [OperationBehavior(TransactionScopeRequired = true)]
   public void Add(int number1,int number2)
   {
      int result = 0;
      ExceptionDetail error = null;

      try
      {
         result = number1 + number2;
      }
      //Don't rethrow 
      catch(Exception exception)
      {
         error = new ExceptionDetail(exception);
      }
      finally
      {
         using(ResponseScope<ICalculatorResponse> scope = new ResponseScope<ICalculatorResponse>("NoMSMQSecurity"))
         {
            scope.Response.OnAddCompleted(result,error);
         }
      }
   }
   public void Dispose()
   {
      MessageBox.Show("Dispose()","MyCalculator");
   }
}

