using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonDocManager
{
    internal sealed class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            (new LegacyApplicationWrapper()).Run(args);
        }
    }
}
