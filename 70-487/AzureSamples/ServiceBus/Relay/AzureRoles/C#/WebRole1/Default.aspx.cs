//---------------------------------------------------------------------------------
// Microsoft (R)  Windows Azure SDK
// Software Development Kit
// 
// Copyright (c) Microsoft Corporation. All rights reserved.  
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
//---------------------------------------------------------------------------------

namespace Microsoft.ServiceBus.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Configuration;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using Microsoft.ServiceBus;

    public partial class _Default : System.Web.UI.Page
    {
        string servicePath;
        string serviceNamespace;
        string issuerName;
        string issuerSecret;
        Uri serviceAddress;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieve Settings from App.Config
            servicePath = ConfigurationManager.AppSettings["ServicePath"];
            serviceNamespace = ConfigurationManager.AppSettings["ServiceNamespace"];
            issuerName = ConfigurationManager.AppSettings["IssuerName"];
            issuerSecret = ConfigurationManager.AppSettings["IssuerSecret"];

            //Construct a Service Bus URI
            serviceAddress = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, servicePath);
            serviceAddressLabel.Text = serviceAddress.ToString();
        }

        protected void sendButton_Click(object sender, EventArgs e)
        {
            ChannelFactory<IEchoChannel> channelFactory = null;
            IEchoChannel channel = null;
            try
            {
                //Create a Behavior for the Credentials
                TransportClientEndpointBehavior sharedSecretServiceBusCredential = new TransportClientEndpointBehavior();
                sharedSecretServiceBusCredential.TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);

                //Create a Channel Factory
                channelFactory = new ChannelFactory<IEchoChannel>(new NetTcpRelayBinding(), new EndpointAddress(serviceAddress));
                channelFactory.Endpoint.Behaviors.Add(sharedSecretServiceBusCredential);

                LogMessage("Opening channel to: {0}", serviceAddress);
                channel = channelFactory.CreateChannel();
                channel.Open();

                LogMessage("Sending: {0}", echoTextBox.Text);
                string echoedText = channel.Echo(echoTextBox.Text);
                LogMessage("Received: {0}", echoedText);
                echoTextBox.Text = string.Empty;

                LogMessage("Closing channel");
                channel.Close();
                LogMessage("Closing factory");
                channelFactory.Close();
            }
            catch (Exception ex)
            {
                LogMessage("Error sending: {0}<br/>{1}", ex.Message, ex.StackTrace.Replace("\n", "<br/>"));

                // Close the channel and factory properly
                if (channel != null)
                {
                    CloseCommunicationObject(channel);
                }
                if (channelFactory != null)
                {
                    CloseCommunicationObject(channelFactory);
                }
            }
        }

        void LogMessage(string format, params object[] parameters)
        {
            msgLabel.Text += DateTime.Now.ToString() + ": " + string.Format(format, parameters) + "<br/>";
        }

        void CloseCommunicationObject(ICommunicationObject communicationObject)
        {
            bool shouldAbort = true;
            if (communicationObject.State == CommunicationState.Opened)
            {
                try
                {
                    communicationObject.Close();
                    shouldAbort = false;
                }
                catch (TimeoutException)
                {
                }
                catch (CommunicationException)
                {
                }
            }

            if (shouldAbort)
            {
                communicationObject.Abort();
            }
        }
    }
}