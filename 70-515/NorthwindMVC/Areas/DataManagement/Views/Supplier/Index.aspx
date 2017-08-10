<%@ Page Title="Suppliers" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NorthwindMVC.Models.PagedViewModel<Northwind.Data.Entities.Supplier>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Suppliers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Suppliers</h2>

	<%: Html.Grid(Model.Items) %>
	<br />
	<%: Html.Pager("Index", Model.PageIndex, Model.PageCount) %>

</asp:Content>
