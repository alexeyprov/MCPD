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
    using System;
    using System.Linq;
    using AccessControlManagement;

    public static class NamespaceAccessControl
    {
        public static void ResetNamespace(Uri rpAddress, AccessControlSettings settings)
        {
            rpAddress = new UriBuilder(rpAddress) {Scheme = "http", Port = -1}.Uri;
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);
            foreach (RuleGroup g1 in from g in serviceClient.RuleGroups where g.Name.StartsWith(rpAddress.AbsoluteUri) select g)
            {
                serviceClient.DeleteRuleGroupByNameIfExists(g1.Name);
            }
            serviceClient.SaveChanges();
        }

        public static AccessControlList GetAccessControlList(Uri relyingPartyUri, AccessControlSettings settings)
        {
            string localPath = relyingPartyUri.LocalPath;
            relyingPartyUri =
                new UriBuilder(relyingPartyUri)
                    {Scheme = "http", Port = -1, Path = localPath.Substring(0, localPath.EndsWith("/") ? localPath.Length - 1 : localPath.Length)}.Uri;

            string relyingPartyAddress = relyingPartyUri.AbsoluteUri;
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(settings);
            RelyingPartyAddress longestPrefixRpAddress = GetLongestPrefixRelyingPartyAddress(serviceClient, relyingPartyAddress);
            if (longestPrefixRpAddress != null)
            {
                RelyingParty relyingParty = GetRelyingPartyByAddress(serviceClient, longestPrefixRpAddress);
                if (relyingParty != null)
                {
                    return new AccessControlList(relyingPartyUri, relyingParty, serviceClient);
                }
            }
            throw new InvalidOperationException();
        }

        static RelyingParty GetRelyingPartyByAddress(ManagementService serviceClient, RelyingPartyAddress longestPrefixRpAddress)
        {
            return (from rp in serviceClient.RelyingParties.Expand("RelyingPartyAddresses,RelyingPartyIdentityProviders,RelyingPartyRuleGroups")
                    where rp.Id == longestPrefixRpAddress.RelyingPartyId
                    select rp).FirstOrDefault();
        }

        static RelyingPartyAddress GetLongestPrefixRelyingPartyAddress(ManagementService serviceClient, string relyingPartyAddress)
        {
            return
                (from a in serviceClient.RelyingPartyAddresses where relyingPartyAddress.IndexOf(a.Address, StringComparison.Ordinal) == 0 select a).OrderByDescending(
                    a => a.Address.Length).FirstOrDefault();
        }
    }
}