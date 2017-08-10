using System;
using System.Security;
using System.Security.Permissions;
using System.Reflection;

namespace CustomPermission
{
	// Derive from CodeAccessPermission to gain implementations of the following
	// sealed IStackWalk methods: Assert, Demand, Deny, and PermitOnly.
	// Implement the following abstract IPermission methods: Copy, Intersect, and IsSubSetOf.
	// Implementing the Union method of the IPermission class is optional.
	// Implement the following abstract ISecurityEncodable methods: FromXml and ToXml.
	// Making the class 'sealed' is optional.

	public sealed class SoundPermission : CodeAccessPermission, IPermission,
		IUnrestrictedPermission, ISecurityEncodable, ICloneable
	{
		private Boolean _specifiedAsUnrestricted = false;
		private SoundPermissionState _flags = SoundPermissionState.NoSound;

		// This constructor creates and initializes a permission with generic access.
		public SoundPermission(PermissionState state)
		{
			_specifiedAsUnrestricted = (state == PermissionState.Unrestricted);
		}

		// This constructor creates and initializes a permission with specific access.        
		public SoundPermission(SoundPermissionState flags)
		{
			if (!Enum.IsDefined(typeof(SoundPermissionState), flags))
				throw new ArgumentException
					("flags value is not valid for the SoundPermissionState enuemrated type");
			_specifiedAsUnrestricted = false;
			_flags = flags;
		}

		// For debugging, return the state of this object as XML.
		public override String ToString()
		{
			return ToXml().ToString();
		}

		// Private method to cast (if possible) an IPermission to the type.
		private SoundPermission VerifyTypeMatch(IPermission target)
		{
			if (GetType() != target.GetType())
				throw new ArgumentException(String.Format("target must be of the {0} type",
					GetType().FullName));
			return (SoundPermission) target;
		}

		// This is the Private Clone helper method. 
		private SoundPermission Clone(Boolean specifiedAsUnrestricted, SoundPermissionState flags)
		{
			SoundPermission soundPerm = (SoundPermission) Clone();
			soundPerm._specifiedAsUnrestricted = specifiedAsUnrestricted;
			soundPerm._flags = specifiedAsUnrestricted ? SoundPermissionState.PlayAnySound : _flags;
			return soundPerm;
		}

		#region IPermission Members
		// Return a new object that contains the intersection of 'this' and 'target'.
		public override IPermission Intersect(IPermission target)
		{
			// If 'target' is null, return null.
			if (target == null)
				return null;

			// Both objects must be the same type.
			SoundPermission soundPerm = VerifyTypeMatch(target);

			// If 'this' and 'target' are unrestricted, return a new unrestricted permission.
			if (_specifiedAsUnrestricted && soundPerm._specifiedAsUnrestricted)
				return Clone(true, SoundPermissionState.PlayAnySound);

			// Calculate the intersected permissions. If there are none, return null.
			SoundPermissionState val = (SoundPermissionState)
				Math.Min((Int32) _flags, (Int32) soundPerm._flags);
			if (val == 0)
				return null;

			// Return a new object with the intersected permission value.
			return Clone(false, val);
		}

		// Called by the Demand method: returns true if 'this' is a subset of 'target'.
		public override Boolean IsSubsetOf(IPermission target)
		{
			// If 'target' is null and this permission allows nothing, return true.
			if (target == null)
				return _flags == 0;

			// Both objects must be the same type.
			SoundPermission soundPerm = VerifyTypeMatch(target);

			// Return true if the permissions of 'this' is a subset of 'target'.
			return _flags <= soundPerm._flags;
		}

		// Return a new object that matches 'this' object's permissions.
		public sealed override IPermission Copy()
		{
			return (IPermission) Clone();
		}

		// Return a new object that contains the union of 'this' and 'target'.
		// Note: You do not have to implement this method. If you do not, the version
		// in CodeAccessPermission does this:
		//    1. If target is not null, a NotSupportedException is thrown.
		//    2. If target is null, then Copy is called and the new object is returned.
		public override IPermission Union(IPermission target)
		{
			// If 'target' is null, then return a copy of 'this'.
			if (target == null)
				return Copy();

			// Both objects must be the same type.
			SoundPermission soundPerm = VerifyTypeMatch(target);

			// If 'this' or 'target' are unrestricted, return a new unrestricted permission.
			if (_specifiedAsUnrestricted || soundPerm._specifiedAsUnrestricted)
				return Clone(true, SoundPermissionState.PlayAnySound);

			// Return a new object with the calculated, unioned permission value.
			return Clone(false, (SoundPermissionState)
				Math.Max((Int32) _flags, (Int32) soundPerm._flags));
		}
		#endregion

		#region ISecurityEncodable Members
		// Populate the permission's fields from XML.
		public override void FromXml(SecurityElement e)
		{
			_specifiedAsUnrestricted = false;
			_flags = 0;

			// If XML indicates an unrestricted permission, make this permission unrestricted.
			String s = (String) e.Attributes["Unrestricted"];
			if (s != null)
			{
				_specifiedAsUnrestricted = Convert.ToBoolean(s);
				if (_specifiedAsUnrestricted)
					_flags = SoundPermissionState.PlayAnySound;
			}

			// If XML indicates a restricted permission, parse the flags.
			if (!_specifiedAsUnrestricted)
			{
				s = (String) e.Attributes["Flags"];
				if (s != null)
				{
					_flags = (SoundPermissionState)
					Convert.ToInt32(Enum.Parse(typeof(SoundPermission), s, true));
				}
			}
		}

		// Produce XML from the permission's fields.
		public override SecurityElement ToXml()
		{
			// These first three lines create an element with the required format.
			SecurityElement e = new SecurityElement("IPermission");
			// Replace the double quotation marks () with single quotation marks ()
			// to remain XML compliant when the culture is not neutral.
			e.AddAttribute("class", GetType().AssemblyQualifiedName.Replace('\"', '\''));
			e.AddAttribute("version", "1");

			if (!_specifiedAsUnrestricted)
				e.AddAttribute("Flags", Enum.Format(typeof(SoundPermissionState), _flags, "G"));
			else
				e.AddAttribute("Unrestricted", "true");
			return e;
		}
		#endregion

		#region IUnrestrictedPermission Members
		// Returns true if permission is effectively unrestricted.
		public Boolean IsUnrestricted()
		{
			// This means that the object is unrestricted at runtime.
			return _flags == SoundPermissionState.PlayAnySound;
		}
		#endregion

		#region ICloneable Members

		// Return a copy of the permission.
		public Object Clone()
		{
			return MemberwiseClone();
		}

		#endregion

		#region Entry Point
		static void Main()
		{
			SoundPermission sp = new SoundPermission(PermissionState.None);
			Console.WriteLine(sp);
		}
		#endregion
	}
}
