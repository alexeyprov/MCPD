//(C) http://support.microsoft.com/kb/953587
using System;
using System.Security.AccessControl;
using System.Security.Principal;

using Microsoft.Win32;

public class EventLogSecurity
{
    // Event Log Registry path
    const string RK_EVENT_LOG = @"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\EventLog\";
	const string RV_CUSTOM_SD = "CustomSD";

    // Access Masks
    public const int CustomSD_READ_ACCESS = 0x1;
    public const int CustomSD_WRITE_ACCESS = 0x2;
    public const int CustomSD_CLEAR_ACCESS = 0x4;
    public const int CustomSD_ALL_ACCESS = 0x7;

	// Data
	string _logName;

	// Construction/Destruction
	public EventLogSecurity(string logName)
	{
		_logName = logName;
	}

	// Properties
	public string LogName
	{
		get
		{
			return _logName;
		}
	}

    public bool HasCustomSD
    {
		get
		{
        	return (Registry.GetValue(RegPath,
				RV_CUSTOM_SD, null) != null);
		}
    }

	public CommonSecurityDescriptor CustomSD
	{
		get
		{
            string sddl = (string) Registry.GetValue(
				RegPath, RV_CUSTOM_SD, null);

            if (null == sddl)
			{
                return null;
			}
            return new CommonSecurityDescriptor(false, false, sddl);
		}
		set
		{
			string sddl = value.GetSddlForm(AccessControlSections.All);
        	Registry.SetValue(RegPath, RV_CUSTOM_SD, 
				sddl, RegistryValueKind.String);
		}
	}

	// Operations
    public void CreateCustomSD()
    {
        // By default, give all local admins all access  
        SecurityIdentifier admins = new SecurityIdentifier(
			WellKnownSidType.BuiltinAdministratorsSid, null);
           
        // Setup the DACL
        DiscretionaryAcl dacl = new DiscretionaryAcl(false, false, 1);
        dacl.AddAccess(AccessControlType.Allow, admins, CustomSD_ALL_ACCESS, 
			InheritanceFlags.None, PropagationFlags.None);

        // Create the Security Descriptor
        CommonSecurityDescriptor sd = new CommonSecurityDescriptor(false, false, 
			ControlFlags.DiscretionaryAclPresent, admins, admins, null, dacl);
           
        // Save the SDDL into the CustomSD
		CustomSD = sd;
    }

    public void AddUserToCustomSD(string domain, string account, int mask)
    {
        // Create a SID for the user
        SecurityIdentifier sid = (SecurityIdentifier) 
			(new NTAccount(domain, account).Translate(typeof(SecurityIdentifier)));

        // Make sure we have a CustomSD in place
        if (!HasCustomSD)
		{
            CreateCustomSD();
		}

        // Get the current SD
        CommonSecurityDescriptor sd = CustomSD;

        // add the ACE
        sd.DiscretionaryAcl.AddAccess(AccessControlType.Allow, sid, mask, 
			InheritanceFlags.None, PropagationFlags.None);

        // Save the SDDL into the CustomSD
        CustomSD = sd;
    }

    public void RemoveUserFromCustomSD(string domain, string account)
    {
		// Make sure we have a CustomSD in place, if not bail
        if (!HasCustomSD)
		{
			throw new InvalidOperationException();
		}

        // Create a SID for the user
        SecurityIdentifier sid = (SecurityIdentifier)
			(new NTAccount(domain, account).Translate(typeof(SecurityIdentifier)));

        // Get the current sd
        CommonSecurityDescriptor sd = CustomSD;

        // Find the sid in the sd
		foreach (KnownAce ace in sd.DiscretionaryAcl)
		{
			if (ace.SecurityIdentifier.Equals(sid))
			{
                // remove the ace for that sid
            	sd.DiscretionaryAcl.RemoveAccessSpecific(AccessControlType.Allow, 
					sid, ace.AccessMask,
					InheritanceFlags.None, PropagationFlags.None);

            	// write out the CustomSD
            	CustomSD = sd;
				return;
			}
		}

		throw new ArgumentException();
    }

	// Implementation
	private string RegPath
	{
		get
		{
			return RK_EVENT_LOG + _logName;
		}
	}
}