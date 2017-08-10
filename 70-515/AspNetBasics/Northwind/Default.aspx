<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Northwind.UI.DefaultPage" EnableViewState="false" 
	Title="Northwind" MasterPageFile="~/Northwind/Master/Northwind.master" %>

<asp:Content ContentPlaceHolderID="NorthwindContentPlaceHolder" runat="server">
	<h2>Northwind DB Samples</h2>
	<p>This page is a starting point for three basic techniques for ASP.NET data binding</p>
	<ul>
		<li><a href="Ado/Default.aspx">Custom components, <i>ObjectDataSource</i> and <i>SqlDataSource</i></a></li>
		<li><a href="Linq/Default.aspx">LINQ to SQL and <i>LinqDataSource</i></li>
		<li><a href="Entities/Default.aspx">LINQ to Entities and <i>EntityDataSource</i></li>
    </ul>
</asp:Content>