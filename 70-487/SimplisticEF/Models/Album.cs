using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimplisticEF.Models
{
    public class Album
    {
        public int AlbumId
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        public string Name
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public DateTime? ReleaseDate
        {
            get;
            set;
        }

        [ConcurrencyCheck]
        public int Version
        {
            get;
            set;
        }

        public virtual Artist Artist
        {
            get;
            set;
        }

        public virtual ICollection<Reviewer> Reviewers
        {
            get;
            set;
        }
    }
}