using System.ServiceModel;

namespace DuplexService
{
    [ServiceContract(Namespace = "DuplexService")]
    public interface IGreetingsCallback
    {
        [OperationContract(IsOneWay = true)]
        void GreetingGenerated(string greeting);
    }
}
