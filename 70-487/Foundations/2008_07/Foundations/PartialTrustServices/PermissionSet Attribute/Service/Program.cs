//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;

namespace MyNamespace
{
   class Program
   {
      public static void Main()
      {
         ServiceHost host = new ServiceHost(typeof(MyService));
         host.Open();

         Console.WriteLine("Press ENTER to shut down services.");
         Console.ReadLine();

         host.Close();
      }
   }
}






