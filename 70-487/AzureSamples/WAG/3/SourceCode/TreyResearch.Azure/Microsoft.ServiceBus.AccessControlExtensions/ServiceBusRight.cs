//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


// 
// (c) Microsoft Corporation. All rights reserved.
// 
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    public abstract class ServiceBusRight : Right
    {
        public static readonly Right Listen = new ListenRight();
        public static readonly Right Manage = new ManageRight();
        public static readonly Right Send = new SendRight();

        public new static Right CreateGenericRight(string outputClaimType, string outputClaimValue)
        {
            if (outputClaimType == "net.windows.servicebus.action")
            {
                switch (outputClaimValue)
                {
                    case "Listen":
                        return Listen;
                    case "Send":
                        return Send;
                    case "Manage":
                        return Manage;
                }
            }
            return new GenericRight(outputClaimType, outputClaimValue);
        }

        sealed class ListenRight : ServiceBusRight
        {
            public override string ClaimType
            {
                get { return "net.windows.servicebus.action"; }
            }

            public override string ClaimValue
            {
                get { return "Listen"; }
            }
        }

        sealed class ManageRight : ServiceBusRight
        {
            public override string ClaimType
            {
                get { return "net.windows.servicebus.action"; }
            }

            public override string ClaimValue
            {
                get { return "Manage"; }
            }
        }

        sealed class SendRight : ServiceBusRight
        {
            public override string ClaimType
            {
                get { return "net.windows.servicebus.action"; }
            }

            public override string ClaimValue
            {
                get { return "Send"; }
            }
        }
    }
}