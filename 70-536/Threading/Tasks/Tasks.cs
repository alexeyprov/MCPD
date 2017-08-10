using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

internal static class Program
{
	private static void Main()
	{
		PrintThreadInfo("Main");
		TestFactory();
	}

	private static void TestFactory()
	{                               
		Task parent = new Task(() =>
			{
				PrintThreadInfo("Parent task");

				CancellationTokenSource tokens = new CancellationTokenSource();
				TaskFactory<int> factory = new TaskFactory<int>(
					tokens.Token,
					TaskCreationOptions.AttachedToParent,
					TaskContinuationOptions.ExecuteSynchronously,
					TaskScheduler.Default);

				Task<int>[] childTasks = new Task<int>[]
				{
					factory.StartNew(() => Sum(1000, tokens.Token)),
					factory.StartNew(() => Sum(2000, tokens.Token)),
					factory.StartNew(() => Sum(Int32.MaxValue, tokens.Token))
				};

				// if one task fails, all others should be canceled
				foreach (Task<int> child in childTasks)
				{
					child.ContinueWith(
						t => 
						{
							PrintThreadInfo("Cancellation from faulted child task");
							tokens.Cancel();
						}, 
						TaskContinuationOptions.OnlyOnFaulted);
				}

				// When all children are done, get the maximum value returned from the
				// non-faulting/canceled tasks. Then pass the maximum value to another
				// task which displays the maximum result
				factory
					.ContinueWhenAll(
						childTasks,
						tasks => 
						{
							PrintThreadInfo("Children completed task");
							return tasks.Where(t => TaskStatus.RanToCompletion == t.Status).Max(t => t.Result);
						},
						CancellationToken.None)
					.ContinueWith(
						t => 
						{
							PrintThreadInfo("Children completed continuation");
							Console.WriteLine("Maximum value: " + t.Result);
						}); 
						//TaskContinuationOptions.ExecuteSynchronously);
			});


		// When the children are done, show any unhandled exceptions too
		parent.ContinueWith(t =>
			{
				PrintThreadInfo("Parent continuation");

				Console.WriteLine(
					"The following exceptions occured:\n  " +
					string.Join(
						"\n  ",
						t.Exception.Flatten().InnerExceptions.Select(e => e.GetType())));
			},
			TaskContinuationOptions.OnlyOnFaulted);

		parent.Start();

		Console.ReadLine();
	}

	private static int Sum(int threshold, CancellationToken token)
	{
		PrintThreadInfo(string.Format("Sum({0})", threshold));

		int result = 0;
		for (int i = threshold; i > 0; --i)
		{
			checked
			{
				result += i;
			}
			token.ThrowIfCancellationRequested();
		}

		return result;
	}

	private static void PrintThreadInfo(string location)
	{
		Console.WriteLine("[{0}]: {1}", Thread.CurrentThread.ManagedThreadId, location);
	}
}