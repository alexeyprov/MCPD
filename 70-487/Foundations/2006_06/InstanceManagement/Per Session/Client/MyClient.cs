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
         proxy.MyMethod();

         IContextChannel channel = proxy.InnerChannel;
         string sessionID = channel.SessionId;
         Trace.WriteLine("Client session ID: " + sessionID);

         proxy.MyMethod();

         proxy.Close();
      }
   }
}



