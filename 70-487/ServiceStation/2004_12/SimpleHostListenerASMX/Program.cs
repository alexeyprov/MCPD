#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Net;
using System.IO;

#endregion

namespace SimpleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListenerWrapper lw = (HttpListenerWrapper)ApplicationHost.CreateApplicationHost(
                typeof(HttpListenerWrapper), "/", Directory.GetCurrentDirectory());

            string[] prefixes = new string[] {
                "http://localhost:8081/",
                "http://127.0.0.1:8081/"
            };
            lw.Configure(prefixes, "/", Directory.GetCurrentDirectory());
            lw.Start();
            Console.WriteLine("Listening for requests on http://localhost:8081/");
            while (true)
                lw.ProcessRequest();
        }
    }
}
