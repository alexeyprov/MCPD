using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomControlTest : BasePage
{
	protected void udTest_Decremented(object sender, EventArgs e)
	{
		Debug.WriteLine(">>> Decremented event");
	}
	protected void udTest_Incremented(object sender, EventArgs e)
	{
		Debug.WriteLine(">>> Incremented event");
	}
	protected void udTest_ValueChanged(object sender, NumericValueChangedEventArgs e)
	{
		Debug.WriteLine(">>> ValueChanged event");
	}
}