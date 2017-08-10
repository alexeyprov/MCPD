using System;
using System.ServiceModel;
using System.Threading;
using DuplexClient.GreetingsWcfService;

namespace DuplexClient
{
	internal sealed class GreetingsAgent : 
		IGreetingsServiceCallback,
		IGreetingsService,
		IDisposable
	{
		private readonly GreetingsServiceClient _proxy;
		private readonly EventWaitHandle _completedEvent;

		public GreetingsAgent(EventWaitHandle completedEvent)
		{
			_completedEvent = completedEvent;
			
			_proxy = new GreetingsServiceClient(new InstanceContext(this));
		}

		#region IGreetingsServiceCallback Members

		void IGreetingsServiceCallback.GreetingGenerated(string greeting)
		{
			Console.WriteLine("Greeting generated: " + greeting);
			_completedEvent.Set();
		}

		#endregion

		#region IGreetingsService Members

		public void RequestGreeting(string name)
		{
			_proxy.RequestGreeting(name);
		}

		#endregion

		#region IDisposable Members

		void IDisposable.Dispose()
		{
			_completedEvent.Close();
			_proxy.Close();
		}

		#endregion
	}
}
