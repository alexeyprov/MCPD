using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;

namespace WmiTest
{
	class Program
	{
		static void Main()
		{
			using (ManagementObjectSearcher searcher = PrepareSearcher())
			{
				ListLocalDrives(searcher);
				ListNetworkAdapters(searcher);
				ListServices(searcher);
				HandleEvents();
			}
		}

		#region Searcher/Watcher initialization
		private static ManagementObjectSearcher PrepareSearcher()
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher();
			//searcher.Scope = PrepareScope();
 			return searcher;
		}

		private static ManagementEventWatcher PrepareWatcher()
		{
			ManagementEventWatcher watcher = new ManagementEventWatcher();
			watcher.Options.Timeout = new TimeSpan(0, 0, 30);
			//watcher.Scope = PrepareScope();
			return watcher;
		}

		private static ManagementScope PrepareScope()
		{
			ConnectionOptions opts = new ConnectionOptions();
			opts.Username = "johndoe";
			opts.Password = "dummy";
			return new ManagementScope(@"\\myhost", opts);
		}
		#endregion

		#region Object-related methods
		private static void ListLocalDrives(ManagementObjectSearcher searcher)
		{
			const string DISK_QUERY = "SELECT Name, Size, FreeSpace FROM Win32_LogicalDisk WHERE DriveType = 3";
			const string PROP_NAME = "Name";
			const string PROP_SIZE = "Size";
			const string PROP_FREE = "FreeSpace";

			Console.WriteLine("Logical disks >>>");

			searcher.Query = new ObjectQuery(DISK_QUERY);
			foreach (ManagementBaseObject disk in searcher.Get())
			{
				Console.WriteLine("Disk \"{0}\" has {1} bytes free of total {2}",
					disk[PROP_NAME],
					disk[PROP_FREE],
					disk[PROP_SIZE]);
			}
		}

		private static void ListNetworkAdapters(ManagementObjectSearcher searcher)
		{
			const string ADAPTER_QUERY = "SELECT * FROM Win32_NetworkAdapterConfiguration";
			const string PROP_DESCR = "Description";
			const string PROP_MAC = "MACAddress";
			const string PROP_IPFLAG = "IPEnabled";
			const string PROP_DNSHOST = "DNSHostName";
			const string PROP_DNSDOMAIN = "DNSDomain";
			const string PROP_IPADRESSES = "IPAddress";
			const string PROP_IPSUBNETS = "IPSubnet";

			Console.WriteLine("Network adapters >>>");

			searcher.Query = new ObjectQuery(ADAPTER_QUERY);
			foreach (ManagementBaseObject adapter in searcher.Get())
			{
				Console.WriteLine("Adapter \"{0}\" with MAC {1}",
					adapter[PROP_DESCR],
					adapter[PROP_MAC]);
				if ((bool)adapter[PROP_IPFLAG])
				{
					const string INDENT = "    ";
					Console.WriteLine("{2}DNS full name: {0}.{1}",
						adapter[PROP_DNSHOST],
						adapter[PROP_DNSDOMAIN],
						INDENT);

					string[] addrs = (string[])adapter[PROP_IPADRESSES];
					string[] subnets = (string[])adapter[PROP_IPSUBNETS];
					if (null != addrs && null != subnets)
					{
						int cnt = addrs.Length;
						Debug.Assert(subnets.Length == cnt);
						for (int i = 0; i < cnt; i++)
						{
							Console.WriteLine("{2}{2}IP {0} with mask {1}",
								addrs[i],
								subnets[i],
								INDENT);
						}
					}
				}
			}
		}

		private static void ListServices(ManagementObjectSearcher searcher)
		{
			const string SVC_QUERY = "SELECT * FROM Win32_Service WHERE State = \'Paused\'";
			const string PROP_NAME = "Name";
			const string PROP_STATE = "State";

			Console.WriteLine("Paused services >>>");

			searcher.Query = new ObjectQuery(SVC_QUERY);
			foreach (ManagementBaseObject svc in searcher.Get())
			{
				Console.WriteLine("\"{0}\" service is {1}",
					svc[PROP_NAME],
					svc[PROP_STATE]);
			}
		}
		#endregion

		#region Event-related methods
		private static void HandleEvents()
		{
			const string PROCESS_CREATION_QUERY = "SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance isa \"Win32_Process\"";
			using (ManagementEventWatcher watcher = PrepareWatcher())
			{
				watcher.Query = new EventQuery(PROCESS_CREATION_QUERY); //WqlEventQuery...
				Console.WriteLine("Create a process to unlock the application...");
				//watcher.Start();

				ManagementBaseObject evt = null;
				try
				{
					evt = watcher.WaitForNextEvent();
				}
				catch (ManagementException e)
				{
					// unfortunately, ManagementException exposes no useful
					// fields to distinguish time-out and other errors
					// so just dump the exception no matter its real reason
					Console.WriteLine(e.Message);
				}

				if (evt != null)
				{
					Console.WriteLine("\"{0}\" has started",
						((ManagementBaseObject)evt["TargetInstance"])["Name"]);
				}
				watcher.Stop();
			}
		}
		#endregion
	}
}
