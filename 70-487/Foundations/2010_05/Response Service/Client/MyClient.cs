// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel;
using System.Collections.Generic;
using ServiceModelEx;
using ServiceModelEx.ServiceBus;


[ServiceContract]
interface ICalculatorResponse
{
   [OperationContract(IsOneWay = true)]
   void OnAddCompleted(int result,ExceptionDetail error);
}


partial class MyClient : FormHost,ICalculatorResponse
{
   Dictionary<string,int> Results = new Dictionary<string,int>();
   CalculatorClient m_Proxy;

   public MyClient()
   {
      string secret = "**********  Enter Your Secret Here  **********";
      Uri resposeAddress = new Uri(@"https://myservicenamespace.servicebus.windows.net/MyResponseQueue/");

      Host = new BufferedServiceBusHost<MyClient>(this,secret,resposeAddress);

      m_Proxy = new CalculatorClient(secret,resposeAddress);

      InitializeComponent();
   }
   [OperationBehavior(TransactionScopeRequired = true)]
   public void OnAddCompleted(int result,ExceptionDetail error)
   {
      MessageBox.Show("result =  " + result,"MyCalculatorResponse");
      string methodID = ResponseContext.Current.MethodId;

      Results.Add(methodID,result);
      UpdateMethodId(methodID);
   }


   void UpdateMethodId(string id)
   {
      if(m_MethodIDComboBox.Items.Contains(id) == false)
      {
         m_MethodIDComboBox.Items.Add(id);
      }
      m_MethodIDComboBox.Text = id;
   }

   void OnAdd(object sender,EventArgs e)
   {
      int number1 = Convert.ToInt32(m_Number1TextBox.Text);
      int number2 = Convert.ToInt32(m_Number2TextBox.Text);
        
      m_Proxy.Add(number1,number2);
      string methodId = m_Proxy.Header.MethodId;

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

   void OnClosed(object sender,FormClosedEventArgs e)
   {
      m_Proxy.Close();
   }
}


