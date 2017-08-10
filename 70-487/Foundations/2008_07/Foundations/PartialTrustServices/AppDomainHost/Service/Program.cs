//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx;
using System.Security.Policy;
using System.Security;
using System.Security.Permissions;
using MyNamespace.Properties;
using System.Net;
using System.IO;
using System.Threading;

namespace MyNamespace
{
   class Program
   {
      //Hosts will be running under full trust
      public static void Main()
      {
         //It takes a while to create all the app domains 
         EventWaitHandle handle = new EventWaitHandle(false,EventResetMode.ManualReset,"HostReady");

         //For demonstration, there is just one endpoint in the config file, and the hosts use thier own unique base addresses to map it

         //Defualt is service with full trust
         AppDomainHost host0 = new AppDomainHost(typeof(MyService),"Defualt Full Trust App Domain",new Uri("net.tcp://localhost:6000"));
         host0.Open();

         //Can explicitly set full trust
         PermissionSet permissions1 = CodeAccessSecurityHelper.PermissionSetFromStandardSet(StandardPermissionSet.FullTrust);
         AppDomainHost host1 = new AppDomainHost(typeof(MyService),permissions1,"My Full Trust App Domain",new Uri("net.tcp://localhost:6001"));
         host1.Open();


         //With just enough permissions to do work
         PermissionSet permissions2 = new PermissionSet(PermissionState.None);
         permissions2.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows));
         permissions2.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            
         AppDomainHost host2 = new AppDomainHost(typeof(MyService),permissions2,"My Partial Trust App Domain",new Uri("net.tcp://localhost:6002"));
         host2.Open();


         //With not enough permissions to do work
         PermissionSet permissions3 = new PermissionSet(PermissionState.None);
         permissions3.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

         AppDomainHost host3 = new AppDomainHost(typeof(MyService),permissions3,"Partial trust with not enough permissions",new Uri("net.tcp://localhost:6003"));
         host3.Open();


         //Reading permissions from file
         string fileName = @"..\" + Settings.Default.PermissionSetFileName;
         AppDomainHost host4 = new AppDomainHost(typeof(MyService),fileName,"Permisison set from file",new Uri("net.tcp://localhost:6004"));
         host4.Open();

         //Using one of the named permission sets
         AppDomainHost host5 = new AppDomainHost(typeof(MyService),StandardPermissionSet.Internet,"Named permission set",new Uri("net.tcp://localhost:6005"));
         host5.Open();

         handle.Set();     

         Console.WriteLine("Press ENTER to shut down services.");
         Console.ReadLine();

         host0.Close();
         host1.Close();
         host2.Close();
         host3.Close();
         host4.Close();
         host5.Close();
      }
   }
}






