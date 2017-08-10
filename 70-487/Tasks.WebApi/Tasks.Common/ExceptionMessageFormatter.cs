using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Common.Interface;

namespace Tasks.Common
{
    public sealed class ExceptionMessageFormatter : IExceptionMessageFormatter
    {
        string IExceptionMessageFormatter.GetEntireExceptionStack(Exception ex)
        {
            StringBuilder message = new StringBuilder(ex.Message);
            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                message.Append(" --> ");
                message.Append(innerException.Message);

                innerException = innerException.InnerException;
            }

            return message.ToString();
        }
    }
}
