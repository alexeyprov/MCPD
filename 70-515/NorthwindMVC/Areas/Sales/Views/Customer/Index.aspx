<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Northwind.Data.Entities.Customer>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		<%: ViewData[ConstantsHelper.CAPTION_VIEW_DATA_KEY] %>
	</h2>

	<script type="text/javascript" language="javascript">
		<!--
		$(document).ready(
			function ()
			{
				$("#AppContent").tabs();
			}
		);

		function OnEditCustomer(customerId,
								customerName,
								customerContact,
								customerAddress,
								customerCity, 
								customerCountry)
		{
			OpenCustomerDialog(customerId,
								customerName,
								customerContact,
								customerAddress,
								customerCity,
								customerCountry);
		}

		//-->
	</script>
	<div id="AppContent">
		<ul>
			<li><a href="#tabCustomers"><b>Customers</b></a></li>
			<li><a href="#tabOrders"><b>Orders</b></a></li>
			<li><a href="#tabOther"><b>Other</b></a></li>
		</ul>
		<br />
		<div id="tabCustomers">
			<span style="font-size: 125%"><b>Customers</b></span>
			<hr style="border-width: 2px; border-color: Black" />
			<% Html.RenderPartial("CustomerListUserControl", Model); %>
			<p>
				<%: Html.ActionLink("Create New", "Create") %>
			</p>
		</div>
		<div id="tabOrders">
			<span style="font-size: 125%"><b>Orders</b></span>
			<hr style="border-width: 2px; border-color: Black" />
		</div>
		<div id="tabOther">
			<span style="font-size: 125%"><b>Other</b></span>
			<hr style="border-width: 2px; border-color: Black" />
		</div>
	</div>

	<% Html.RenderPartial("CustomerEditDialogUserControl", ViewData[ConstantsHelper.COUNTRIES_VIEW_DATA_KEY]); %>
</asp:Content>
