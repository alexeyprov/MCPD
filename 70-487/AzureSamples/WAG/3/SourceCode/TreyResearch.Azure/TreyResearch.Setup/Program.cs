//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace TreyResearch.Setup
{
    using System;
    using System.Configuration;
    using System.Data.Services.Client;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;
    using ACS.ServiceManagementWrapper;
    using ACS.ServiceManagementWrapper.ACS.Management;
    using Microsoft.IdentityModel.Claims;
    using Microsoft.ServiceBus.AccessControlExtensions;
    using Orders.Shared.Communication;
    using ManagementService = Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement.ManagementService;
    using RelyingParty = Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement.RelyingParty;
    using RelyingPartyAddress = Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement.RelyingPartyAddress;
    using RuleGroup = Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement.RuleGroup;
    using ServiceIdentity = Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement.ServiceIdentity;
    using ServiceIdentityKey = Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement.ServiceIdentityKey;

    internal class Program
    {
        private static readonly string AcsServiceNamespace = ConfigurationManager.AppSettings["acsnamespace"];
        private static readonly string AcsUsername = ConfigurationManager.AppSettings["acsusername"];
        private static readonly string AcsPassword = ConfigurationManager.AppSettings["acspassword"];

        private static readonly string RealmAddress = ConfigurationManager.AppSettings["realmaddress"];
        private static readonly string ReplyAddress = ConfigurationManager.AppSettings["replyaddress"];

        private static readonly string SymmetricKeyForContoso = ConfigurationManager.AppSettings["symmetrickeyforcontoso"];
        private static readonly string SymmetricKeyFabrikam = ConfigurationManager.AppSettings["symmetrickeyforfabrikam"];

        private static readonly string ServiceBusNamespace = ConfigurationManager.AppSettings["servicebusnamespace"];
        private static readonly string DefaultKey = ConfigurationManager.AppSettings["defaultkey"];
        private static readonly string Issuer = ConfigurationManager.AppSettings["defaultissuer"];

        private static readonly string ServiceBusQueueName = ConfigurationManager.AppSettings["servicebusqueuename"];
        private static readonly string TopicName = ConfigurationManager.AppSettings["topicname"];

        private static readonly string AuditLogListener = ConfigurationManager.AppSettings["auditloglistener.name"];

        private static readonly string ContosoPassword = ConfigurationManager.AppSettings["contosoPassword"];
        private static readonly string FabrikamPassword = ConfigurationManager.AppSettings["fabrikamPassword"];

        private static readonly string ContosoDisplayName = ConfigurationManager.AppSettings["contoso.name"];
        private static readonly string FabrikamDisplayName = ConfigurationManager.AppSettings["fabrikam.name"];

        internal static void Main(string[] args)
        {
            try
            {
                var acs = new ServiceManagementWrapper(AcsServiceNamespace, AcsUsername, AcsPassword);

                Console.WriteLine("Setting up ACS namespace:" + AcsServiceNamespace);

                // ACS namespace setup for the Orders Website
                CleanupIdenityProviders(acs);
                CleanupRelyingParties(acs);
                CreateIdentityProviders(acs);
                CreateRelyingPartysWithRules(acs);

                // Creates a Service Bus Topic, subscriptions and Queue.
                SetupServiceBusTopicAndQueue();

                Console.WriteLine("Setup complete. Press any key to exit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.Error.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                Console.Error.WriteLine(
                    "Please make sure the ACS and ServiceBus namespaces are created, and that you have the corresponding keys and management passwords available.");
                Console.WriteLine();
                Console.WriteLine("Setup did not complete successfully.");
            }

            Console.ReadKey();
        }

        #region ServiceBus Setup

        private static void SetupServiceBusTopicAndQueue()
        {
            // Create Service Bus Queue for order status update messages
            Console.Write(string.Format("Creating {0} ...", ServiceBusQueueName));
            var serviceBusQueueDescription = new ServiceBusQueueDescription
                { Namespace = ServiceBusNamespace, QueueName = ServiceBusQueueName, Issuer = Issuer, DefaultKey = DefaultKey };

            var orderStatusUpdatesQueue = new ServiceBusQueue(serviceBusQueueDescription);
            orderStatusUpdatesQueue.CreateIfNotExists();
            Console.WriteLine("done");

            // Setting up ACS for ServiceBus Queue
            var workerRoleName = ConfigurationManager.AppSettings["statusupdatejob.name"];
            DeleteServiceIdentity(workerRoleName);
            CreateServiceIdentityWithKey(workerRoleName);

            var queueRealm = string.Format("http://{0}.servicebus.windows.net/{1}", ServiceBusNamespace, ServiceBusQueueName.ToLowerInvariant());
            CreateRelyingPartyWithDefaultRuleGroup(ServiceBusQueueName, queueRealm);
            var groupName = "Rule Group for " + workerRoleName;
            CreateRuleGroupWithListenRule(groupName, workerRoleName, ServiceBusQueueName);

            Console.WriteLine(string.Format("Setting up Topic {0} and subscriptions:", TopicName));

            // Create one subscription per transport partner with corresponding filter expression.
            var transportPartners = new[] { "Contoso", "Fabrikam" };
            for (int i = 0; i <= 1; i++)
            {
                string transportPartnerName = transportPartners[i];
                string formattedName = transportPartnerName.Replace(" ", string.Empty).ToLowerInvariant();
                Console.Write(string.Format("Creating {0}Subscription...", formattedName));

                var serviceBusTopicDescription = new ServiceBusSubscriptionDescription
                    {
                        Namespace = ServiceBusNamespace,
                        TopicName = TopicName,
                        SubscriptionName = string.Format("{0}Subscription", formattedName),
                        Issuer = Issuer,
                        DefaultKey = DefaultKey
                    };

                var serviceBusSubscription = new ServiceBusSubscription(serviceBusTopicDescription);

                string filterExpression = string.Format("TransportPartnerName = '{0}'", transportPartnerName);
                serviceBusSubscription.CreateIfNotExists(filterExpression);

                Console.WriteLine("done");

                // Create Service Identity
                DeleteServiceIdentity(formattedName);
                CreateServiceIdentityWithKey(formattedName);

                // Create Relying Party
                var subscriptionRealm = string.Format(
                    "http://{0}.servicebus.windows.net/{1}/subscriptions/{2}subscription", ServiceBusNamespace, TopicName, formattedName.ToLowerInvariant());
                CreateRelyingPartyWithDefaultRuleGroup(formattedName, subscriptionRealm);

                // Create Rule Group
                groupName = "Rule Group for " + formattedName;
                CreateRuleGroupWithListenRule(groupName, formattedName, formattedName);

                // Create Rule Group for OrderStatusUpdateQueue
                groupName = "Rule Group for Queue for " + formattedName;
                CreateRuleGroupWithSendRule(groupName, formattedName, ServiceBusQueueName);
            }

            // Create ServiceBus subscription for AuditLogListener
            SetupAuditLogListener();

            Console.WriteLine("done");

            // Setting up ACS for ServiceBus Topic
            var newOrdersJobName = ConfigurationManager.AppSettings["neworderjob.name"];
            DeleteServiceIdentity(newOrdersJobName.ToLowerInvariant());
            CreateServiceIdentityWithKey(newOrdersJobName.ToLowerInvariant());

            var topicRealm = string.Format("http://{0}.servicebus.windows.net/{1}", ServiceBusNamespace, TopicName.ToLowerInvariant());
            var ruleGroupName = "Rule Group for " + TopicName;
            CreateRelyingPartyWithDefaultRuleGroup(TopicName, topicRealm);
            CreateRuleGroupWithSendRule(ruleGroupName, newOrdersJobName, TopicName);

            // Setting up ACS for OrdersStatistics relayed service
            DeleteServiceIdentity("externaldataanalyzer");
            CreateServiceIdentityWithKey("externaldataanalyzer");
            DeleteServiceIdentity("headoffice");
            CreateServiceIdentityWithKey("headoffice");
            CreateRelyingPartyWithDefaultRuleGroup(
                "OrdersStatisticsService", string.Format("http://{0}.servicebus.windows.net/Services/RelayedOrdersStatistics", ServiceBusNamespace));
            CreateOrdersStatisticsServiceRules();
        }

        private static void SetupAuditLogListener()
        {
            var formattedName = AuditLogListener.Replace(" ", string.Empty).ToLowerInvariant();
            Console.Write(string.Format("Creating {0}Subscription...", formattedName));

            var serviceBusTopicDescription = new ServiceBusSubscriptionDescription
                {
                    Namespace = ServiceBusNamespace,
                    TopicName = TopicName,
                    SubscriptionName = string.Format("{0}Subscription", formattedName),
                    Issuer = Issuer,
                    DefaultKey = DefaultKey
                };

            var serviceBusSubscription = new ServiceBusSubscription(serviceBusTopicDescription);
            const int AuditAmount = 10000;
            var filterExpression = string.Format("OrderAmount > {0}", AuditAmount);
            serviceBusSubscription.CreateIfNotExists(filterExpression);

            Console.WriteLine("done");

            // Create Service Identity
            DeleteServiceIdentity(formattedName);
            CreateServiceIdentityWithKey(formattedName);

            // Create Relying Party
            var subscriptionRealm = string.Format(
                "http://{0}.servicebus.windows.net/{1}/subscriptions/{2}subscription", ServiceBusNamespace, TopicName, formattedName.ToLowerInvariant());
            CreateRelyingPartyWithDefaultRuleGroup(formattedName, subscriptionRealm);

            // Create Rule Group
            var groupName = "Rule Group for " + formattedName;
            CreateRuleGroupWithListenRule(groupName, formattedName, formattedName);
        }

        private static void CreateRuleGroupWithAccessRule(string ruleGroupName, string identityName, string relyingPartyName, string permission)
        {
            Console.Write(string.Format("Creating {0} Rule for {1} ...", permission, identityName));
            var settings = new AccessControlSettings(ServiceBusNamespace, DefaultKey);
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);

            serviceClient.DeleteRuleGroupByNameIfExists(ruleGroupName);
            serviceClient.SaveChanges(SaveChangesOptions.Batch);

            var ruleGroup = new RuleGroup { Name = ruleGroupName };
            serviceClient.AddToRuleGroups(ruleGroup);

            const string InputClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            var inputClaimValue = identityName.ToLowerInvariant();
            const string OutputClaimType = "net.windows.servicebus.action";
            var outputClaimValue = permission;

            // Equivalent to select Access Control Service as Input claim issuer in the Portal.
            var issuerByName = serviceClient.GetIssuerByName("LOCAL AUTHORITY");

            serviceClient.CreateRule(issuerByName, InputClaimType, inputClaimValue, OutputClaimType, outputClaimValue, ruleGroup, string.Empty);

            var relyingParty = serviceClient.GetRelyingPartyByName(relyingPartyName, true);
            var relyingPartyRuleGroup = new Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement.RelyingPartyRuleGroup();

            relyingParty.RelyingPartyRuleGroups.Add(relyingPartyRuleGroup);
            serviceClient.AddToRelyingPartyRuleGroups(relyingPartyRuleGroup);

            serviceClient.AddLink(relyingParty, "RelyingPartyRuleGroups", relyingPartyRuleGroup);
            serviceClient.AddLink(ruleGroup, "RelyingPartyRuleGroups", relyingPartyRuleGroup);

            serviceClient.SaveChanges(SaveChangesOptions.Batch);
            Console.WriteLine("done");
        }

        private static void CreateRuleGroupWithListenRule(string ruleGroupName, string identityName, string relyingPartyName)
        {
            CreateRuleGroupWithAccessRule(ruleGroupName, identityName, relyingPartyName, "Listen");
        }

        private static void CreateRuleGroupWithSendRule(string ruleGroupName, string identityName, string relyingPartyName)
        {
            CreateRuleGroupWithAccessRule(ruleGroupName, identityName, relyingPartyName, "Send");
        }

        private static void CreateRelyingPartyWithDefaultRuleGroup(string relyingPartyName, string realm)
        {
            Console.Write("Creating relying party for " + relyingPartyName + "...");
            var settings = new AccessControlSettings(ServiceBusNamespace, DefaultKey);
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);

            serviceClient.DeleteRelyingPartyByRealmIfExists(realm);
            serviceClient.SaveChanges();

            var relyingParty = new RelyingParty { Name = relyingPartyName, DisplayName = relyingPartyName, TokenType = "SWT", TokenLifetime = 1200, };

            // Add the relying party
            serviceClient.AddToRelyingParties(relyingParty);

            var relyingPartyAddress = new RelyingPartyAddress { Address = realm, EndpointType = "Realm" };

            // Add the realm address
            serviceClient.AddRelatedObject(relyingParty, "RelyingPartyAddresses", relyingPartyAddress);

            var defaultRuleGroup = serviceClient.RuleGroups.Where(rg => rg.Name == "Default Rule Group for ServiceBus").Single();

            var defaultRpRuleGroup = new Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement.RelyingPartyRuleGroup();
            serviceClient.AddToRelyingPartyRuleGroups(defaultRpRuleGroup);
            serviceClient.AddLink(relyingParty, "RelyingPartyRuleGroups", defaultRpRuleGroup);
            serviceClient.AddLink(defaultRuleGroup, "RelyingPartyRuleGroups", defaultRpRuleGroup);

            serviceClient.SaveChanges(SaveChangesOptions.Batch);
            Console.WriteLine("done");
        }

        private static void DeleteServiceIdentity(string name)
        {
            Console.Write("Delete Service Identity " + name + "...");
            var settings = new AccessControlSettings(ServiceBusNamespace, DefaultKey);
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);
            serviceClient.DeleteServiceIdentityIfExists(name);
            serviceClient.SaveChanges();
            Console.WriteLine("done");
        }

        private static void CreateServiceIdentityWithKey(string identityName)
        {
            Console.Write("Creating Service Identity: " + identityName + "...");
            var settings = new AccessControlSettings(ServiceBusNamespace, DefaultKey);
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);

            var keyAndPassword = ConfigurationManager.AppSettings[identityName.ToLowerInvariant() + ".key"];

            var bytes = Convert.FromBase64String(keyAndPassword);
            var encoded = Encoding.UTF8.GetBytes(keyAndPassword);

            var serviceIdentity = new ServiceIdentity { Name = identityName.ToLowerInvariant() };

            var key = new ServiceIdentityKey
                {
                    EndDate = DateTime.MaxValue.ToUniversalTime(),
                    StartDate = DateTime.UtcNow.ToUniversalTime(),
                    Type = "Symmetric",
                    Usage = "Signing",
                    Value = bytes,
                    DisplayName = string.Format(CultureInfo.InvariantCulture, "{0} key for {1}", "Symmetric", identityName)
                };

            var password = new ServiceIdentityKey
                {
                    EndDate = DateTime.MaxValue.ToUniversalTime(),
                    StartDate = DateTime.UtcNow.ToUniversalTime(),
                    Type = "Password",
                    Usage = "Password",
                    Value = encoded,
                    DisplayName = string.Format(CultureInfo.InvariantCulture, "{0} key for {1}", "Password", identityName)
                };

            serviceIdentity.ServiceIdentityKeys.Add(key);
            serviceIdentity.ServiceIdentityKeys.Add(password);

            serviceClient.AddToServiceIdentities(serviceIdentity);
            serviceClient.AddRelatedObject(serviceIdentity, "ServiceIdentityKeys", key);
            serviceClient.AddRelatedObject(serviceIdentity, "ServiceIdentityKeys", password);

            serviceClient.SaveChanges(SaveChangesOptions.Batch);

            Console.WriteLine("done");
        }

        #endregion

        #region ACS Setup

        private static void CleanupIdenityProviders(ServiceManagementWrapper acsWrapper)
        {
            Console.Write("Cleaning up identity providers...");

            var identityProviders = acsWrapper.RetrieveIdentityProviders();
            foreach (var provider in identityProviders)
            {
                if (provider.DisplayName != SocialIdentityProviders.WindowsLiveId.DisplayName)
                {
                    acsWrapper.RemoveIssuer(provider.DisplayName);
                }
            }

            Console.WriteLine("done");
        }

        private static void CleanupRelyingParties(ServiceManagementWrapper acsWrapper)
        {
            Console.Write("Cleaning up relying parties...");
            var rps = acsWrapper.RetrieveRelyingParties();
            foreach (var rp in rps)
            {
                if (rp.Name != "AccessControlManagement")
                {
                    acsWrapper.RemoveRelyingParty(rp.Name);
                }
            }

            Console.WriteLine("done");
        }

        private static void CreateGoogleRules(ServiceManagementWrapper acsWrapper, RelyingPartyRuleGroup defaultRuleGroup)
        {
            Console.Write("Creating Google mapping rules....");

            var identityProviderName = SocialIdentityProviders.Google.HomeRealm;

            // transform email as name
            acsWrapper.AddPassThroughRuleToRuleGroup(defaultRuleGroup.RuleGroup.Name, identityProviderName, ClaimTypes.Email, ClaimTypes.Name);

            Console.WriteLine("done.");
        }

        private static void CreateIdentityProviders(ServiceManagementWrapper acsWrapper)
        {
            acsWrapper.AddGoogleIdentityProvider();
            acsWrapper.AddYahooIdentityProvider();
        }

        private static void CreateOrderStatusUpdateQueueRelyingParty(ServiceManagementWrapper acsWrapper, string transportPartnerDisplayName, string transportPartnerSymmetricKey)
        {
            Console.Write(string.Format("Creating Order acknowledgement relying party for {0}...", transportPartnerDisplayName));

            var relyingPartyName = string.Format("TreyResearch Order Acknowledgement for {0}", transportPartnerDisplayName);
            var realm = string.Format("urn:{0}/{1}", ServiceBusQueueName, HttpUtility.UrlEncode(transportPartnerDisplayName));
            var ruleGroupName = string.Format("Default Rule Group for Trey Research order acknowledgement for {0}", transportPartnerDisplayName);
            acsWrapper.AddRelyingPartyWithSymmetricKey(
                relyingPartyName, realm, string.Empty, Convert.FromBase64String(transportPartnerSymmetricKey), TokenType.SWT, 1200, ruleGroupName, new[] { "LOCAL AUTHORITY" });

            Console.WriteLine("done");
        }

        private static void CreateContosoServiceIdentity(ServiceManagementWrapper acswrapper)
        {
            acswrapper.RemoveServiceIdentity(ContosoDisplayName);
            acswrapper.AddServiceIdentity(ContosoDisplayName, ContosoPassword);
        }

        private static void CreateFabrikamServiceIdentity(ServiceManagementWrapper acswrapper)
        {
            acswrapper.RemoveServiceIdentity(FabrikamDisplayName);
            acswrapper.AddServiceIdentity(FabrikamDisplayName, FabrikamPassword);
        }

        private static void CreateOrdersStatisticsServiceRules()
        {
            Console.Write("Creating Rules for OrdersStatisticsService...");
            var settings = new AccessControlSettings(ServiceBusNamespace, DefaultKey);
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);

            serviceClient.DeleteRuleGroupByNameIfExists("Rule group for OrdersStatisticsService");
            serviceClient.SaveChanges(SaveChangesOptions.Batch);

            var ruleGroup = new RuleGroup { Name = "Rule group for OrdersStatisticsService" };
            serviceClient.AddToRuleGroups(ruleGroup);

            // Equivalent to select Access Control Service as Input claim issuer in the Portal.
            var issuerByName = serviceClient.GetIssuerByName("LOCAL AUTHORITY");

            serviceClient.CreateRule(
                issuerByName,
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                "externaldataanalyzer",
                "net.windows.servicebus.action",
                "Send",
                ruleGroup,
                string.Empty);

            serviceClient.CreateRule(
                issuerByName, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "headoffice", "net.windows.servicebus.action", "Listen", ruleGroup, string.Empty);

            var relyingParty = serviceClient.GetRelyingPartyByName("OrdersStatisticsService", true);
            var relyingPartyRuleGroup = new Microsoft.ServiceBus.AccessControlExtensions.AccessControlManagement.RelyingPartyRuleGroup();

            relyingParty.RelyingPartyRuleGroups.Add(relyingPartyRuleGroup);
            serviceClient.AddToRelyingPartyRuleGroups(relyingPartyRuleGroup);

            serviceClient.AddLink(relyingParty, "RelyingPartyRuleGroups", relyingPartyRuleGroup);
            serviceClient.AddLink(ruleGroup, "RelyingPartyRuleGroups", relyingPartyRuleGroup);

            serviceClient.SaveChanges(SaveChangesOptions.Batch);
            Console.WriteLine("done");
        }

        private static void CreateOrderStatusUpdateQueueRules(ServiceManagementWrapper acsWrapper, string transportPartnerDisplayName)
        {
            var name = string.Format("TreyResearch Order Acknowledgement for {0}", transportPartnerDisplayName);

            var rp = acsWrapper.RetrieveRelyingParties().SingleOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (rp == null)
            {
                throw new Exception(string.Format("Could not retrieve relying party '{0}'", name));
            }

            var defaultRuleGroup = rp.RelyingPartyRuleGroups.FirstOrDefault();
            if (defaultRuleGroup == null)
            {
                throw new Exception(string.Format("Could not retrieve default rule group for relying party '{0}'", name));
            }

            acsWrapper.RemoveAllRulesInGroup(defaultRuleGroup.RuleGroup.Name);
            CreateDefaultRulesForTransportPartner(acsWrapper, defaultRuleGroup);
        }

        private static void CreateDefaultRulesForTransportPartner(ServiceManagementWrapper acsWrapper, RelyingPartyRuleGroup defaultRuleGroup)
        {
            acsWrapper.RetrieveRules(defaultRuleGroup.RuleGroup.Name);
            var localIssuer = acsWrapper.RetrieveIssuers().SingleOrDefault(i => i.Name.Equals("LOCAL AUTHORITY", StringComparison.OrdinalIgnoreCase));

            acsWrapper.AddPassThroughRuleToRuleGroup(defaultRuleGroup.RuleGroup.Name, localIssuer);
        }

        private static void CreateOrdersWebsiteRelyingParty(string[] identityProviders, ServiceManagementWrapper acsWrapper)
        {
            Console.Write("Creating Orders.Website relying party....");

            const string RuleGroup = "Default Rule Group for Orders.Website";
            acsWrapper.AddRelyingParty("Orders.Website", RealmAddress, ReplyAddress, null, null, null, RuleGroup, identityProviders);

            Console.WriteLine("done");
        }

        private static void CreateOrdersWebsiteRules(ServiceManagementWrapper acsWrapper)
        {
            // Create rules
            const string Name = "Orders.Website";

            var rp = acsWrapper.RetrieveRelyingParties().Single(r => r.Name == Name);
            if (rp == null)
            {
                throw new Exception(string.Format("Could not retrieve relying party '{0}'", Name));
            }

            var defaultRuleGroup = rp.RelyingPartyRuleGroups.FirstOrDefault();
            if (defaultRuleGroup == null)
            {
                throw new Exception(string.Format("Could not retrieve default rule group for relying party '{0}'", Name));
            }

            acsWrapper.RemoveAllRulesInGroup(defaultRuleGroup.RuleGroup.Name);

            // Social IP
            CreateGoogleRules(acsWrapper, defaultRuleGroup);
            CreateYahooRules(acsWrapper, defaultRuleGroup);
            CreateWindowsLiveRules(acsWrapper, defaultRuleGroup);
        }

        private static void CreateRelyingPartysWithRules(ServiceManagementWrapper acsWrapper)
        {
            // Create Orders.Website relying party
            var ips = new[] { SocialIdentityProviders.WindowsLiveId.DisplayName, SocialIdentityProviders.Google.DisplayName, SocialIdentityProviders.Yahoo.DisplayName };
            CreateOrdersWebsiteRelyingParty(ips, acsWrapper);
            CreateOrdersWebsiteRules(acsWrapper);

            // Create order acknowledgement relying party for Contoso and Fabrikam transport partners
            CreateOrderStatusUpdateQueueRelyingParty(acsWrapper, ContosoDisplayName, SymmetricKeyForContoso);
            CreateOrderStatusUpdateQueueRelyingParty(acsWrapper, FabrikamDisplayName, SymmetricKeyFabrikam);
            CreateContosoServiceIdentity(acsWrapper);
            CreateFabrikamServiceIdentity(acsWrapper);
            CreateOrderStatusUpdateQueueRules(acsWrapper, ContosoDisplayName);
            CreateOrderStatusUpdateQueueRules(acsWrapper, FabrikamDisplayName);
        }

        private static void CreateWindowsLiveRules(ServiceManagementWrapper acsWrapper, RelyingPartyRuleGroup defaultRuleGroup)
        {
            Console.Write("Creating Windows Live mapping rules....");

            var identityProviderName = SocialIdentityProviders.WindowsLiveId.DisplayName;

            // pass nameidentifier as name
            acsWrapper.AddPassThroughRuleToRuleGroup(defaultRuleGroup.RuleGroup.Name, identityProviderName, ClaimTypes.NameIdentifier, ClaimTypes.Name);

            Console.WriteLine("done.");
        }

        private static void CreateYahooRules(ServiceManagementWrapper acsWrapper, RelyingPartyRuleGroup defaultRuleGroup)
        {
            Console.Write("Creating Yahoo mapping rules....");

            var identityProviderName = SocialIdentityProviders.Yahoo.HomeRealm;

            // transform email as name
            acsWrapper.AddPassThroughRuleToRuleGroup(defaultRuleGroup.RuleGroup.Name, identityProviderName, ClaimTypes.Email, ClaimTypes.Name);

            Console.WriteLine("done.");
        }

        #endregion
    }
}