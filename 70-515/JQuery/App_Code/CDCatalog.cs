using System;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Collections;

public class CD
{
	public string Title
	{
		get;
		set;
	}

	public string Artist
	{
		get;
		set;
	}

	public string Company
	{
		get;
		set;
	}

	public double Price
	{
		get;
		set;
	}
}

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class CDCatalog : WebService
{
	[WebMethod]
	public CD[] GetCDCatalog()
	{
		XDocument docXML = XDocument.Load("http://www.w3schools.com/xml/cd_catalog.xml");

		var CDs =
		  from cd in docXML.Descendants("CD")
		  select new CD
		  {
			  Title = cd.Element("TITLE").Value,
			  Artist = cd.Element("ARTIST").Value,
			  Company = cd.Element("COMPANY").Value,
			  Price = Convert.ToDouble(cd.Element("PRICE").Value),
		  };

		return CDs.ToArray<CD>();
	}
}