<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductsByCategory.aspx.cs" Inherits="Northwind_ProductsByCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Products by Categories</title>
</head>
<body>
	<h1>Categories and Products</h1>
	<p>This page demonstrates usage of:</p>
	<ul>
		<li>Hierarchical GridView</li>
		<li>DataSet composed of two DataTables with calculated columns</li>
		<li>LINQ to DataSet</li>
	</ul>
	
    <form id="form1" runat="server">
    <div>
		<asp:GridView ID="grdCategories" runat="server" AutoGenerateColumns="False"
			OnRowDataBound="grdCategories_RowDataBound" >
			<Columns>
				<asp:TemplateField HeaderText="Category">
					<ItemTemplate>
						<span><b><%# Eval("CategoryName") %></b> - (<%# Eval("Product Count") %> total)</span>
						<br />
						<br />
						<%# Eval("Description") %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Products">
					<ItemTemplate>
						<asp:GridView ID="grdProducts" runat="server" ShowFooter="true" AutoGenerateColumns="false"
							EnableSortingAndPagingCallbacks="true"
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
    </form>
</body>
</html>
