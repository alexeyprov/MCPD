<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
	<ul>
		<li><%: Html.ActionLink("Sales", "Index", "Product",
			new { area="Sales" }, null) %></li>
		<li><%: Html.RouteLink("Data Management", "DataManagement_default",
			new { controller="Supplier" }, null) %></li>
	</ul>
</asp:Content>
