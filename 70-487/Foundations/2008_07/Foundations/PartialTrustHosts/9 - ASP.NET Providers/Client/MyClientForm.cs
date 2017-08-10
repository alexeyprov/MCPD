//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel;
using System.Net;


partial class MyClientForm : Form
{
   public MyClientForm()
   {
      InitializeComponent();
   }

   void OnCall(object sender,EventArgs e)
   {
      MyContractClient proxy = new MyContractClient();

      proxy.ClientCredentials.UserName.UserName = m_UserNameTextbox.Text;
      proxy.ClientCredentials.UserName.Password = m_PasswordTextBox.Text;

      proxy.MyMethod();

      proxy.Close();
   }
}



