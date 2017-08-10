// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;
using ServiceModelEx.ServiceBus;


[ServiceContract] 
interface ICalculator
{
   [OperationContract(IsOneWay = true)]
   void Add(int number1,int number2);
}

class MyCalculator : ICalculator
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
         CalculatorResponseClient proxy = new CalculatorResponseClient();
         proxy.OnAddCompleted(result,error);
         proxy.Close();
      }
   }
}

