#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Net;
using System.Web.Hosting;
using HttpListenerLibrary;
#endregion

namespace HostingASMX
{
    public partial class ASMXService : ServiceBase
    {
        private HttpListenerController _controller;

        public ASMXService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string[] prefixes = new string[] {
                "http://localhost:8081/asmxsvc/",
                "http://127.0.0.1:8081/asmxsvc/",
            };
            _controller = new HttpListenerController(prefixes, "/asmxsvc", @"C:\ServiceStation\HostingASMX\ASMXEndpoints");
            _controller.Start();

        }

        protected override void OnStop()
        {
            _controller.Stop();
        }
    }
}
