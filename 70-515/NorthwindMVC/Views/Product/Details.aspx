<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Northwind.Data.Entities.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		Product Details
	</h2>
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
	<p>
		<%: Html.ActionLink("Edit", "Edit", new { id=Model.ProductID }) %>
		|
		<%: Html.ActionLink("Delete", "Delete", new { id=Model.ProductID }) %>
		|
		<%: Html.ActionLink("Back to List", "Index") %>
	</p>
</asp:Content>
