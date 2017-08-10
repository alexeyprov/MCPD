using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDependencyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            SqlDependency.Start(connString);

            Console.ReadLine();

            SqlDependency.Stop(connString);
        }
    }
}
