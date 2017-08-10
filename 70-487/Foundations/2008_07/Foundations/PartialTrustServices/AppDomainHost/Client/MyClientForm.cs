//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.ServiceModel.Security;
using System.Threading;
using System.Diagnostics;


public partial class MyClientForm : Form
{
   string m_EndpointName = "DefaultFullTrust";

   public MyClientForm()
   {
      InitializeComponent();
      EventWaitHandle handle = new EventWaitHandle(false,EventResetMode.ManualReset,"HostReady");
      handle.WaitOne();
      handle.Close();
   }

   void OnCall(object sender,EventArgs e)
   {
      MyContractClient proxy = new MyContractClient(m_EndpointName);
      try
      {
         proxy.MyMethod();
         proxy.Close();
      }
      catch(SecurityAccessDeniedException exception)
      {
         Trace.Write("SecurityAccessDeniedException: " + exception.Message);
      }
   }

   void OnCheckedChanged(object sender,EventArgs e)
   {
      m_EndpointName = (sender as RadioButton).Text.Replace(" ","");
   }
}

