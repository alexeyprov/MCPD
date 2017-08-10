using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HelloFromClickOnce
{
    static class Program
    {
        private const string PLUGIN_DEPLOYMENT_GROUP = "PluginOne";
        private const string PLUGIN_ASSEMBLY_NAME = "PluginOne";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if FULL_TRUST
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (string.Compare(args.Name, PLUGIN_ASSEMBLY_NAME + ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", true) != 0)
            {
                return null;
            }

            if (!ApplicationDeployment.CurrentDeployment.IsFileGroupDownloaded(PLUGIN_DEPLOYMENT_GROUP))
            {
                ApplicationDeployment.CurrentDeployment.DownloadFileGroup(PLUGIN_DEPLOYMENT_GROUP);
            }

            string rootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Assembly.LoadFrom(
                Path.Combine(
                    rootFolder, 
                    Path.ChangeExtension(PLUGIN_ASSEMBLY_NAME, "dll")));
        }
    }
}
