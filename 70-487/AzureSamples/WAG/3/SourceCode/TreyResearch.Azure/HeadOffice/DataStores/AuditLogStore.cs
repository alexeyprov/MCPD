//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace HeadOffice.DataStores
{
    using System.Collections.Generic;
    using System.Linq;
    using HeadOffice.DataStores.Entities;

    public class AuditLogStore : IAuditLogStore
    {
        public IEnumerable<Models.AuditLog> FindAll()
        {
            using (var database = new TreyResearchDataModelContainer())
            {
                return database.AuditLog.Select(l => new Models.AuditLog
                { 
                    OrderId = l.OrderId,
                    OrderDate = l.OrderDate,
                    CustomerName = l.CustomerName,
                    Amount = l.Amount
                }).ToList();
            }
        }

        public void Save(Models.AuditLog auditLog)
        {
            using (var database = new TreyResearchDataModelContainer())
            {
                var exists = database.AuditLog.FirstOrDefault(l => l.OrderId == auditLog.OrderId) != null;
                
                if (exists)
                {
                    return;
                }

                var logToSave = new AuditLog
                {
                    OrderId = auditLog.OrderId,
                    OrderDate = auditLog.OrderDate,
                    Amount = auditLog.Amount,
                    CustomerName = auditLog.CustomerName
                };

                database.AuditLog.AddObject(logToSave);
                database.SaveChanges();
            }
        }
    }
}