<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Northwind.UI.Linq.DefaultPage"
	EnableViewState="False" MasterPageFile="~/Northwind/Master/Northwind.master"
	Title="Northwind LINQ to SQL" %>

<%@ OutputCache Duration="60" VaryByParam="None" %>

<asp:Content ContentPlaceHolderID="NorthwindContentPlaceHolder" runat="server">
	<h2>Northwind DB Samples (LINQ to SQL)</h2>
	<p>This page illustrates basics of ASP.NET data binding using LINQ to SQL.</p>
	<p>You can review the following practices:</p>
	<ul>
		<li><a href="Employees.aspx">Templated <i>GridView</i> demo</a></li>
		<li><a href="Suppliers.aspx"><i>ListView</i> demo</a></li>
		<li><a href="Shippers.aspx"><i>DataList</i> demo</a></li>
        <li><a href="ProductsByCategory.aspx">Hierarchical <i>GridView</i> and <i>DataSet</i> demo</a></li>
        <li><a href="Territories.aspx">Master-Detail <i>LinqDataSource</i> demo</a></li>
        <li><a href="Customers.aspx">CRUD <i>LinqDataSource</i> demo</a></li>
    </ul>

</asp:Content>