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
         IContextChannel channel;
         string sessionID;

         MyContractProxy proxy = new MyContractProxy();

         //proxy.CannotStart();

         proxy.StartSession();
         proxy.EndSession();
         //proxy.StartSession();
         proxy.Close();

         channel = proxy.InnerChannel;
         sessionID = channel.SessionId;
         Trace.WriteLine(sessionID);

         proxy = new MyContractProxy();
         proxy.StartAndEndSession();
         //proxy.StartSession();
         channel = proxy.InnerChannel;
         sessionID = channel.SessionId;
         Trace.WriteLine(sessionID);

         proxy.Close();
      }
   }
}



