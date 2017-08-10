using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Common.Interface
{
    public interface IDatabaseValueParser
    {
        Guid ParseGuid(object obj);

        long ParseLong(object obj);

        DateTime ParseDateTime(object obj);

        Guid? ParseGuidNullable(object obj);

        long? ParseLongNullable(object obj);

        DateTime? ParseDateTimeNullable(object obj);

        string ParseString(object obj);

        byte[] ParseByteArray(object obj);
    }

}
