using System;

namespace SherlockLib 
{
    public partial class quotations 
	{
		public partial class quotationRow
		{
			public string Quote
			{
				get
				{
					return String.Join("\n<br/>", Array.ConvertAll(GetpRows(), r => r.p_Text));
				}
			}
		}

    }

}
