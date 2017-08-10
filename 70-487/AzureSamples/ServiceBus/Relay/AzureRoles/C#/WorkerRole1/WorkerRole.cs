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
    using System.Configuration;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using Microsoft.ServiceBus;
    using Microsoft.WindowsAzure.ServiceRuntime;

    public class WorkerRole : RoleEntryPoint
    {
        private ServiceHost host;

        public override void Run()
        {
            // Retrieve Settings from App.Config
            string servicePath = ConfigurationManager.AppSettings["ServicePath"];
            string serviceNamespace = ConfigurationManager.AppSettings["ServiceNamespace"];
            string issuerName = ConfigurationManager.AppSettings["IssuerName"];
            string issuerSecret = ConfigurationManager.AppSettings["IssuerSecret"];

            // Construct a Service Bus URI
            Uri uri = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, servicePath);

            // Create a Behavior for the Credentials
            TransportClientEndpointBehavior sharedSecretServiceBusCredential = new TransportClientEndpointBehavior();
            sharedSecretServiceBusCredential.TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);


            // Create the Service Host 
            host = new ServiceHost(typeof(EchoService), uri);
            ContractDescription contractDescription = ContractDescription.GetContract(typeof(IEchoContract), typeof(EchoService));
            ServiceEndpoint serviceEndPoint = new ServiceEndpoint(contractDescription);
            serviceEndPoint.Address = new EndpointAddress(uri);
            serviceEndPoint.Binding = new NetTcpRelayBinding();
            serviceEndPoint.Behaviors.Add(sharedSecretServiceBusCredential);
            host.Description.Endpoints.Add(serviceEndPoint);

            // Open the Host
            host.Open();

            while (true)
            {
                //Do Nothing
            }
            
        }

        public override void OnStop()
        {
            base.OnStop();
            host.Close();
        }

        public override bool OnStart()
        {
            RoleEnvironment.Changing += RoleEnvironmentChanging;
            return base.OnStart();
        }

        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
                e.Cancel = true;
        }
    }
}
