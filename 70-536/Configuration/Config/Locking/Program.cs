using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using Locking.Configuration;

namespace Locking
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Configuration.Configuration c =
				ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			UrlSection s = (UrlSection)c.GetSection("locking.urls");
			ToggleLock(s.LockElements, "urls");

			foreach (UrlElement url in s.Urls)
			{
				ConfigurationLockCollection locks = url.LockAttributes;
				ToggleLock(locks, "port");

				Console.WriteLine("{0} URL's locks changed to {1}",
					url.Name, locks.AttributeList);
			}
			c.Save(ConfigurationSaveMode.Full);
		}

		static void ToggleLock(ConfigurationLockCollection locks, string name)
		{
			if (locks.Contains(name))
			{
				locks.Remove(name);
			}
			else
			{
				locks.Add(name);
			}
		}
	}
}
