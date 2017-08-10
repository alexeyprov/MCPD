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
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Web;
    using System.ServiceModel.Syndication;
    
    [ServiceContract(Name = "SyndicationContract", Namespace = "http://samples.microsoft.com/ServiceModel/Relay/")]
    public interface SyndicationContract
    {
        [OperationContract, WebGet(UriTemplate = "/")]
        Rss20FeedFormatter GetFeed();
    }

    public interface SyndicationChannel : SyndicationContract, IClientChannel { }
}