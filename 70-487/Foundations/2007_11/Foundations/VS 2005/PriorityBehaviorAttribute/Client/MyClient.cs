//2008 IDesign Inc.   
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel;
using ServiceModelEx;
using System.Threading;

namespace Client
{
   public partial class MyClient : Form
   {
      CallPriority m_Priority = CallPriority.Normal;

      public MyClient()
      {
         InitializeComponent();
      }
      void OnCall(object sender,EventArgs e)
      {
         //Does not have to use a worker thread, but it enables using  
         //a single client to demo multiple calls
         Thread thread = new Thread(delegate()
                                    {
                                       MyContractClient proxy = new MyContractClient(m_Priority);
                                       proxy.MyMethod("Called with Priority " + m_Priority);
                                       proxy.Close();
                                    });
         thread.IsBackground = true;
         thread.Start();
      }

      void OnChecked(object sender,EventArgs e)
      {
         RadioButton button = sender as RadioButton;
         Debug.Assert(button != null);

         if(button.Checked)
         {
            m_Priority = (CallPriority)Enum.Parse(typeof(CallPriority),button.Text);
         }
      }
   }
}



