using System;
using System.Collections.Generic;
using System.Text;

namespace IsoStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prompt the user for their username.

            Console.WriteLine("Login:");

            // Does no error checking.
            LoginPrefs lp = new LoginPrefs(Console.ReadLine());

            if (lp.NewPrefs)
            {
                Console.WriteLine("Please set preferences for a new user.");
                GatherInfoFromUser(lp);

                // Write the new preferences to storage.
                double percentUsed = lp.SetPrefsForUser();
                Console.WriteLine("Your preferences have been written. Current space used is " + percentUsed.ToString() + " %");
            }
            else
            {
                Console.WriteLine("Welcome back.");

                Console.WriteLine("Your preferences have expired, please reset them.");
                GatherInfoFromUser(lp);
                lp.SetNewPrefsForUser();

                Console.WriteLine("Your news site has been set to {0}\n and your sports site has been set to {1}.", lp.NewsUrl, lp.SportsUrl);
            }
            lp.GetIsoStoreInfo();
            Console.WriteLine("Enter 'd' to delete the IsolatedStorage files and exit, or press any other key to exit without deleting files.");
            string consoleInput = Console.ReadLine();
            if (consoleInput.ToLower() == "d")
            {
                lp.DeleteFiles();
                lp.DeleteDirectories();
            }

        }

        static void GatherInfoFromUser(LoginPrefs lp)
        {
            Console.WriteLine("Please enter the URL of your news site.");
            lp.NewsUrl = Console.ReadLine();
            Console.WriteLine("Please enter the URL of your sports site.");
            lp.SportsUrl = Console.ReadLine();
        }
    }
}
