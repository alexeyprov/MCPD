using System;
using System.ServiceModel;
using DuplexClient.StockQuoteWcfService;

namespace DuplexClient
{
    internal sealed class StockQuoteAgent : 
        StockQuoteService, 
        StockQuoteServiceCallback,
        IDisposable
    {
        private readonly StockQuoteServiceClient _proxy;

        public StockQuoteAgent()
        {
            _proxy = new StockQuoteServiceClient(new InstanceContext(this));
        }

        #region StockQuoteServiceCallback Members

        void StockQuoteServiceCallback.UpdateQuote(string ticker, decimal price)
        {
            Console.WriteLine(">>> {0} : {1:c}", ticker, price);
        }

        #endregion

        #region StockQuoteService Members

        public void Subscribe(string ticker)
        {
            _proxy.Subscribe(ticker);
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            try
            {
                _proxy.Close();
            }
            catch
            {
            }
        }

        #endregion
    }
}
