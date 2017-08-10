using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace LoggerService.Facade
{
	public class Logger : 
		MarshalByRefObject,
		ILogger
	{
		static ILogger _realLogger;

		public static ILogger RealLogger
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			private get
			{
				return _realLogger;
			}

			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				_realLogger = value;
			}
		}

		#region ILogger Members
		public void WriteMessage(string msg)
		{
			ILogger log = RealLogger;
			if (log != null)
			{
				log.WriteMessage(msg);
			}
		}
		#endregion
	}
}
