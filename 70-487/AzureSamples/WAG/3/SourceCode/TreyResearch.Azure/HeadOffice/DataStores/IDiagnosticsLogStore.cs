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
    using Models;

    public interface IDiagnosticsLogStore
    {
        IEnumerable<DiagnosticsLog> FindAll();
        void Save(DiagnosticsLog diagnosticsLog);
    }
}
