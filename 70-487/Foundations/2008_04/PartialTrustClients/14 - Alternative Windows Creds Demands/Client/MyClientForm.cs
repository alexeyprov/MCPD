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
      m_DomainTextbox.Text = "MYVPC";
   }

   void OnCall(object sender,EventArgs e)
   {
      NetworkCredential credentials = new NetworkCredential();
      credentials.Domain = m_DomainTextbox.Text;
      credentials.UserName = m_UserNameTextbox.Text;
      credentials.Password = m_PasswordTextBox.Text;

      MyContractClient proxy = new MyContractClient();
      proxy.ClientCredentials.Windows.ClientCredential = credentials;

      proxy.MyMethod();

      proxy.Close();
   }
}


