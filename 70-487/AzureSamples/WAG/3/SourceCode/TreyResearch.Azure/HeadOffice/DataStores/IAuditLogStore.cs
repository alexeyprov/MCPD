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

    public interface IAuditLogStore
    {
        IEnumerable<Models.AuditLog> FindAll();
        void Save(Models.AuditLog auditLog);
    }
}
