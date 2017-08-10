<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Northwind.UI.Ado.DefaultPage"
	EnableViewState="False" MasterPageFile="~/Northwind/Master/Northwind.master"
	Title="Northwind ADO.NET" %>

<%@ OutputCache Duration="60" VaryByParam="None" %>
<asp:Content ContentPlaceHolderID="NorthwindContentPlaceHolder" runat="server">
	<h2>
		Northwind DB Samples (ADO.NET)</h2>
	<p>
		This page illustrates basics of ASP.NET data binding using custom data access code
		and page-embedded SQL.</p>
	<p>
		You can review the following practices:</p>
	<ul>
		<li><a href="Employees.aspx">Templated <i>GridView</i> demo</a></li>
		<li><a href="Suppliers.aspx"><i>ListView</i> demo</a></li>
		<li><a href="Shippers.aspx"><i>DataList</i> demo</a></li>
		<li><a href="ProductsByCategory.aspx">Hierarchical <i>GridView</i> and <i>DataSet</i>
			demo</a></li>
		<li><a href="Territories.aspx"><i>SqlDataSource</i> demo</a></li>
		<li><a href="Customers.aspx"><i>ObjectDataSource</i> demo</a></li>
		<li><a href="TerritoriesByEmployee.aspx">Asynchronous pages demo</a></li>
	</ul>
</asp:Content>
