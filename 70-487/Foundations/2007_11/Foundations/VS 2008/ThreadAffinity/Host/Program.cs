//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ServiceModel;
using MyNamespace;
using System.Threading;
using ServiceModelEx;

namespace Host
{
   static class Program
   {
      static void Main()
      {
         SynchronizationContext synchronizationContext = new AffinitySynchronizer();
         using(synchronizationContext as IDisposable)
         {
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);

            ServiceHost host = new ServiceHost(typeof(MyService),new Uri("http://localhost:8000"));
            host.Open();

            Thread.Sleep(Timeout.Infinite);


            host.Close();
         }
      }
   }
}

