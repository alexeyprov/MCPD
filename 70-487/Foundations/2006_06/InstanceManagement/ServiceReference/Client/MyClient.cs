//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows.Forms;

namespace Client
{
   public partial class MyClient 
   {
      MyContractProxy m_Proxy = new MyContractProxy();

      public MyClient()
      {
         InitializeComponent();
      }
      void OnPassReference(object sender,EventArgs e)
      {
         IClientChannel channel = m_Proxy.InnerChannel;
         EndpointAddress serviceAddress = channel.ResolveInstance();
         SomeContractProxy proxy = new SomeContractProxy();
         proxy.PassReference(EndpointAddress10.FromEndpointAddress(serviceAddress));
         proxy.Close();
      }
      void OnCallMyService(object sender,EventArgs e)
      {
         m_Proxy.MyMethod();
       }
      void OnClosing(object sender,FormClosingEventArgs e)
      {
         m_Proxy.Close();
      }
   }
}



