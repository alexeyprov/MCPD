using System.Text;

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
					StringBuilder sb = new StringBuilder();
					foreach (pRow p in GetpRows())
					{
						sb.AppendLine(p.p_Text);
						sb.AppendLine("<br/>");
					}
					sb.Remove(sb.Length - 7, 6);
					return sb.ToString();
				}
			}
		}

    }

}
