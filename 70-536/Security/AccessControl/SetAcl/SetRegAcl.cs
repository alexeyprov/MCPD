using System;
using System.IO;
using System.Security.AccessControl;

using Microsoft.Win32;

class SetAcl
{
    const string RK_SOFTWARE = "Software";
	const string RK_ACL = "AclTest";
	static void Main()
	{
		RegistrySecurity rs = new RegistrySecurity();

		rs.SetAccessRule(new RegistryAccessRule(@"vorlonxp\larisa", 
			RegistryRights.FullControl, AccessControlType.Deny));

        rs.SetAccessRule(new RegistryAccessRule(@"BUILTIN\Администраторы", 
			RegistryRights.FullControl, AccessControlType.Allow));

		rs.SetAccessRule(new RegistryAccessRule(@"vorlonxp\developer", 
			RegistryRights.ReadKey | RegistryRights.WriteKey, 
            AccessControlType.Allow));

		rs.SetAccessRuleProtection(true, //protect from being inherited by children
			true); //inherit from parent

		using (RegistryKey rkSoft = Registry.LocalMachine.CreateSubKey(RK_SOFTWARE))
		{
			using (RegistryKey rk = rkSoft.CreateSubKey(RK_ACL))
			{
				rk.SetAccessControl(rs);
			}
		}
	}
}