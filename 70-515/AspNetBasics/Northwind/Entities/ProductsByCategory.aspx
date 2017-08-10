<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductsByCategory.aspx.cs" Inherits="Northwind.UI.Entities.ProductsByCategoryPage" Title="Products by Category" MasterPageFile="~/Northwind/Master/Northwind.master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
	<h2>Categories and Products</h2>
	<p>This page demonstrates usage of:</p>
	<ul>
		<li>Hierarchical GridView</li>
		<li>LINQ to Entities</li>
	</ul>
	
    <div>
		<asp:GridView ID="grdCategories" runat="server" AutoGenerateColumns="False"
			OnRowDataBound="grdCategories_RowDataBound" >
			<Columns>
				<asp:TemplateField HeaderText="Category">
					<ItemTemplate>
						<span><b><%# Eval("Name") %></b> - (<%# Eval("ProductCount") %> total)</span>
						<br />
						<br />
						<%# Eval("Description") %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Products">
					<ItemTemplate>
						<asp:GridView ID="grdProducts" runat="server" ShowFooter="true" AutoGenerateColumns="false"
							EnableSortingAndPagingCallbacks="true" AllowSorting="true" Width="250px"
							OnDataBound="grdProducts_DataBound">
							<Columns>
								<asp:BoundField DataField="ProductName" HeaderText="Name" SortExpression="ProductName" />
								<asp:BoundField DataField="UnitPrice" HeaderText="Price" SortExpression="UnitPrice"
									 DataFormatString="{0:C}" />
							</Columns>
						</asp:GridView>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
    </div>
</asp:Content>