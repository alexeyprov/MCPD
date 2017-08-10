using System;

internal sealed class AttrsApp
{
	static void Main()
	{
		CheckAccount(typeof(ChildAccount));
		CheckAccount(typeof(ParentAccount));
		CheckAccount(typeof(AttrsApp));
	}

	static void CheckAccount(Type t)
	{
		AccountAttribute effectiveAccount = (AccountAttribute) 
			Attribute.GetCustomAttribute(t, typeof(AccountAttribute), false);

		if (effectiveAccount != null && _etalonAccount.Match(effectiveAccount))
		{
			Console.WriteLine("{0} can write checks", t);
		}
		else
		{
			Console.WriteLine("{0} can NOT write checks", t);
		}
	}

	static readonly AccountAttribute _etalonAccount = new AccountAttribute(AccountType.Checking);
}