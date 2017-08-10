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
    public abstract class Claim
    {
        public abstract string ClaimType { get; }
        public abstract string ClaimValue { get; }

        public override bool Equals(object obj)
        {
            return ((Claim) obj).ClaimType == this.ClaimType && ((Claim) obj).ClaimValue == this.ClaimValue;
        }

        public override int GetHashCode()
        {
            return this.ClaimType.GetHashCode() ^ this.ClaimValue.GetHashCode();
        }

        public static bool operator ==(Claim c1, Claim c2)
        {
            return Equals(c1, c2);
        }

        public static bool operator !=(Claim c1, Claim c2)
        {
            return !Equals(c1, c2);
        }
    }
}