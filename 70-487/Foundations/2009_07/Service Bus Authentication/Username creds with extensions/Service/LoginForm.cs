// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using Microsoft.ServiceBus;



partial class LoginForm : Form
{
   public LoginForm(string solution)
   {
      InitializeComponent();
      m_SolutionLabel.Text += solution;
   }
   
   public string Password
   {get;protected set;}

   void OnLogin(object sender,EventArgs e)
   {
      Password = m_PasswordTextBox.Text;
      Close();
   }
}
