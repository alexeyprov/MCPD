using System;
using System.Threading;

namespace DuplexClient
{
	class Program
	{
		private static void Main()
		{
			Console.Write("Enter your name: ");
			string name = Console.ReadLine();

			EventWaitHandle @event = new AutoResetEvent(false);

			using (GreetingsAgent serviceAgent = new GreetingsAgent(@event))
			{
				serviceAgent.RequestGreeting(name);

				@event.WaitOne();
			}
		}
	}
}
