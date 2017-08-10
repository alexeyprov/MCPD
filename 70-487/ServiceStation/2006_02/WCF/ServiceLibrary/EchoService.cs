using System.ServiceModel;

namespace ServiceLibrary
{
    [ServiceContract(Namespace="http://example.org/echo/")]
    public interface IEchoService
    {
        [OperationContract]
        string Echo(string msg);
    }

    [ServiceBehavior(
        InstanceContextMode=InstanceContextMode.Single,
        ConcurrencyMode=ConcurrencyMode.Multiple)]
    public class EchoService : IEchoService
    {
        #region IEchoService Members

        public string Echo(string msg)
        {
            return msg;
        }
        #endregion
    }
}
