using System.Collections.Generic;

namespace SimplisticEF.Models
{
    public class Reviewer : BaseMusicEntity
    {
        public int ReviewerId
        {
            get;
            set;
        }

        public virtual ICollection<Album> Albums
        {
            get;
            set;
        }
    }
}