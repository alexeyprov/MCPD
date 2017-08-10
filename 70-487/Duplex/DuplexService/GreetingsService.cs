using System;
using System.ServiceModel;

namespace DuplexService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
	public class GreetingsService : IGreetingsService
	{
		#region IGreetingsService Members

		void IGreetingsService.RequestGreeting(string name)
		{
			string greeting = String.Format("Hola, {0}", name);
			
			IGreetingsCallback callback = OperationContext.Current.GetCallbackChannel<IGreetingsCallback>();
			callback.GreetingGenerated(greeting);
		}

		#endregion
	}
}
