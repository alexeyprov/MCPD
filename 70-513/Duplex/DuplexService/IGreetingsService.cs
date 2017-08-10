using System.ServiceModel;

namespace DuplexService
{
	[ServiceContract(Namespace = "DuplexService", CallbackContract = typeof(IGreetingsCallback))]
	public interface IGreetingsService
	{
		[OperationContract(IsOneWay = true)]
		void RequestGreeting(string name);
	}
}
