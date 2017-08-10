//IDesign Inc. 2007 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel;
using ServiceModelEx;

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
         MyContractClient proxy1 = new MyContractClient();
         SecurityHelper.SecureProxy(proxy1,m_UserNameTextbox.Text,m_PasswordTextBox.Text);
         proxy1.MyMethod();
         proxy1.Close();

         MyContractSecureClient proxy2 = new MyContractSecureClient(m_UserNameTextbox.Text,m_PasswordTextBox.Text);
         proxy2.MyMethod();
         proxy2.Close();
      }
   }
}



