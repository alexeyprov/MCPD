using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace ServerObjectModel.Basic
{
	internal class Program
	{
		private const int LOCAL_SITE_PORT = 40113;

		private static void Main()
		{
			ShowFarmInfo();
			ShowSiteInfo();
			ShowList();
			ShowPermissions();
		}

		private static void ShowPermissions()
		{
			Console.WriteLine("--------------------");

			using (SPSite site = GetSiteCollection())
			{
				using (SPWeb web = site.RootWeb)
				{
					foreach (SPRoleAssignment assignment in web.RoleAssignments)
					{
						Console.WriteLine(">> Role assignments for: " + assignment.Member.Name);

						foreach (SPRoleDefinition role in assignment.RoleDefinitionBindings)
						{
							Console.WriteLine(">>> Permissions: " + role.BasePermissions);
						}
					}
				}
			}
		}

		private static void ShowFarmInfo()
		{
			SPFarm farm = SPFarm.Local;

			Console.WriteLine(">>> Servers:");
			foreach (SPServer server in farm.Servers)
			{
				Console.WriteLine("Server address: " + server.Address);
				Console.WriteLine("Server name: " + server.Name);
				Console.WriteLine("Server role: " + server.Role);
			}

			Console.WriteLine("\n>>> Services:");
			foreach (SPService service in farm.Services)
			{
				Console.WriteLine("--------------------");
				Console.WriteLine("Service type: " + service.TypeName);
				Console.WriteLine("Instance count: " + service.Instances.Count);

				SPWebService webService = service as SPWebService;
				if (webService != null)
				{
					foreach (SPWebApplication application in webService.WebApplications)
					{
						Console.WriteLine("Web application: " + application.DisplayName);

						Console.WriteLine("Content databases");
						foreach (SPContentDatabase database in application.ContentDatabases)
						{
							Console.WriteLine("Database name: " + database.Name);
							Console.WriteLine("Database connection string: " + database.DatabaseConnectionString);
						}
					}
				}
			}
		}

		private static void ShowSiteInfo()
		{
			Console.WriteLine("--------------------");
			using (SPSite site = GetSiteCollection("ewf"))
			{
				Console.WriteLine("Site collection: " + site.Url);
				Console.WriteLine("Site collection zone: " + site.Zone);

				foreach (SPWeb web in site.AllWebs)
				{
					using (web)
					{
						Console.WriteLine("Site title: " + web.Title);
						Console.WriteLine("Site URL: " + web.Url);
					}
				}
			}
		}

		private static void ShowList()
		{
			Console.WriteLine("--------------------");

			using (SPSite defaultSite = GetSiteCollection())
			{
				foreach (SPListItem item in defaultSite.RootWeb.Lists["Books"].Items)
				{
					Console.WriteLine("Book: " + item.Title);
				}
			}
		}

		private static SPSite GetSiteCollection(string name = null)
		{
			UriBuilder uri = new UriBuilder(
				"http", 
				Environment.MachineName,
				LOCAL_SITE_PORT,
				string.IsNullOrEmpty(name) ? null : "sites/" + name);

			return new SPSite(uri.ToString());
		}
	}
}
