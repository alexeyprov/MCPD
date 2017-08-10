using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SherlockLib
{
	public class QuoteSource
	{
		private quotations _ds;

		public QuoteSource()
		{
			_ds = new quotations();

			using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream("SherlockLib.SherlockQuotes.xml"))
			{
				_ds.ReadXml(stm);
			}
		}

		public quotations.quotationRow RandomQuote
		{
			get
			{
				Random rg = new Random();
				return _ds.quotation[rg.Next(_ds.quotation.Count - 1)];
			}
		}
	}
}
