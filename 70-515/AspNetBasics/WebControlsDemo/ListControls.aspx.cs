using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebControlsDemo_ListControls : BasePage
{
	#region Private Constants

	private const string COMPRESSED_VIEWSTATE_FIELD = "__COMPRESSED_VIEWSTATE";

	#endregion

	#region Private Fields

	private static Hashtable _menu; 

	#endregion

	#region Class Constructor

	static WebControlsDemo_ListControls()
	{
		_menu = new Hashtable(3);
		_menu.Add(ProductId.Lazanga, "Lazanga");
		_menu.Add(ProductId.Pizza, "Pizza");
		_menu.Add(ProductId.Spaghetti, "Spaghetti");
	}
 
	#endregion

	#region Event Handlers

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			lstMenu.DataSource = Menu;
			cmbMenu.DataSource = Menu;
			cblMenu.DataSource = Menu;
			rblMenu.DataSource = Menu;

			// Bind all 4 controls at once
			this.DataBind();
		}
	}

	protected void btnGetSelection_Click(object sender, EventArgs e)
	{
		StringBuilder sb = new StringBuilder();

		PrintMultipleSelection(sb, lstMenu);
		PrintSingleSelection(sb, cmbMenu);
		PrintMultipleSelection(sb, cblMenu);
		PrintSingleSelection(sb, rblMenu);

		lblSelection.Text = sb.ToString();
	}
 
	#endregion

	#region Overrides

	protected override void SavePageStateToPersistenceMedium(object state)
	{
		string data;
		
		using (MemoryStream innerStream = new MemoryStream())
		{
			using (GZipStream outerStream = new GZipStream(innerStream, CompressionMode.Compress, true))
			{
				ObjectStateFormatter formatter = new ObjectStateFormatter();
				formatter.Serialize(outerStream, state);
			}

			innerStream.Seek(0, SeekOrigin.Begin);
			data = Convert.ToBase64String(innerStream.ToArray());
		}

		ClientScript.RegisterHiddenField(COMPRESSED_VIEWSTATE_FIELD, data);
	}

	protected override object LoadPageStateFromPersistenceMedium()
	{
		string data = Request.Form[COMPRESSED_VIEWSTATE_FIELD];
		if (String.IsNullOrEmpty(data))
		{
			return base.LoadPageStateFromPersistenceMedium();
		}

		using (MemoryStream innerStream = new MemoryStream(Convert.FromBase64String(data)))
		{
			using (GZipStream outerStream = new GZipStream(innerStream, CompressionMode.Decompress, true))
			{
				ObjectStateFormatter formatter = new ObjectStateFormatter();
				return formatter.Deserialize(outerStream);
			}
		}
	}

	#endregion

	#region Implementation

	protected static ICollection Menu
	{
		get
		{
			return _menu;
		}
	}

	private static void PrintSingleSelection(StringBuilder sb, ListControl c)
	{
		sb.AppendFormat("Selection in {0} (Text, Value): ", c.ID);

		ListItem item = c.SelectedItem;
		if (null == item)
		{
			sb.AppendLine("none<br/>");
		}
		else
		{
			sb.AppendFormat("({0}, {1})<br/>{2}",
				item.Text,
				item.Value,
				Environment.NewLine);
		}
	}

	private static void PrintMultipleSelection(StringBuilder sb, ListControl c)
	{
		bool anySelected = false;
		sb.AppendFormat("Selection in {0} (Text, Value): <br/>{1}",
			c.ID,
			Environment.NewLine);

		foreach (ListItem item in c.Items)
		{
			if (item.Selected)
			{
				sb.AppendFormat("{0}{0}({1}, {2})<br/>{3}",
					"&nbsp;",
					item.Text,
					item.Value,
					Environment.NewLine);
				anySelected = true;
			}
		}

		if (!anySelected)
		{
			sb.AppendLine("none<br/>");
		}
	}

	#endregion

	#region Inner Classes

	enum ProductId
	{
		Pizza,
		Lazanga,
		Spaghetti
	} 

	#endregion
}
