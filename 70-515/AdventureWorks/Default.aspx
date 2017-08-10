<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="AdventureWorks._Default" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<h2>
		Welcome to AdventureWorks!
	</h2>
	<asp:ScriptManager runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/Scripts/RoleManagement.js" />
		</Scripts>
	</asp:ScriptManager>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AdventureWorks %>"
		SelectCommand="SELECT SalesLT.Product.* FROM SalesLT.Product"></asp:SqlDataSource>
	<asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID"
		DataSourceID="SqlDataSource1">
		<Columns>
			<asp:BoundField DataField="Name" HeaderText="Product" SortExpression="Name" />
			<asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
			<asp:BoundField DataField="ListPrice" HeaderText="ListPrice" SortExpression="ListPrice" />
			<asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size" />
			<asp:BoundField DataField="Weight" HeaderText="Weight" SortExpression="Weight" />
		</Columns>
	</asp:GridView>

	<div id="adminDiv" style="display:none">
		Click <a href="Admin/UserList.aspx">here</a> to see users list.
	</div>
</asp:Content>
