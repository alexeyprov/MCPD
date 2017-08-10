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

    public class DiagnosticsLogStore : IDiagnosticsLogStore
    {
        public IEnumerable<Models.DiagnosticsLog> FindAll()
        {
            using (var database = new TreyResearchDataModelContainer())
            {
                return database.DiagnosticsLogs.Select(l => new Models.DiagnosticsLog
                {
                    DataCenter = l.DataCenter,
                    DeploymentId = l.DeploymentId,
                    Id = l.Id,
                    Message = l.Message,
                    PartitionKey = l.PartitionKey,
                    Role = l.Role,
                    RoleInstance = l.RoleInstance,
                    RowKey = l.RowKey,
                    TimeStamp = l.TimeStamp
                }).ToList();
            }
        }

        public void Save(Models.DiagnosticsLog diagnosticsLog)
        {
            using (var database = new TreyResearchDataModelContainer())
            {
                var logToSave = new DiagnosticsLog
                {
                    DataCenter = diagnosticsLog.DataCenter,
                    DeploymentId = diagnosticsLog.DeploymentId,
                    Id = diagnosticsLog.Id,
                    Message = diagnosticsLog.Message,
                    PartitionKey = diagnosticsLog.PartitionKey,
                    Role = diagnosticsLog.Role,
                    RoleInstance = diagnosticsLog.RoleInstance,
                    RowKey = diagnosticsLog.RowKey,
                    TimeStamp = diagnosticsLog.TimeStamp
                };

                database.DiagnosticsLogs.AddObject(logToSave);
                database.SaveChanges();
            }
        }
    }
}