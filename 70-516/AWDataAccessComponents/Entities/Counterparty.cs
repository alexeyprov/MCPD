//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdventureWorks.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public abstract partial class Counterparty
    {
        public Counterparty()
        {
            this.CounterpartyAddresses = new HashSet<CounterpartyAddress>();
        }
    
        public int CounterpartyId { get; set; }
        public string AccountNumber { get; set; }
    
        public virtual ICollection<CounterpartyAddress> CounterpartyAddresses { get; set; }
    }
}
