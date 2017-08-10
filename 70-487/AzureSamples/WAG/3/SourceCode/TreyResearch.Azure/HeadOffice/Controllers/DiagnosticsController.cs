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
    using System.Data.Services.Client;
    using System.Globalization;
    using System.Linq;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using HeadOffice.DataStores;
    using HeadOffice.Models;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using RetryPolicy = Microsoft.Practices.TransientFaultHandling.RetryPolicy;

    [HandleError]
    public class DiagnosticsController : Controller
    {
        private readonly IDiagnosticsLogStore store;
        private readonly RetryPolicy storageRetryPolicy;

        public DiagnosticsController()
            : this(new DiagnosticsLogStore())
        {
        }

        public DiagnosticsController(IDiagnosticsLogStore store)
        {
            this.store = store;
            this.storageRetryPolicy = RetryPolicyFactory.GetDefaultAzureStorageRetryPolicy();
        }

        public ActionResult Index(string message)
        {
            ViewData["message"] = message;
            var logs = this.store.FindAll();
            return View(logs);
        }

        [HttpPost]
        public ActionResult TransferLogs(FormCollection formCollection)
        {
            var message = string.Empty;
            var deleteEntries = formCollection.GetValue("deleteEntries") != null;
            var dataCenters = WebConfigurationManager.AppSettings["dataCenters"].Split(',');

            try
            {
                if (dataCenters[0].Equals("StorageEmulator", StringComparison.OrdinalIgnoreCase))
                {
                    this.TransferLogs(dataCenters[0], CloudStorageAccount.DevelopmentStorageAccount, deleteEntries);
                }
                else
                {
                    var dataCenters2 = dataCenters.Select(dc => dc.Trim()).Where(dc => !string.IsNullOrEmpty(dc.Trim()));

                    var accountNames = dataCenters2.Select(
                        dc => string.Format(CultureInfo.InvariantCulture, "diagnosticsStorageAccountName.{0}", dc));
                    var accountKeys = dataCenters2.Select(
                        dc => string.Format(CultureInfo.InvariantCulture, "diagnosticsStorageAccountKey.{0}", dc));

                    for (var i = 0; i < dataCenters2.Count(); i++)
                    {
                        var cred = new StorageCredentialsAccountAndKey(
                                WebConfigurationManager.AppSettings[accountNames.ElementAt(i)],
                                WebConfigurationManager.AppSettings[accountKeys.ElementAt(i)]);
                        var storageAccount = new CloudStorageAccount(cred, true);

                        this.TransferLogs(dataCenters2.ElementAt(i), storageAccount, deleteEntries);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                message = ex.Message;
                return RedirectToAction("Index", new { message });
            }

            return RedirectToAction("Index");           
        }

        private void TransferLogs(string dataCenter, CloudStorageAccount storageAccount, bool deleteWadLogsTableEntries)
        {
            var tableStorage = storageAccount.CreateCloudTableClient();

            if (!tableStorage.DoesTableExist("WADLogsTable"))
            {
                throw new ApplicationException("WADLogsTable is unavailable.  Please try again in a few moments or try generating some entries by using the Orders Website.");
            }

            var context = tableStorage.GetDataServiceContext();
        
            if (!deleteWadLogsTableEntries)
            {
                context.MergeOption = MergeOption.NoTracking;
            }

            IQueryable<WadLog> query = this.storageRetryPolicy.ExecuteAction(() => context.CreateQuery<WadLog>("WADLogsTable"));

            foreach (var logEntry in query)
            {
                var diagLog = new DiagnosticsLog
                                        {
                                            Id = Guid.NewGuid(),
                                            PartitionKey = logEntry.PartitionKey,
                                            RowKey = logEntry.RowKey,
                                            DeploymentId = logEntry.DeploymentId,
                                            DataCenter = dataCenter,
                                            Role = logEntry.Role,
                                            RoleInstance = logEntry.RoleInstance,
                                            Message = logEntry.Message,
                                            TimeStamp = logEntry.Timestamp
                                        };
                this.store.Save(diagLog);

                if (deleteWadLogsTableEntries)
                {
                    context.DeleteObject(logEntry);
                    this.storageRetryPolicy.ExecuteAction(() => context.SaveChanges());
                }
            }
        }
    }
}
