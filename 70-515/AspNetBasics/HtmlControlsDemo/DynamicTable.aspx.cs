using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class HtmlControlsDemo_DynamicTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
	protected void btnGenerate_Click(object sender, EventArgs e)
	{
		if (IsValid)
		{
			int rowCount = Int32.Parse(txtRowCount.Text);
			int colCount = Int32.Parse(txtColCount.Text);

			// Set up a table
			HtmlTable table = new HtmlTable();
			table.CellPadding = 3;
			table.CellSpacing = 5;
			table.BorderColor = "red";
			table.Border = 1;

			// Set up rows
			for (int i = 0; i < rowCount; ++i)
			{
				HtmlTableRow row = new HtmlTableRow();
				row.BgColor = (0 == i % 2) ? "lightyellow" : "cyan";
				
				// Set up columns
				for (int j = 0; j < colCount; ++j)
				{
					HtmlTableCell cell = new HtmlTableCell();
					cell.InnerHtml = String.Format("Row {0}<br/>Column {1}", i + 1, j + 1);
					row.Cells.Add(cell);
				}

				table.Rows.Add(row);
			}

			phTable.Controls.Add(table);
		}
	}
}
