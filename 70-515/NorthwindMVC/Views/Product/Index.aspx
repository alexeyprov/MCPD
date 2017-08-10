﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Northwind.Data.Entities.Product>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Products
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Products</h2>

    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Name
            </th>
            <th>
                Quantity per Unit
            </th>
            <th>
                Unit Price
            </th>
            <th>
                Units in Stock
            </th>
            <th>
                Units on Order
            </th>
            <th>
                Reorder Level
            </th>
            <th>
                Discontinued
            </th>
        </tr>

    <% foreach (var item in Model) 
	   { 
	%>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.ProductID }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.ProductID })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ProductID })%>
            </td>
            <td>
                <%: item.ProductID %>
            </td>
            <td>
                <%: item.ProductName %>
            </td>
            <td>
                <%: item.QuantityPerUnit %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.UnitPrice) %>
            </td>
            <td>
                <%: item.UnitsInStock %>
            </td>
            <td>
                <%: item.UnitsOnOrder %>
            </td>
            <td>
                <%: item.ReorderLevel %>
            </td>
            <td>
                <%: item.Discontinued %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>
