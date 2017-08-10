#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HttpListenerLibrary;

#endregion

namespace SampleWinAppHost
{
    partial class Form1 : Form
    {
        HttpListenerController _controller = null;
        
        string[] prefixes = new string[] {
                "http://localhost:8081/winapp/", 
                "http://127.0.0.1:8081/winapp/"
        };
        string vdir = "/winapp";
        string pdir = @"C:\Temp\HostingASMX\SampleWinAppHost\";

        public Form1()
        {
            InitializeComponent();

            _controller = new HttpListenerController(prefixes, vdir, pdir);
            _controller.Start();
            lblMessage.Text = string.Format("Listening on {0}", prefixes[0]);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
             _controller.Stop();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}