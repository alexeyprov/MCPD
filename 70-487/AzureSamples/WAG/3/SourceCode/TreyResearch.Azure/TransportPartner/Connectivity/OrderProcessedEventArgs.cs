//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace TransportPartner.Connectivity
{
    using System;

    public class OrderProcessedEventArgs : EventArgs
    {
        public ActiveOrder ActiveOrder { get; set; }
    }
}
