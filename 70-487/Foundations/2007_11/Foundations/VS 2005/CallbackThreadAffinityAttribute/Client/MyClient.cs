//2008 IDesign Inc.   
//Questions? Comments? go to 
//http://www.idesign.net

using System;

using System.ServiceModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ServiceModelEx;

namespace Client
{
   [CallbackThreadAffinityBehavior(typeof(MyClient))]
   public partial class MyClient : IMyContractCallback
   {
      public void OnCallback()
      {
         MessageBox.Show("Callback thread is " + Thread.CurrentThread.Name,"MyClient.OnCallback()");
      }
   }
}