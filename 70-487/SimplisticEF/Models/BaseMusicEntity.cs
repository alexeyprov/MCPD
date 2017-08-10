using System.ComponentModel.DataAnnotations;

namespace SimplisticEF.Models
{
    public abstract class BaseMusicEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }
    }
}