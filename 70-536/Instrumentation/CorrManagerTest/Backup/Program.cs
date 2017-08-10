using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace CorrManagerTest
{
	class Program
	{
		static void Main(string[] args)
		{
			const string ID = "Primary Thread";
			Trace.CorrelationManager.StartLogicalOperation(ID);
			Trace.TraceInformation("(in main), stack = {0}", PrintLogicalStack());
			ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc));
			Thread.Sleep(1000);
			Trace.CorrelationManager.StopLogicalOperation();
		}

		static void ThreadProc(object state)
		{
			const string ID = "Secondary Thread";
			Trace.CorrelationManager.StartLogicalOperation(ID);
			Trace.TraceInformation("(in thread proc), stack = {0}", PrintLogicalStack());
			Trace.CorrelationManager.StopLogicalOperation();
		}

		static string PrintLogicalStack()
		{
			string[] frames = Array.ConvertAll(Trace.CorrelationManager.LogicalOperationStack.ToArray(),
				new Converter<object, string>(Convert.ToString));
			return String.Join(",", frames);
		}
	}
}
