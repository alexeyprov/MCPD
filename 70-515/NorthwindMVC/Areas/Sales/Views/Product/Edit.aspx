<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Northwind.Data.Entities.Product>" %>
<%@ Import Namespace="NorthwindMVC.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2> Edit Product</h2>
	<% using (Html.BeginForm())
		{
	%>
	<fieldset>
		<legend>Product Data</legend>
		<p>
			<%: Html.ValidationSummary("Edit was unsuccessful. Plese correct the errors below and try again")%>
		</p>
		<table>
			<tr>
				<td>
					<%: Html.LabelFor(model => model.ProductName) %>
				</td>
				<td>
					<%: Html.TextBoxFor(model => model.ProductName) %>
					<%: Html.ValidationMessageFor(model => model.ProductName) %>
				</td>
			</tr>
			<tr>
				<td>
					<%: Html.LabelFor(model => model.QuantityPerUnit) %>
				</td>
				<td>
					<%: Html.TextBoxFor(model => model.QuantityPerUnit) %>
					<%: Html.ValidationMessageFor(model => model.QuantityPerUnit) %>
				</td>
			</tr>
			<tr>
				<td>
					<%: Html.LabelFor(model => model.UnitPrice) %>
				</td>
				<td>
					<%: Html.TextBoxFor(model => model.UnitPrice, new
							{
								Value = String.Format("{0:F}", Model.UnitPrice)
							})
					%>
					<%: Html.ValidationMessageFor(model => model.UnitPrice) %>
				</td>
			</tr>
			<tr>
				<td>
					<%: Html.LabelFor(model => model.UnitsInStock) %>
				</td>
				<td>
					<%: Html.TextBoxFor(model => model.UnitsInStock) %>
					<%: Html.ValidationMessageFor(model => model.UnitsInStock) %>
				</td>
			</tr>
			<tr>
				<td>
					<%: Html.LabelFor(model => model.UnitsOnOrder) %>
				</td>
				<td>
					<%: Html.TextBoxFor(model => model.UnitsOnOrder) %>
					<%: Html.ValidationMessageFor(model => model.UnitsOnOrder) %>
				</td>
			</tr>
			<tr>
				<td>
					<%: Html.LabelFor(model => model.ReorderLevel) %>
				</td>
				<td>
					<%: Html.TextBoxFor(model => model.ReorderLevel) %>
					<%: Html.ValidationMessageFor(model => model.ReorderLevel) %>
				</td>
			</tr>
			<tr>
				<td>
					<%: Html.LabelFor(model => model.Discontinued) %>
				</td>
				<td>
					<%: Html.CheckBoxFor(model => model.Discontinued) %>
					<%: Html.ValidationMessageFor(model => model.Discontinued) %>
				</td>
			</tr>
			<tr>
				<td>
					<%: Html.LabelFor(model => model.Supplier) %>
				</td>
				<td>
					<%: Html.DropDownListFor(model => model.SupplierID,
						ViewData[ConstantsHelper.SUPPLIERS_VIEW_DATA_KEY] as IEnumerable<SelectListItem>) %>
				</td>
			</tr>
			<tr>
				<td>
					<%: Html.LabelFor(model => model.Category) %>
				</td>
				<td>
					<%: Html.DropDownListFor(model => model.CategoryID,
						ViewData[ConstantsHelper.CATEGORIES_VIEW_DATA_KEY] as IEnumerable<SelectListItem>) %>
				</td>
			</tr>
		</table>
		<p>
			<input type="submit" value="Save" />
		</p>
	</fieldset>
	<% } %>
	<div>
		<%: Html.ActionLink("Back to List", "Index") %>
	</div>
</asp:Content>
