using System;

[Flags]
public enum AccountType
{
	Saving,
	Checking,
	Brokerage
}

[AttributeUsage(AttributeTargets.Class)]
public class AccountAttribute : Attribute
{
	AccountType _accType;

	public AccountAttribute(AccountType accType)
	{
		_accType = accType;
	}

	public AccountType AccountType
	{
		get
		{
			return _accType;
		}
	}

	// override Match for bit mask comparison
	// return true if other object is a superset of this one
	public override bool Match(object o)
	{
		AccountAttribute a = o as AccountAttribute;
		if (null == a)
		{
			return false;
		}

		return ((a._accType & _accType) == _accType);
	}

	// override Equals for exact comparison
	public override bool Equals(object o)
	{
		AccountAttribute a = o as AccountAttribute;
		if (null == a)
		{
			return false;
		}

		return (a._accType == _accType);
	}

	// need to override GetHashCode since Equals is overridden
	public override int GetHashCode()
	{
		return (int) _accType;
	}
}