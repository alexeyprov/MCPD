//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace HeadOffice.Controllers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using HeadOffice.DataStores;
    using HeadOffice.Models;
    using Orders.Shared.Communication;
    using Orders.Shared.Communication.Messages;

    [HandleError]
    public class AuditController : Controller
    {
        private readonly IAuditLogStore auditLogStore;

        public AuditController() : this(new AuditLogStore())
        {
        }

        public AuditController(IAuditLogStore auditLogStore)
        {
            this.auditLogStore = auditLogStore;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var logs = this.auditLogStore.FindAll();

            ViewData["processing"] = MvcApplication.TokenSourceList.ContainsKey(Session.SessionID);
            return View(logs);
        }

        [HttpPost]
        public ActionResult DownloadLogs()
        {
            CancellationTokenSource tokenSource;
            if (MvcApplication.TokenSourceList.ContainsKey(Session.SessionID))
            {
                tokenSource = MvcApplication.TokenSourceList[Session.SessionID];
            }
            else
            {
                tokenSource = new CancellationTokenSource();
                MvcApplication.TokenSourceList.Add(Session.SessionID, tokenSource);
            }

            var serviceBusNamespaces = WebConfigurationManager.AppSettings["AuditServiceBusList"].Split(',').ToList();
            var context = TaskScheduler.Current;

            foreach (var serviceBusNamespace in serviceBusNamespaces)
            {
                // Connect to servicebus, download messages from the Audit log subscription, save to database.
                var serviceBusTopicDescription = new ServiceBusSubscriptionDescription
                {
                    Namespace = serviceBusNamespace,
                    TopicName = WebConfigurationManager.AppSettings["topicName"],
                    SubscriptionName = WebConfigurationManager.AppSettings["subscriptionName"],
                    Issuer = WebConfigurationManager.AppSettings["issuer"],
                    DefaultKey = WebConfigurationManager.AppSettings["defaultKey"]
                };

                var serviceBusSubscription = new ServiceBusSubscription(serviceBusTopicDescription);

                // MessagePollingInterval should be configured taking into consideration variables such as CPU processing power, 
                // expected volume of orders to process and number of worker role instances
                var receiverHandler = new ServiceBusReceiverHandler<NewOrderMessage>(serviceBusSubscription.GetReceiver()) { MessagePollingInterval = TimeSpan.FromSeconds(2) };

                receiverHandler.ProcessMessages(
                    (message, queueDescription, token) =>
                    {
                        return Task.Factory.StartNew(
                            () => this.ProcessMessage(message, queueDescription),
                            Task.Factory.CancellationToken,
                            TaskCreationOptions.None,
                            context);
                    },
                    tokenSource.Token);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CancelDownload()
        {
            if (MvcApplication.TokenSourceList.ContainsKey(Session.SessionID))
            {
                MvcApplication.TokenSourceList[Session.SessionID].Cancel();
                MvcApplication.TokenSourceList.Remove(Session.SessionID);
            }

            return RedirectToAction("Index");
        }

        [NonAction]
        public void ProcessMessage(NewOrderMessage message, ServiceBusQueueDescription queueDescription)
        {
            // Save the AuditLog to the database
            var auditLog = new AuditLog
            {
                OrderId = message.OrderId,
                OrderDate = message.OrderDate,
                Amount = Convert.ToDecimal(message.Amount),
                CustomerName = message.CustomerName
            };

            this.auditLogStore.Save(auditLog);
        }
    }
}
