<%@ Page Title="" Language="C#" MasterPageFile="~/Northwind/Master/Northwind.master" AutoEventWireup="true" CodeFile="EmployeeSummary.aspx.cs" Inherits="Northwind.UI.Linq.EmployeeSummaryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NorthwindContentPlaceHolder" Runat="Server">
	<h2>Employee Summary</h2>
	<p>This page illustrates <i>LINQ to XML</i>, <i>LINQ to SQL</i> and the <i>Xml</i> control</p>
	<asp:Xml ID="xmlEmployees" TransformSource="~/Transforms/Employees2Html.xslt" runat="server" EnableViewState="False" />
</asp:Content>

