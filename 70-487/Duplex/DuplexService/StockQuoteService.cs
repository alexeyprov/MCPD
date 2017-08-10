using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DuplexService
{
    public sealed class StockQuoteService : IStockQuoteService
    {
        #region IStockQuoteService Members

        async Task IStockQuoteService.Subscribe(string ticker)
        {
            Random rnd = new Random();
            IStockQuoteCallback callback = OperationContext.Current.GetCallbackChannel<IStockQuoteCallback>();
            decimal price = rnd.Next(10, 100);

            while (((ICommunicationObject)callback).State == CommunicationState.Opened)
            {
                await callback.UpdateQuote(ticker, price);

                await Task.Delay(1000);
                price += (0.5M - (decimal)rnd.NextDouble()) * 2M;
            }
        }

        #endregion
    }
}
