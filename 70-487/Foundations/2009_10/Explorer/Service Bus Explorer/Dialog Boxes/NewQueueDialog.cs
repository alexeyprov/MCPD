//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using Microsoft.ServiceBus;
using System.Windows.Forms;
using System.Diagnostics;


namespace ServiceModelEx
{
   partial class NewQueueDialog : Form
   {
      readonly string BaseAddress;

      public QueueClient Client
      {get;private set;}

      string Solution
      {
         get
         {
            ExplorerForm form = Application.OpenForms[0] as ExplorerForm;
            return form.Solution;
         }
      }

      TransportClientEndpointBehavior Credential
      {
         get
         {
            ExplorerForm form = Application.OpenForms[0] as ExplorerForm;
            return form.Graphs[Solution.ToLower()].Credential;
         }
      }

      public NewQueueDialog(string solution)
      {
         InitializeComponent();

         BaseAddress = ServiceBusEnvironment.CreateServiceUri("sb",solution,"").AbsoluteUri;
         if(BaseAddress.EndsWith(@"/") == false)
         {
            BaseAddress += @"/";
         }

         m_AddressTextBox.Text = BaseAddress;
         OnTextChanged(this,EventArgs.Empty);


         QueuePolicy policy = new QueuePolicy();

         m_DequeueRetriesTextBox.Text = policy.MaxDequeueRetries.ToString();
         m_ExpirationTimePicker.Value = policy.ExpirationInstant;;
         m_QueueLengthTextBox.Text = policy.MaxQueueLength.ToString();
         m_MaxReadersTextBox.Text = policy.MaxConcurrentReaders.ToString();

         int overflowIndex = 0;
         switch(policy.Overflow)
         {
            case OverflowPolicy.RejectIncomingMessage:
            {
               overflowIndex = 0;
               break;
            }
            case OverflowPolicy.DiscardIncomingMessage:
            {
               overflowIndex = 1;
               break;
            }
            case OverflowPolicy.DiscardExistingMessage:
            {
               overflowIndex = 2;
               break;
            }
         }
         m_OverflowComboBox.Text = m_OverflowComboBox.Items[overflowIndex] as string;
      }

      void OnCreate(object sender,EventArgs e)
      {
         Debug.Assert(m_AddressTextBox.Text != BaseAddress);

         QueuePolicy policy = new QueuePolicy();

         if(m_MaxReadersTextBox.Text != "")
         {
            policy.MaxConcurrentReaders = Convert.ToInt32(m_MaxReadersTextBox.Text);
         }

         if(m_DequeueRetriesTextBox.Text != "")
         {
            policy.MaxDequeueRetries = Convert.ToInt32(m_DequeueRetriesTextBox.Text);
         }

         policy.ExpirationInstant = m_ExpirationTimePicker.Value;

         if(m_QueueLengthTextBox.Text != "")
         {
            policy.MaxQueueLength = Convert.ToInt32(m_QueueLengthTextBox.Text);
         }

         switch(m_OverflowComboBox.Text)
         {
            case "Reject":
            {
               policy.Overflow = OverflowPolicy.RejectIncomingMessage;
               break;
            }            
            case "Discard Incoming":
            {
               policy.Overflow = OverflowPolicy.DiscardIncomingMessage;
               break;
            }
            case "Discard Existing":
            {
               policy.Overflow = OverflowPolicy.DiscardExistingMessage;
               break;
            }
         }
         if(m_AddressTextBox.Text.EndsWith(@"/") == false)
         {
            m_AddressTextBox.Text += @"/";
         }
         try
         {
            Client = QueueManagementClient.CreateQueue(Credential,new Uri(m_AddressTextBox.Text),policy);
         }
         catch(Exception exception)
         {
            MessageBox.Show("Unable to create queue: " + exception.Message,"Service Bus Explorer",MessageBoxButtons.OK,MessageBoxIcon.Error);
            return;
         }

         Close();
      }

      void OnTextChanged(object sender,EventArgs e)
      {
         m_CreateButton.Enabled = m_AddressTextBox.Text.StartsWith(BaseAddress) && m_AddressTextBox.Text.Length >= BaseAddress.Length+1;
      }
   }
}
