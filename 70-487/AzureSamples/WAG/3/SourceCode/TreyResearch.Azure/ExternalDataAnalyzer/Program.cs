//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace ExternalDataAnalyzer
{
    using System;
    using System.Globalization;
    using System.ServiceModel;
    using Microsoft.ServiceBus;

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Configuring service details.");

                string serviceNamespace = System.Configuration.ConfigurationManager.AppSettings["serviceBusNamespace"];
                string uriScheme = System.Configuration.ConfigurationManager.AppSettings["UriScheme"];
                string servicePath = System.Configuration.ConfigurationManager.AppSettings["ServicePath"];

                var serviceUri = ServiceBusEnvironment.CreateServiceUri(uriScheme, serviceNamespace, servicePath);

                var channelFactory = new ChannelFactory<IOrdersStatisticsChannel>("RelayEndpoint", new EndpointAddress(serviceUri));

                Console.WriteLine("Connecting to the Service Bus.");

                var channel = channelFactory.CreateChannel();
                channel.Open();

                Console.WriteLine("Connected to: " + serviceUri);

                while (true)
                { 
                    Console.WriteLine("Sales by [R]egion or [Q]uarter? [Enter] to exit");
                    var keyInput = Console.ReadKey();
                    Console.WriteLine();

                    if (keyInput.Key == ConsoleKey.Enter)
                    {
                        channel.Close();
                        channelFactory.Close();

                        Environment.Exit(0);
                    }

                    switch (keyInput.KeyChar)
                    {
                        case 'R':
                        case 'r':
                            {
                                SalesByRegion(channelFactory, channel);
                                break;
                            }
                        case 'Q':
                        case 'q':
                            {
                                SalesByQuarter(channelFactory, channel);
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: \n{0}", ex);
                Console.WriteLine("Press [Enter] to exit");
                Console.ReadLine();
            }
        }

        private static void SalesByQuarter(ChannelFactory<IOrdersStatisticsChannel> channelFactory, IOrdersStatisticsChannel channel)
        {
            Console.WriteLine("Choose a quarter: [1]st, [2]nd, [3],rd [4]th or [Enter] to exit");
            var keyQuarter = Console.ReadKey();

            if (keyQuarter.Key == ConsoleKey.Enter)
            {
                channel.Close();
                channelFactory.Close();

                Environment.Exit(0);
            }

            var salesByQuarter = channel.SalesByQuarter(int.Parse(keyQuarter.KeyChar.ToString(CultureInfo.CurrentCulture)) - 1);
            Console.WriteLine();
            Console.WriteLine(string.Format("Sales by quarter: {0}", salesByQuarter));
        }

        private static void SalesByRegion(ChannelFactory<IOrdersStatisticsChannel> channelFactory, IOrdersStatisticsChannel channel)
        {
            Console.WriteLine("Choose a region: [W]est, [C]entral, [E]ast or [Enter] to exit");
            var keyRegion = Console.ReadKey();

            if (keyRegion.Key == ConsoleKey.Enter)
            {
                channel.Close();
                channelFactory.Close();

                Environment.Exit(0);
            }

            double salesByRegion = 0;

            switch (keyRegion.KeyChar)
            {
                case 'W':
                case 'w':
                    {
                        salesByRegion = channel.SalesByRegion("West");
                        break;
                    }
                case 'E':
                case 'e':
                    {
                        salesByRegion = channel.SalesByRegion("East");
                        break;
                    }
                case 'C':
                case 'c':
                    {
                        salesByRegion = channel.SalesByRegion("Central");
                        break;
                    }
            }

            Console.WriteLine();
            Console.WriteLine(string.Format("Sales by region: {0}", salesByRegion));
        }
    }
}
