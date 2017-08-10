using System.ServiceModel;
using System.Threading.Tasks;

namespace DuplexService
{
    [ServiceContract(Name = "StockQuoteService", Namespace = "http://alexeypr.com/2015/06/Duplex", CallbackContract=typeof(IStockQuoteCallback))]
    public interface IStockQuoteService
    {
        [OperationContract(IsOneWay=true)]
        Task Subscribe(string ticker); 
    }
}
