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
    public enum IdentityProviderKeyType
    {
        ApplicationKey,
        Symmetric,
        X509Certificate
    }

    public enum IdentityProviderKeyUsage
    {
        ApplicationId,
        ApplicationSecret,
        Signing
    }

    public enum IdentityProviderProtocolType
    {
        Facebook,
        OpenId,
        WsFederation
    }

    public enum IdentityProviderEndpointType
    {
        EmailDomain,
        FedMetadataUrl,
        ImageUrl,
        SignIn,
        SignOut
    }

    public enum RelyingPartyTokenType
    {
        SAML_1_1,
        SAML_2_0,
        SWT
    } ;

    public enum RelyingPartyKeyType
    {
        Symmetric,
        X509Certificate
    } ;

    public enum RelyingPartyKeyUsage
    {
        Encrypting,
        Signing
    } ;

    public enum RelyingPartyAddressType
    {
        Error,
        Realm,
        Reply
    }

    public enum ServiceIdentityKeyType
    {
        Password,
        Symmetric,
        X509Certificate
    } ;

    /// <summary>
    ///   The valid list of key usages
    /// </summary>
    public enum ServiceIdentityKeyUsage
    {
        Password,
        Signing
    }

    public enum ServiceKeyType
    {
        X509Certificate,
        Password,
        Symmetric
    }

    public enum ServiceKeyUsage
    {
        Signing,
        Management,
        Encrypting,
    }
}