//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Windows.Forms;


class Program
{
   static void Main(string[] args)
   {
      MyClientForm client = new MyClientForm();
      Application.Run(client);

   }
}
