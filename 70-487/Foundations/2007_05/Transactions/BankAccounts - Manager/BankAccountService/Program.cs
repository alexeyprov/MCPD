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
         ServiceHost accountHost = new ServiceHost(typeof(AccountService));
         accountHost.Open();

         ServiceHost accountManagerHost = new ServiceHost(typeof(AccountManager));
         accountManagerHost.Open();

         //Can do blocking calls:
         Application.Run(new HostForm());

         accountHost.Close();
         accountManagerHost.Close();
      }
   }
}
