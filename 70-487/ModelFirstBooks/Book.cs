//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModelFirstBooks
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        public int BookId { get; set; }
        public string Isbn { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public byte[] Rowversion { get; set; }
        public int AuthorId { get; set; }
    
        public virtual Author Author { get; set; }
    }
}
