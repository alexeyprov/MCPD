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
      void OnCallFirstEndpoint(object sender,EventArgs e)
      {
         MyContractProxy proxy = new MyContractProxy();
         proxy.MyMethod();

         string sessionID = proxy.InnerChannel.SessionId;
         Trace.WriteLine(sessionID);

         proxy.Close();
      }
      void OnCallSecondEndpoint(object sender,EventArgs e)
      {
         MyOtherContractProxy proxy = new MyOtherContractProxy();
         proxy.MyOtherMethod();

         string sessionID = proxy.InnerChannel.SessionId;
         Trace.WriteLine(sessionID);

         proxy.Close();
      }
   }
}



