using System;
using System.Diagnostics;

class EventLogAcl
{
	private const string LOG_NAME = "AP Test Log";
	private const string USER_NAME = "Larisa";

	static void Main()
	{
		Debugger.Break();
		using (TempEventLog log = new TempEventLog(LOG_NAME))
		{
			log.WriteInfo("Start");
			EventLogSecurity sec = new EventLogSecurity(LOG_NAME);
			sec.AddUserToCustomSD(Environment.MachineName, USER_NAME,
				EventLogSecurity.CustomSD_READ_ACCESS);
			sec.RemoveUserFromCustomSD(Environment.MachineName, USER_NAME);
			log.WriteInfo("Finish");
		}
	}
}