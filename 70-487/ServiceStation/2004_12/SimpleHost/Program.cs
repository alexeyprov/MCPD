#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.IO;

#endregion

namespace SimpleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: simplehost filename [querystring]");
                return;
            }
            string file = args[0];
            string query = (args.Length > 1) ? args[1] : "";

            MySimpleHost msh = (MySimpleHost)
                ApplicationHost.CreateApplicationHost(
                   typeof(MySimpleHost), "/", 
                   Directory.GetCurrentDirectory());
            msh.ProcessRequest(file, query);
        }
    }

    public class MySimpleHost : MarshalByRefObject
    {
        public void ProcessRequest(string file, string query)
        {
            SimpleWorkerRequest swr = 
                new SimpleWorkerRequest(file, query, Console.Out);
            HttpRuntime.ProcessRequest(swr);
        }
    }
}
