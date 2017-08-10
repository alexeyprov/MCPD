using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SessionStateTest : BasePage
{
	protected void btnAddObject_Click(object sender, EventArgs e)
	{
		Session[Title] = NonSerializableObject;
	}

	private static NonSerializableClass NonSerializableObject
	{
		get
		{
			NonSerializableClass obj = new NonSerializableClass();
			obj.NumericProperty = 7;
			obj.TextProperty = "Hola!";
			return obj;
		}
	}
	protected void lnkSignOut_Click(object sender, EventArgs e)
	{
		Session.Abandon();
	}
}
