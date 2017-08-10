using System;
using System.Diagnostics;

class TestEventLog : IDisposable
{
    const string SOURCE_NAME = "Test App";
    const string LOG_NAME = "Application";
    EventLog _log;
    
    static void Main()
    {
        using (TestEventLog test = new TestEventLog())
        {
            test.WriteRandomMessage();
            test.PrintLogContents();
        }
    }

    TestEventLog()
    {
        if (!EventLog.SourceExists(SOURCE_NAME))
        {
            EventLog.CreateEventSource(SOURCE_NAME, LOG_NAME);
        }
        _log = new EventLog(LOG_NAME);
        _log.Source = SOURCE_NAME;
    }

    void PrintLogContents()
    {
        foreach (EventLogEntry e in _log.Entries)
        {
            if (SOURCE_NAME == e.Source)
            {
                Console.WriteLine("{0:G} >>> {1}", e.TimeGenerated, e.Message);
            }
        }
    }

    void WriteRandomMessage()
    {
        string[] dow = {"Monday", "Tuesday", "Wednesday", "Thursday",
            "Friday", "Saturdy", "Sunday"};
        string[] rating = {"bad", "so-so", "good"};
        
        Random r = new Random();
        string msg = String.Format("{0} is {1}",
            dow[r.Next(dow.Length)],
            rating[r.Next(rating.Length)]);
        
        int id = r.Next(short.MaxValue);
        short category = (short) r.Next(short.MaxValue);
        
        _log.WriteEntry(msg, EventLogEntryType.Information, id, category);
    }
    
    
    public void Dispose()
    {
        if (_log != null)
        {
            _log.Close();
        }
    }
}