using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CorrManagerTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string ID = "Primary Thread";
            Trace.CorrelationManager.StartLogicalOperation(ID);
            Trace.TraceInformation("(in main), stack = {0}", PrintLogicalStack());

            Task t = Task.Factory.StartNew(ThreadProc);
            t.Wait();

            Trace.CorrelationManager.StopLogicalOperation();
        }

        private static void ThreadProc()
        {
            const string ID = "Secondary Thread";
            Trace.CorrelationManager.StartLogicalOperation(ID);
            Trace.TraceInformation("(in thread proc), stack = {0}", PrintLogicalStack());
            Trace.CorrelationManager.StopLogicalOperation();
        }

        private static string PrintLogicalStack()
        {
            return string.Join(
                ",",
                from object f in Trace.CorrelationManager.LogicalOperationStack
                select f.ToString());
        }
    }
}
