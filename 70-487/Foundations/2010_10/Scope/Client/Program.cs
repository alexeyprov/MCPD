// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Diagnostics;
using System.ServiceModel.Discovery;
using System.Windows.Forms;


class Program
{
   static void Main()
   {
      Application.Run(new DiscoveryForm());
   }
}
