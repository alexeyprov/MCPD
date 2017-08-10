using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetBasics.ClientProgrammingDemo.UI
{
	public partial class PageHistoryPage : BasePage
	{
		protected void smAjax_Navigate(object sender, HistoryEventArgs e)
		{
			string savedState = e.State[wizSimple.ID];

			wizSimple.ActiveStepIndex = (String.IsNullOrEmpty(savedState)) ?
				0 :
				Int32.Parse(savedState);
			this.Title = WizardTitle;
		}

		protected void wizSimple_ActiveStepChanged(object sender, EventArgs e)
		{
			if (smAjax.IsInAsyncPostBack && !smAjax.IsNavigating)
			{
				smAjax.AddHistoryPoint(wizSimple.ID, wizSimple.ActiveStepIndex.ToString(), WizardTitle);
			}
		}

		private string WizardTitle
		{
			get
			{
				return String.Format("Step #{0}", wizSimple.ActiveStepIndex + 1);
			}
		}
	} 
}