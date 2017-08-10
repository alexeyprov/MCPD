using System;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace SecHostApp
{
	class CustomPermission : CodeAccessPermission,
		IUnrestrictedPermission,
		ISecurityEncodable
	{
		#region Construction/Destruction
		public CustomPermission(PermissionState state)
		{
			_state = state;
		}
		#endregion

		#region Overrides
		public override IPermission Copy()
		{
			return new CustomPermission(_state);
		}

		public override void FromXml(SecurityElement elem)
		{
			try
			{
				_state = (Boolean.Parse(elem.Attribute(ATTR_NAME))) ?
					PermissionState.Unrestricted :
					PermissionState.None;
			}
			catch (FormatException)
			{
				_state = PermissionState.None;
			}
			catch (ArgumentException)
			{
				_state = PermissionState.None;
			}
		}

		public override IPermission Intersect(IPermission target)
		{
			CustomPermission cp = target as CustomPermission;
			if (null == cp)
			{
				return null;
			}

			return new CustomPermission(cp._state & this._state);
		}

		public override bool IsSubsetOf(IPermission target)
		{
			CustomPermission cp = target as CustomPermission;
			if (null == cp)
			{
				return false;
			}

			if (cp.IsUnrestricted())
			{
				return true;
			}
			else if (this.IsUnrestricted())
			{
				return false;
			}

			return (cp._state == this._state);
		}

		public override SecurityElement ToXml()
		{
			SecurityElement se = new SecurityElement(GetType().Name);
			se.AddAttribute(ATTR_NAME, IsUnrestricted().ToString());
			return se;
		}
		#endregion

		#region IUnrestrictedPermission Members

		public bool IsUnrestricted()
		{
			return (_state == PermissionState.Unrestricted);
		}

		#endregion

		#region Data Members
		private const string ATTR_NAME = "Unrestricted";
		private PermissionState _state;
		#endregion
	}
}
