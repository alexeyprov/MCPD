//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Diagnostics;


namespace ServiceModelEx
{
   partial class LogonDialog : Form
   {
      public string Password
      {get;private set;}

      public LogonDialog(string solution)
      {
         InitializeComponent();

         Debug.Assert(String.IsNullOrEmpty(solution) == false);

         m_CertNValueTextBox.Text = solution;
         m_FindValueComboBox.Text = m_FindValueComboBox.Items[0] as string;
         m_StoreLoctionComboBox.Text = m_StoreLoctionComboBox.Items[0] as string;
         m_StoreNameComboBox.Text = m_StoreNameComboBox.Items[0] as string;

         m_SolutionTextBox.Text = solution;

         OnTextChanged(this,EventArgs.Empty);
      }

      void OnLogon(object sender,EventArgs e)
      {
         Debug.Assert(String.IsNullOrEmpty(m_PasswordTextBox.Text) == false);
         Password = m_PasswordTextBox.Text;

         Close();
      }

      void OnTextChanged(object sender,EventArgs e)
      {
         m_LogonButton.Enabled = String.IsNullOrEmpty(m_PasswordTextBox.Text) == false;
      }
   }
}
