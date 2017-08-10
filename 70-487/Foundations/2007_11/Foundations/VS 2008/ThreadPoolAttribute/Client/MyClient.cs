//2008 IDesign Inc. 
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
         MyContractClient proxy = new MyContractClient();
         proxy.MyMethod();
         proxy.Close();
      }
      void OnCallSecondEndpoint(object sender,EventArgs e)
      {
         MyOtherContractClient proxy = new MyOtherContractClient();
         proxy.MyOtherMethod();
         proxy.Close();
      }
   }
}



