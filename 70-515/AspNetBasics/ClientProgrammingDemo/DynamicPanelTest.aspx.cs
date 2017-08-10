using System;

namespace AspNetBasics.ClientProgrammingDemo.UI
{
	public partial class DynamicPanelTestPage : BasePage
	{
		protected void dynaPanel_Refresh(object sender, EventArgs e)
		{
			lbl.Text = String.Format("Sum {0}. Updated at {1:g}",
				Int32.Parse(op1.Text) + Int32.Parse(op2.Text),
				DateTime.Now);
		}
	} 
}