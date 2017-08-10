using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplisticEF.Models
{
    [Table("ArtistDetail", Schema="music")]
    public class ArtistDetail
    {
        [ForeignKey("Artist")]
        [Key]
        public int ArtistId
        {
            get;
            set;
        }

        public virtual Artist Artist
        {
            get;
            set;
        }

        public string Bio
        {
            get;
            set;
        }
    }
}