using CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Diagnostics;

namespace OrderProcessing
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Wait for services to start, then enter the desired degree of parallelism: ");
            var degreeOfParallellism = int.Parse(Console.ReadKey().KeyChar.ToString());
            Console.WriteLine();

            //define the blocks
            IPropagatorBlock<Order, Order> orderBuffer = new BufferBlock<Order>();
            IPropagatorBlock<Order, Order> broadcaster = new BroadcastBlock<Order>(order => order);
            IPropagatorBlock<Order, Order[]> upsBatcher = new BatchBlock<Order>(5);
            IPropagatorBlock<Order, Order[]> fedexBatcher = new BatchBlock<Order>(5);
            IPropagatorBlock<Order[], ItemTrackingDetail> upsProcessor = new TransformManyBlock<Order[], ItemTrackingDetail>(
                orders => PostToCarrierAsync(CarrierType.Ups, orders));
            IPropagatorBlock<Order[], ItemTrackingDetail> fedexProcessor = new TransformManyBlock<Order[], ItemTrackingDetail>(
                orders => PostToCarrierAsync(CarrierType.Fedex, orders));
            ITargetBlock<ItemTrackingDetail> storageProcessor = new ActionBlock<ItemTrackingDetail>(
                PersistToDatabase,
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = degreeOfParallellism
                });

            //link the blocks together
            orderBuffer.LinkTo(broadcaster);
            broadcaster.LinkTo(upsBatcher, order => order.Carrier == CarrierType.Ups);
            broadcaster.LinkTo(fedexBatcher, order => order.Carrier == CarrierType.Fedex);
            upsBatcher.LinkTo(upsProcessor);
            fedexBatcher.LinkTo(fedexProcessor);
            upsProcessor.LinkTo(storageProcessor);
            fedexProcessor.LinkTo(storageProcessor);

            //set the completion propagation rules
            orderBuffer.Completion.ContinueWith(t => broadcaster.Complete());
            broadcaster.Completion.ContinueWith(t =>
            {
                upsBatcher.Complete();
                fedexBatcher.Complete();
            });

            upsBatcher.Completion.ContinueWith(t => upsProcessor.Complete());
            fedexBatcher.Completion.ContinueWith(t => fedexProcessor.Complete());

            Action<Task> postOrderCompletion = t =>
            {
                Task.WaitAll(upsProcessor.Completion, fedexProcessor.Completion);
                storageProcessor.Complete();
            };
            upsProcessor.Completion.ContinueWith(postOrderCompletion);
            fedexProcessor.Completion.ContinueWith(postOrderCompletion);

            Stopwatch s = Stopwatch.StartNew();
            for (int i = 0; i < 25; i++)
            {
                orderBuffer.Post(
                    new Order
                    {
                        Carrier = i % 2 == 0 ? CarrierType.Ups : CarrierType.Fedex,
                        OrderId = Guid.NewGuid(),
                        Items = new List<OrderItem>()
                    });
            }

            orderBuffer.Complete();
            storageProcessor.Completion.Wait();
            s.Stop();

            Console.WriteLine("Processing completed in {0}. Press <Enter> to exit.", s.Elapsed);
            Console.ReadLine();
        }

        // Simulates transformation of a list of Order objects into a service api model (ShipDetail).
        private static List<ShipDetail> CreateShipDetails(IEnumerable<Order> orders)
        {
            IEnumerable<ShipDetail> shipDetails = orders.Select(order =>
                new ShipDetail
                {
                    ShipId = order.OrderId,
                    Items = order.Items.Select(item =>
                        new ShipItemInfo
                        {
                            Sku = item.Sku,
                        }).ToList()
                });

            return shipDetails.ToList();
        }

        // Sends orders to a shipping service endpoint dependent on the specified carrier
        private static async Task<IEnumerable<ItemTrackingDetail>> PostToCarrierAsync(CarrierType carrierType, Order[] orders)
        {
            List<ShipDetail> shipDetails = CreateShipDetails(orders);
            Console.WriteLine("Sending {0} orders to {1}.", orders.Length, carrierType);

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(
                    "http://localhost:9099/api/" + carrierType.ToString(),
                    shipDetails,
                    new JsonMediaTypeFormatter());

                if (!response.IsSuccessStatusCode)
                    throw new ApplicationException("Exception in shipping processor (" + carrierType + "): " + response.ReasonPhrase);

                return await response.Content.ReadAsAsync<List<ItemTrackingDetail>>();
            }
        }

        // Simulates persistence of tracking numbers for each item to a database.
        private static async Task PersistToDatabase(ItemTrackingDetail itemTrackingDetail)
        {
            // ...  your DB code here
            //Simulate updating the order to the database.
            await Task.Delay(50); 
            Console.WriteLine("Wrote tracking details to DB for order.", itemTrackingDetail.ShippingId);
        }
    }
}
