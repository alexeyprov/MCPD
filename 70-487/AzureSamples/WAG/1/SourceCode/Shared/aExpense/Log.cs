//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense
{
    using System.Diagnostics;
    using Microsoft.Practices.EnterpriseLibrary.Logging;

    public static class Log
    {
        public static void Write(EventKind eventKind, string message, params object[] args)
        {
            var logEntry = new LogEntry { Message = string.Format(System.Globalization.CultureInfo.InvariantCulture, message, args) };

            switch (eventKind)
            {
                case EventKind.Error:
                case EventKind.Critical:
                    logEntry.Severity = TraceEventType.Critical;
                    Logger.Write(logEntry);
                    break;
                case EventKind.Warning:
                    logEntry.Severity = TraceEventType.Warning;
                    Logger.Write(logEntry);
                    break;
                case EventKind.Information:
                case EventKind.Verbose:
                    logEntry.Severity = TraceEventType.Verbose;
                   Logger.Write(logEntry);
                    break;
            }
        }
    }
}