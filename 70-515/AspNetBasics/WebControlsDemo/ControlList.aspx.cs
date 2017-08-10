using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ControlList : BasePage
{
	#region Constants

	private const string DYNA_BUTTON_ID = "dynaButton";
	private const string MIME_TYPE_TEXT = "text/plain";

	#endregion

	#region Page Events

	protected void Page_Load(object sender, EventArgs e)
	{
		SetupHeader();
		CreateDynaButton();
		ListControls(this.Controls, 1);
		Response.Write("<hr/>");
	}

	#endregion

	#region Event Handlers

	protected void dynaButton_Click(object sender, EventArgs e)
	{
		Debug.Assert(IsDynaButton);
		lblOutput.Text = "Dynamic button was clicked";
	}

	protected void btnClear_Click(object sender, EventArgs e)
	{
		lblOutput.Text = String.Empty;
	}

	protected void btnRemove_Click(object sender, EventArgs e)
	{
		RemoveDynaButton();
		IsDynaButton = false;
	}

	protected void btnCreate_Click(object sender, EventArgs e)
	{
		IsDynaButton = true;
		CreateDynaButton();
	}

	protected void btnCountLines_Click(object sender, EventArgs e)
	{
		if (txtFileName.HasFile && txtFileName.PostedFile.ContentLength != 0 && MIME_TYPE_TEXT == txtFileName.PostedFile.ContentType)
		{
			int lineCount = GetLineCount(new StreamReader(txtFileName.PostedFile.InputStream));
			lblOutput.Text = String.Format("The submitted file contains {0} line(s).", lineCount);
			Debug.WriteLine("A file was submitted!");
		}
		else
		{
			lblOutput.Text = "Wrong input";
		}
	}

	#endregion

	#region Implementation

	private bool IsDynaButton
	{
		get
		{
			return btnRemove.Visible;
		}
		set
		{
			btnRemove.Visible = value;
			btnCreate.Visible = !value;
		}
	}

	private void CreateDynaButton()
	{
		if (IsPostBack && IsDynaButton)
		{
			Button dynaButton = new Button();
			dynaButton.Text = "*** Dynamic Button ***";
			dynaButton.ID = DYNA_BUTTON_ID;
			dynaButton.Click += new EventHandler(dynaButton_Click);
			phDynaButtons.Controls.Add(dynaButton);
		}
	}

	private void RemoveDynaButton()
	{
		Control c = FindControl(DYNA_BUTTON_ID);
		if (c != null)
		{
			c.Parent.Controls.Remove(c);
		}
	}

	private void SetupHeader()
	{
		Page.Header.Title = "Dynamically generated title";
		Header.Controls.Add(CreateMetaControl("description", "A great site to learn ASP.NET"));
		Header.Controls.Add(CreateMetaControl("keywords", "ASP.NET,C#,.NET"));
	}

	private static Control CreateMetaControl(string name, string value)
	{
		HtmlMeta c = new HtmlMeta();
		c.Name = name;
		c.Content = value;
		return c;
	}

	private void ListControls(ControlCollection controlCollection, int depth)
	{
		foreach (Control c in controlCollection)
		{
			Response.Write(String.Format("{0}{1} - <b>{2}</b><br/>",
				(new String('>', depth << 1)).Replace(">", "&nbsp;"),
				c.GetType(),
				c.ID));

			LiteralControl lc = c as LiteralControl;
			if (lc != null)
			{
				Response.Write(String.Format("&nbsp;Text: {0}<br/>",
					Server.HtmlEncode(lc.Text)));
			}

			ListControls(c.Controls, depth + 1);
		}
	}

	private static int GetLineCount(TextReader reader)
	{
		int count = 0;
		using (reader)
		{
			while (reader.ReadLine() != null)
			{
				count++;
			}
		}
		return count;
	}

	#endregion
}
