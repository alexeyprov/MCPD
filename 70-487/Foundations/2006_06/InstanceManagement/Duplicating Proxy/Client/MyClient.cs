//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Client
{
   public partial class MyClient 
   {
      public MyClient()
      {
         InitializeComponent();
      }

      void OnCall(object sender,EventArgs e)
      {
         MyContractProxy proxy1 = new MyContractProxy();
         proxy1.MyMethod();

         IClientChannel channel1 = proxy1.InnerChannel;
         string sessionID1 = channel1.SessionId;
         Trace.WriteLine("Client sessionID1: " + sessionID1);

         //For simplicity,just grab binding from first proxy
         Binding binding = proxy1.Endpoint.Binding;

         EndpointAddress address = channel1.ResolveInstance();
         
         proxy1.Close();

         MyContractProxy proxy2 = new MyContractProxy(binding,address);
         proxy2.MyMethod();

         IContextChannel channel2 = proxy2.InnerChannel;
         string sessionID2 = channel2.SessionId;
         Trace.WriteLine("Client sessionID2: " + sessionID2); 

         Debug.Assert(sessionID1 != sessionID2);


         proxy2.MyMethod();

         proxy2.Close();
      }
   }
}



