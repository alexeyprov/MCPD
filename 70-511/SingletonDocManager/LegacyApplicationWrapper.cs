using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonDocManager
{
    internal class LegacyApplicationWrapper : WindowsFormsApplicationBase
    {
        private App _application;

        public LegacyApplicationWrapper()
        {
            IsSingleInstance = true;
        }

        protected override bool OnStartup(StartupEventArgs eventArgs)
        {
            if (_application == null)
            {
                _application = new App();
                _application.Run();
            }

            return false;
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            base.OnStartupNextInstance(eventArgs);

            if (_application != null)
            {
                _application.ProcessCommandLine(eventArgs.CommandLine);
            }
        }
    }
}
