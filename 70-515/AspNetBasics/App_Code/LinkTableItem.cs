using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LinkItem
/// </summary>
[Serializable]
public class LinkTableItem
{
	public LinkTableItem()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	public LinkTableItem(string text, string url)
	{
		this.Text = text;
		this.Url = url;
	}

	public string Text
	{
		get;
		set;
	}
	public string Url
	{
		get;
		set;
	}
}