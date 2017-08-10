using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

using Northwind.Data.Linq;

namespace Northwind.UI.Linq
{
	public partial class EmployeeSummaryPage : Northwind.UI.NorthwindBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;

			NorthwindDataContext context = new NorthwindDataContext(connString);
			
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XElement("Employees",
					from employee in context.Employees
					where employee.EmployeeID > 3
					select new XElement[] 
					{
						new XElement("Employee",
							new XElement("ID", employee.EmployeeID),
							new XElement("Name", employee.FirstName + " " + employee.LastName),
							new XElement("Position", employee.Title)
									)
					}
							)
										);

			xmlEmployees.DocumentContent = doc.ToString();
		}
	} 
}