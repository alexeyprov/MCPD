// © 2007-2008 Michele Leroux Bustamante. All rights reserved 
// Book: Learning WCF, O'Reilly
// Book Blog: www.thatindigogirl.com
// Michele's Blog: www.dasblonde.net
// IDesign: www.idesign.net
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace RouterHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = null;

            try
            {
                host = new ServiceHost(typeof(Router.RouterService));
                host.Faulted += new EventHandler(host_Faulted);
                host.Open();


                Console.WriteLine();
                Console.WriteLine("Press <ENTER> to terminate host.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                try
                {
                    if (host.State == CommunicationState.Faulted)
                        host.Abort();
                    else
                        host.Close();
                }
                catch 
                {
                }
            }
            Console.ReadLine();

        }

        static void host_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("ServiceHost faulted.");
        }
    }
}
