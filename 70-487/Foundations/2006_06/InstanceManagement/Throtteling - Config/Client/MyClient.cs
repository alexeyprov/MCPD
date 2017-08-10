//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Client
{
   public partial class MyClient : Form
   {
      MyContractProxy m_Proxy = new MyContractProxy();

      public MyClient()
      {
         InitializeComponent();
      }

      void OnCall(object sender,EventArgs e)
      {
         m_Proxy.MyMethod();
      }

      void OnClosing(object sender,FormClosingEventArgs e)
      {
         m_Proxy.Close();
      }
   }
}



