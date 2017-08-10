using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoggerService.Facade
{
	public interface ILogger
	{
		void WriteMessage(string msg);
	}
}
