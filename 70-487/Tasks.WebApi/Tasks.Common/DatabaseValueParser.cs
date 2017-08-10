using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Common.Interface;

namespace Tasks.Common
{
    public sealed class DatabaseValueParser : IDatabaseValueParser
    {
        #region IDatabaseValueParser Members

        Guid IDatabaseValueParser.ParseGuid(object obj)
        {
            return Guid.Parse(obj.ToString());
        }

        long IDatabaseValueParser.ParseLong(object obj)
        {
            return long.Parse(obj.ToString());
        }

        DateTime IDatabaseValueParser.ParseDateTime(object obj)
        {
            return DateTime.Parse(obj.ToString());
        }

        Guid? IDatabaseValueParser.ParseGuidNullable(object obj)
        {
            return (obj == null || obj is DBNull) ?
                (Guid?)null :
                ((IDatabaseValueParser)this).ParseGuid(obj);
        }

        long? IDatabaseValueParser.ParseLongNullable(object obj)
        {
            return (obj == null || obj is DBNull) ?
                (long?)null :
                ((IDatabaseValueParser)this).ParseLong(obj);
        }

        DateTime? IDatabaseValueParser.ParseDateTimeNullable(object obj)
        {
            return (obj == null || obj is DBNull) ?
                (DateTime?)null :
                ((IDatabaseValueParser)this).ParseDateTime(obj);
        }

        string IDatabaseValueParser.ParseString(object obj)
        {
            return (obj == null || obj is DBNull) ?
                null :
                obj.ToString();
        }

        byte[] IDatabaseValueParser.ParseByteArray(object obj)
        {
            return (obj == null || obj is DBNull) ?
                null :
                (byte[]) obj;
        }

        #endregion
    }
}
