﻿<%@ Page Title="Orders" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NorthwindMVC.Models.PagedViewModel<Northwind.Data.Entities.Order>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Orders
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Orders</h2>

	<%: Html.Grid(Model.Items) %>
	<br />
	<%: Html.Pager("Index", Model.PageIndex, Model.PageCount) %>

</asp:Content>
