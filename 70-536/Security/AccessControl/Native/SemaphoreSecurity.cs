using System;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;

public class Example
{
    public static void Main()
    {
        // Create a string representing the current user.
        string user = Environment.UserDomainName + "\\" + 
            Environment.UserName;

        // Create a security object that grants no access.
        SemaphoreSecurity sSec = new SemaphoreSecurity();

        // Add a rule that grants the current user the 
        // right to enter or release the semaphore.
        SemaphoreAccessRule rule = new SemaphoreAccessRule(user, 
            SemaphoreRights.Synchronize | SemaphoreRights.Modify, 
            AccessControlType.Allow);
        sSec.AddAccessRule(rule);

        // Add a rule that denies the current user the 
        // right to change permissions on the semaphore.
        rule = new SemaphoreAccessRule(user, 
            SemaphoreRights.ChangePermissions, 
            AccessControlType.Deny);
        sSec.AddAccessRule(rule);

        // Display the rules in the security object.
        ShowSecurity(sSec);

        // Add a rule that allows the current user the 
        // right to read permissions on the semaphore. This rule
        // is merged with the existing Allow rule.
        rule = new SemaphoreAccessRule(user, 
            SemaphoreRights.Delete, 
            AccessControlType.Allow);
        sSec.AddAccessRule(rule);

        ShowSecurity(sSec);
    }

    private static void ShowSecurity(SemaphoreSecurity security)
    {
        Console.WriteLine("\r\nCurrent access rules:\r\n");

        foreach(SemaphoreAccessRule ar in 
            security.GetAccessRules(true, true, typeof(NTAccount)))
        {
            Console.WriteLine("        User: {0}", ar.IdentityReference);
            Console.WriteLine("        Type: {0}", ar.AccessControlType);
            Console.WriteLine("      Rights: {0}", ar.SemaphoreRights);
            Console.WriteLine();
        }
    }
}
