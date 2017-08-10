//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Windows.Forms;

namespace TransactionsDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         ServiceHost accountHost = new ServiceHost(typeof(AccountService),new Uri("http://localhost:8000"));
         accountHost.Open();

         ServiceHost accountManagerHost = new ServiceHost(typeof(AccountManager),new Uri("http://localhost:7000"));
         accountManagerHost.Open();

         //Can do blocking calls:
         Application.Run(new HostForm());

         accountHost.Close();
         accountManagerHost.Close();
      }
   }
}
