// © 2007-2008 Michele Leroux Bustamante. All rights reserved 
// Book: Learning WCF, O'Reilly
// Book Blog: www.thatindigogirl.com
// Michele's Blog: www.dasblonde.net
// IDesign: www.idesign.net

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MessagingClient
{
    public partial class SendMessageForm : Form
    {
        public SendMessageForm()
        {
            InitializeComponent();
        }

        localhost.MessageManagerServiceClient m_proxy = new MessagingClient.localhost.MessageManagerServiceClient();
        localhost.MessageManagerServiceOneWayClient m_proxyOneWay = new MessagingClient.localhost.MessageManagerServiceOneWayClient();

        private void cmdSend_Click(object sender, EventArgs e)
        {

            try
            {
                this.txtResponse.Text = m_proxy.SendMessage(this.txtMessage.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendMessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {
                if (this.m_proxy.State != System.ServiceModel.CommunicationState.Faulted)
                    this.m_proxy.Close();
                else
                    this.m_proxy.Abort();
            }
            catch { }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtResponse.Text = "";
                m_proxyOneWay.SendOneWayMessage(this.txtMessage.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}