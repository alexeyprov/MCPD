﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Northwind.Data.Entities.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Delete Product</h2>
	<h3>Are you sure you want to delete the product below?</h3>
	<fieldset>
		<legend>Product Details</legend>
		<table>
			<tr>
				<td>
					ID
				</td>
				<td>
					<%: Model.ProductID %>
				</td>
			</tr>
			<tr>
				<td>
					Name
				</td>
				<td>
					<%: Model.ProductName %>
				</td>
			</tr>
			<tr>
				<td>
					Category
				</td>
				<td>
					<%: Model.Category.CategoryName %>
				</td>
			</tr>
			<tr>
				<td>
					Supplier
				</td>
				<td>
					<%: Model.Supplier.CompanyName %>
				</td>
			</tr>
			<tr>
				<td>
					Quantity per Unit
				</td>
				<td>
					<%: Model.QuantityPerUnit %>
				</td>
			</tr>
			<tr>
				<td>
					Unit Price
				</td>
				<td>
					<%: String.Format("{0:F}", Model.UnitPrice) %>
				</td>
			</tr>
			<tr>
				<td>
					Units in Stock
				</td>
				<td>
					<%: Model.UnitsInStock %>
				</td>
			</tr>
			<tr>
				<td>
					Units on Order
				</td>
				<td>
					<%: Model.UnitsOnOrder %>
				</td>
			</tr>
			<tr>
				<td>
					ReorderLevel
				</td>
				<td>
					<%: Model.ReorderLevel %>
				</td>
			</tr>
			<tr>
				<td>
					Discontinued
				</td>
				<td>
					<%: Html.CheckBoxFor(m => m.Discontinued, new { disabled = true }) %>
				</td>
			</tr>
		</table>
	</fieldset>
	<% using (Html.BeginForm())
		{
	%>
	<p>
		<input type="submit" value="Delete" />
		|
		<%: Html.ActionLink("Back to List", "Index") %>
	</p>
	<% } %>
</asp:Content>