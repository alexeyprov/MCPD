//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Shared.Communication
{
    public class ServiceBusTopicDescription
    {
        public string Namespace { get; set; }
        public string TopicName { get; set; }
        public string DefaultKey { get; set; }
        public string Issuer { get; set; }
    }
}
