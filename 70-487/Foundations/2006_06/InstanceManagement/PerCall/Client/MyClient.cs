//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel;

namespace Client
{
   public partial class MyClient : Form
   {
      public MyClient()
      {
         InitializeComponent();
      }

      void OnCall(object sender,EventArgs e)
      {
         MyContractProxy proxy = new MyContractProxy();

         IContextChannel channel = proxy.InnerChannel;
         string sessionID = channel.SessionId;
         Trace.WriteLine("Client Session1: " + (sessionID??"None"));

         proxy.MyMethod();

         sessionID = channel.SessionId;
         Trace.WriteLine("Client Session2: " + (sessionID??"None"));


         proxy.MyMethod();

         proxy.Close();
      }
   }
}



