using System;

namespace AspNetBasics.ClientProgrammingDemo.UI
{
	public partial class ThreePanelsPage : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FirstLabel.Text = SecondLabel.Text = ThirdLabel.Text = DateTime.Now.ToString();
		}
	} 
}