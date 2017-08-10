using System.ServiceModel;
using System.Threading.Tasks;

namespace DuplexService
{
    [ServiceContract(Name="StockQuoteCallback", Namespace="http://alexeypr.com/2015/05/Tasks")]
    public interface IStockQuoteCallback
    {
        [OperationContract(IsOneWay=true)]
        Task UpdateQuote(string ticker, decimal price);
    }
}
