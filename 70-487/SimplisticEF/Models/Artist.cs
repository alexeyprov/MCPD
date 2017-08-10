using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimplisticEF.Models
{
    public class Artist : BaseMusicEntity
    {
        public int ArtistId
        {
            get;
            set;
        }

        public virtual ICollection<Album> Albums
        {
            get;
            set;
        }

        public ArtistDetail Detail
        {
            get;
            set;
        }

        [Timestamp]
        public byte[] RowVersion
        {
            get;
            set;
        }
    }
}