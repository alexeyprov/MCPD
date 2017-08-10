//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Policy;
using System.Security;
using System.Security.Permissions;

static class Program
{
   static void Main()
   {
      Application.Run(new MyClientForm());
   }
}