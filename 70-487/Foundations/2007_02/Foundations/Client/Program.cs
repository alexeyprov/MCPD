//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.ServiceModel;

namespace Client
{
   static class Program
   {
      static void Main()
      {
         MyCalculatorResponse.AddCompleted += MyClient.OnAddCompleted;

         ServiceHost host = new ServiceHost(typeof(MyCalculatorResponse));
         host.Open();

         Application.Run(new MyClient());
      }
   }
}