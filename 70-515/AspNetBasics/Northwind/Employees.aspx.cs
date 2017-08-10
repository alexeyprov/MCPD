using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Northwind_Employees : Page
{
	#region Constants

	private const string COMPANY_COMMAND = "ShowCompany";
	private const string EDITED_EMPLOYEE_SESSION_KEY = "EDITED_EMPLOYEE_ID";

	private const string EMPLOYEE_ID_FIELD = "EMPLOYEEID";
	private const string FIRST_NAME_FIELD = "FIRSTNAME";
	private const string LAST_NAME_FIELD = "LASTNAME";

	#endregion

	#region Fields

	private List<int> _highlightedIndexes;

	#endregion

	#region Event Handlers

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback)
		{
		}
	}

	protected void grdEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (DataControlRowType.DataRow == e.Row.RowType && 1 == e.Row.RowIndex % 2)
		{
			foreach (int idx in HighlightedIndexes)
			{
				e.Row.Cells[idx].BackColor = Color.LightGoldenrodYellow;
			}
		}
	}

	protected void grdEmployees_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (grdEmployees.SelectedIndex >= 0)
		{
			string firstName, lastName;
			GridViewRow row = grdEmployees.SelectedRow;
			if (row.DataItem != null)
			{
				//never happens, SelectedIndexChanged is always found before DataBound
				firstName = DataBinder.Eval(row.DataItem, FIRST_NAME_FIELD).ToString();
				lastName = DataBinder.Eval(row.DataItem, LAST_NAME_FIELD).ToString();
			}
			else
			{
				firstName = row.Cells[1].Text;
				lastName = row.Cells[2].Text;
			}

			lblEmployeeOrders.Text = String.Format("Orders entered by {0} {1} (ID {2}):",
				firstName,
				lastName,
				grdEmployees.SelectedDataKey.Value);
			pnlOrders.Visible = true;
			lblSelectedCompany.Visible = false;
		}
	}

	protected void grdEmployees_Sorted(object sender, EventArgs e)
	{
		grdEmployees.SelectedIndex = -1;
	}

	protected void grdEmployees_RowEditing(object sender, GridViewEditEventArgs e)
	{
		if (e.NewEditIndex >= 0)
		{
			Session[EDITED_EMPLOYEE_SESSION_KEY] = grdEmployees.DataKeys[e.NewEditIndex].Value;
		}
	}

	protected void grdEmployees_RowUpdated(object sender, GridViewUpdatedEventArgs e)
	{
		// check for concurrency
		if (e.KeepInEditMode = pnlError.Visible = (0 == e.AffectedRows))
		{
			// bind grid to the updated values
			grdEmployees.DataBind();

			// keep user-provided values in the edit row
			TableCellCollection editCells = grdEmployees.Rows[grdEmployees.EditIndex].Cells;

			for (int i = 0; i < e.NewValues.Count; ++i)
			{
				TableCell cell = editCells[i + 1];
				if (cell.Controls.Count > 0)
				{
					TextBox control = cell.Controls[0] as TextBox;
					object value = e.NewValues[i];
					if (control != null)
					{
						control.Text = (value != null) ? value.ToString() : String.Empty;
					}
				}
			}
		}
	}

	protected void grdOrders_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (COMPANY_COMMAND == e.CommandName)
		{
			lblSelectedCompany.Visible = true;
			lblSelectedCompany.Text = String.Format("Selected company: {0}", e.CommandArgument);
		}
	}

	protected void srcConflictingEmployee_Selecting(object sender, SqlDataSourceCommandEventArgs e)
	{
		if (!pnlError.Visible)
		{
			e.Cancel = true;
		}
	}

	#endregion

	#region Implementation

	private IEnumerable<int> HighlightedIndexes
	{
		get
		{
			if (null == _highlightedIndexes)
			{
				_highlightedIndexes = new List<int>();

				for (int i = 0; i < grdEmployees.Columns.Count; ++i)
				{
					if (Color.LightSteelBlue == grdEmployees.Columns[i].ItemStyle.BackColor)
					{
						_highlightedIndexes.Add(i);
					}
				}
			}

			return _highlightedIndexes;
		}
	}

	protected string CalculateAge(object dataItem)
	{
		DateTime birthDate = (DateTime) DataBinder.Eval(dataItem, "BIRTHDATE");
		DateTime bdThisYear = new DateTime(DateTime.Today.Year, birthDate.Month, birthDate.Day);
		int age = DateTime.Today.Year - birthDate.Year - ((bdThisYear > DateTime.Today) ? 1 : 0);
		return age.ToString();
	}

	protected Color GetRequiredDateColor(object shippedDate, object requiredDate)
	{
		if (null == shippedDate || Convert.IsDBNull(shippedDate) ||
			null == requiredDate || Convert.IsDBNull(requiredDate))
		{
			return Color.Blue;
		}

		return DateTime.Compare((DateTime)shippedDate, (DateTime)requiredDate) > 0 ?
			Color.Red :
			grdEmployees.ForeColor;
	}

	#endregion
}
