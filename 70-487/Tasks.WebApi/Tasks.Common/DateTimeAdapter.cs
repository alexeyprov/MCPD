using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Common.Interface;

namespace Tasks.Common
{
    public class DateTimeAdapter : IDateTime
    {
        #region IDateTime Members

        DateTime IDateTime.UtcNow
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        #endregion
    }
}
