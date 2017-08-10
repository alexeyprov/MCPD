using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

class SetAcl
{
	const string FILE_NAME = "subject.txt";
	const AccessControlSections SD_AREA = AccessControlSections.Access | 
		AccessControlSections.Audit |
		AccessControlSections.Owner;

	static void Main()
	{
		FileSecurity fs = new FileSecurity(FILE_NAME, SD_AREA);

		fs.SetAccessRule(new FileSystemAccessRule(@"vorlonxp\larisa", 
			FileSystemRights.FullControl, AccessControlType.Deny));

		IdentityReference adminsGroup = new SecurityIdentifier(
			WellKnownSidType.BuiltinAdministratorsSid, null);

        fs.SetAccessRule(new FileSystemAccessRule(adminsGroup, 
			FileSystemRights.FullControl, AccessControlType.Allow));

		fs.SetAccessRule(new FileSystemAccessRule(@"vorlonxp\developer", 
			FileSystemRights.Read | FileSystemRights.AppendData | FileSystemRights.Modify, 
            AccessControlType.Allow));

        (new FileInfo(FILE_NAME)).SetAccessControl(fs);

		PrintOwner(fs, true);
		PrintOwner(fs, false);

		Console.WriteLine("Security descriptor is {0}", 
			fs.GetSecurityDescriptorSddlForm(SD_AREA));

		PrintSacl(fs);
	}

	static void PrintOwner(ObjectSecurity sec, bool getSid)
	{
		IdentityReference id = sec.GetOwner(getSid ? 
			typeof(SecurityIdentifier) : 
			typeof(NTAccount));
		Console.WriteLine("Owner is {0}", id.ToString());
	}

	static void PrintSacl(CommonObjectSecurity sec)
	{
		Console.WriteLine("SACL:");
		foreach (AuditRule rule in sec.GetAuditRules(true, true, typeof(NTAccount)))
		{
			string access = "unknown";

			if (rule is FileSystemAuditRule)
			{
				access = ((FileSystemAuditRule) rule).FileSystemRights.ToString();
			}
			Console.WriteLine("  {0} access {1} attempts audited for {2}",
				access, rule.AuditFlags, rule.IdentityReference);
		}
	}
}