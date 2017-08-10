using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SherlockLib;

public partial class HolmesQuote : BasePage
{
	private const string QUOTES_KEY = "SHERLOCK_HOLMES_QUOTES_KEY";

	protected void Page_Load(object sender, EventArgs e)
	{
		quotations.quotationRow q = Quotes.RandomQuote;
		lblSource.Text = q.source;
		lblDate.Text = q.date;
		lblQuote.Text = q.Quote;
	}

	private QuoteSource Quotes
	{
		get
		{
			QuoteSource retval = Cache[QUOTES_KEY] as QuoteSource;

			if (null == retval)
			{
				retval = new QuoteSource();
				Cache[QUOTES_KEY] = retval;
			}

			return retval;
		}
	}
}
