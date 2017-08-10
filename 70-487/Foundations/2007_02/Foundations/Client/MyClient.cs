//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel;
using System.Messaging;
using ServiceModelEx;
using System.Transactions;
using System.Collections.Generic;

namespace Client
{
   public partial class MyClient : Form
   {
      static Dictionary<string,int> Results = new Dictionary<string,int>();

      public MyClient()
      {
         QueuedServiceHelper.VerifyQueue<ICalculator>();

         InitializeComponent();

         foreach(string methodId in Results.Keys)
         {
            m_MethodIDComboBox.Items.Add(methodId);
            m_MethodIDComboBox.Text = methodId;
         }
      }

      public static void OnAddCompleted(string methodID,int result)
      {
         lock(typeof(MyClient))
         {
            Results.Add(methodID,result);
         }
      }

      void UpdateMethodId(string id)
      {
         m_MethodIDComboBox.Items.Add(id);
         m_MethodIDComboBox.Text = id;
      }

      void OnAdd(object sender,EventArgs e)
      {
         int number1 = Convert.ToInt32(m_Number1TextBox.Text);
         int number2 = Convert.ToInt32(m_Number2TextBox.Text);

         CalculatorClient proxy = new CalculatorClient("net.msmq://localhost/private/MyCalculatorResponseQueue");
         string methodId = proxy.Add(number1,number2);
         proxy.Close();

         UpdateMethodId(methodId);
      }

      void OnGetResult(object sender,EventArgs e)
      {
         string methodId = m_MethodIDComboBox.Text;
         lock(typeof(MyClient))
         {
            if(Results.ContainsKey(methodId))
            {
               m_ResultTextBox.Text = Results[methodId].ToString();
            }
         }
      }

      void OnTimerChecked(object sender,EventArgs e)
      {
         m_UpdateTimer.Enabled = m_TimerCheckbox.Checked;
      }
   }
}



