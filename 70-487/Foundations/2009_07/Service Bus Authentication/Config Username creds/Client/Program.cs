// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;


class Program
{
   static void Main(string[] args)
   {
      MyContractClient proxy = new MyContractClient();

      proxy.MyMethod();

      proxy.Close();
   }
}
