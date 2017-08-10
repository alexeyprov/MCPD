//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.ServiceModel;
using Microsoft.ServiceBus;

namespace ServiceModelEx
{
   partial class QueueViewControl : NodeViewControl
   {
      DateTime m_Expiration;

      public QueueViewControl()
      {
         InitializeComponent();
      }
      
      public override void Refresh(ServiceBusNode node,TransportClientEndpointBehavior credential)
      {
         QueuePolicy policy = node.Policy as QueuePolicy;

         m_Expiration = policy.ExpirationInstant;
         m_ExpirationTimePicker.Value = m_Expiration;

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

         m_DequeueRetriesTextBox.Text = policy.MaxDequeueRetries.ToString();
         m_QueueLengthTextBox.Text = policy.MaxQueueLength.ToString();
         m_MaxReadersTextBox.Text = policy.MaxConcurrentReaders.ToString();

         base.Refresh(node,credential);
      }
      
      bool IsDirty(QueuePolicy policy)
      {
         if(m_DequeueRetriesTextBox.Text != "")
         {
            if(Convert.ToInt32(m_DequeueRetriesTextBox.Text) != policy.MaxDequeueRetries)
            {
               return true;
            }
         }
         if(m_QueueLengthTextBox.Text != "")
         {
            if(Convert.ToInt32(m_QueueLengthTextBox.Text) != policy.MaxQueueLength)
            {
               return true;
            }
         }
         if(m_MaxReadersTextBox.Text != "")
         {
            if(Convert.ToInt32(m_MaxReadersTextBox.Text) != policy.MaxConcurrentReaders)
            {
               return true;
            }
         }

         if(m_ExpirationTimePicker.Value != m_Expiration)
         {
            return true;
         }

         switch(m_OverflowComboBox.Text)
         {
            case "Reject":
            {
               if(policy.Overflow != OverflowPolicy.RejectIncomingMessage)
               {
                  return true;
               }
               break;
            }            
            case "Discard Incoming":
            {
               if(policy.Overflow != OverflowPolicy.DiscardIncomingMessage)
               {
                  return true;
               }
               break;
            }
            case "Discard Existing":
            {
               if(policy.Overflow != OverflowPolicy.DiscardExistingMessage)
               {
                  return true;
               }
               break;
            }
         }
         return false;
      }

      void OnTimerTick(object sender,EventArgs e)
      {
         try
         {
            if(Node == null)
            {
               return;
            }
            QueuePolicy policy = Node.Policy as QueuePolicy;
            m_UpdateButton.Enabled = IsDirty(policy);
            m_ResetButton.Enabled = IsDirty(new QueuePolicy());
         }
         catch
         {}
      }

      void OnUpdate(object sender,EventArgs e)
      {
         QueuePolicy policy = new QueuePolicy();

         if(m_DequeueRetriesTextBox.Text != "")
         {
            policy.MaxDequeueRetries = Convert.ToInt32(m_DequeueRetriesTextBox.Text);
         }
         if(m_QueueLengthTextBox.Text != "")
         {
            policy.MaxQueueLength = Convert.ToInt32(m_QueueLengthTextBox.Text);
         }
         if(m_MaxReadersTextBox.Text != "")
         {
            policy.MaxConcurrentReaders = Convert.ToInt32(m_MaxReadersTextBox.Text);
         }
         policy.ExpirationInstant = m_ExpirationTimePicker.Value;

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
         ApplyPolicy(policy);  
      }
      void ApplyPolicy(QueuePolicy policy)
      {
         try
         {
            QueueClient client = QueueManagementClient.GetQueue(Credential,RealAddress);
            client.DeleteQueue();
            QueueManagementClient.CreateQueue(Credential,RealAddress,policy);
            Explore();
         }
         catch(Exception exception)
         {
            MessageBox.Show("Error applying change: " + exception.Message,"Service Bus Explorer",MessageBoxButtons.OK,MessageBoxIcon.Error);
         }
      }
      void OnReset(object sender,EventArgs e)
      {
         DialogResult result = MessageBox.Show("Are you sure you want to reset the queue to it's default?","Service Bus Explorer",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
         if(result == DialogResult.No)
         {
            return;
         }
         ApplyPolicy(new QueuePolicy());  
      }
      void OnPurge(object sender,EventArgs e)
      {
         DialogResult result = MessageBox.Show("Are you sure you want to remove all messages?","Service Bus Explorer",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
         if(result == DialogResult.No)
         {
            return;
         }
         try
         {
            QueueClient client = QueueManagementClient.GetQueue(Credential,RealAddress);
            client.Purge();
         }
         catch(Exception exception)
         {
            MessageBox.Show("Error purging queue: " + exception.Message,"Service Bus Explorer",MessageBoxButtons.OK,MessageBoxIcon.Error);
         }
      }
      void OnDelete(object sender,EventArgs e)
      {
         DialogResult result = MessageBox.Show("Are you sure you want to delete the queue?","Service Bus Explorer",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
         if(result == DialogResult.No)
         {
            return;
         }
         try
         {
            QueueClient client = QueueManagementClient.GetQueue(Credential,RealAddress);
            client.DeleteQueue();
            Explore();
         }
         catch(Exception exception)
         {
            MessageBox.Show("Error deleting queue: " + exception.Message,"Service Bus Explorer",MessageBoxButtons.OK,MessageBoxIcon.Error);
         }
      }
   }
}
