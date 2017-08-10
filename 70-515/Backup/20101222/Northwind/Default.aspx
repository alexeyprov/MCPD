<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Northwind_Default" %>
<%@ OutputCache Duration="60" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Northwind</title>
	<link type="text/css" rel="Stylesheet" href="../App_Themes/MasterStyleSheet.css" />
</head>
<body>
	<h1>Northwind DB Samples</h1>
	<p>This page illustrates basics of ASP.NET data binding using the Northwind sample SQL Server database.</p>
	<p>You can review the following practices:</p>
	<ul>
		<li><a href="Employees.aspx">Templated <i>GridView</i> demo</a></li>
		<li><a href="Suppliers.aspx"><i>ListView</i> demo</a></li>
        <li><a href="ProductsByCategory.aspx">Hierarchical <i>GridView</i> and <i>DataSet</i> demo</a></li>
        <li><a href="Territories.aspx"><i>SqlDataSource</i> demo</a></li>
        <li><a href="Customers.aspx"><i>ObjectDataSource</i> demo</a></li>
        <li><a href="TerritoriesByEmployee.aspx">Asynchronous pages demo</a></li>
    </ul>

    <form id="frm" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
