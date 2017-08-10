// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;

[ServiceContract]
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}

class MyService : IMyContract
{
   static int m_Counter = 0;

   public void MyMethod()
   {      
      Console.WriteLine(m_Counter++.ToString());
   }
}