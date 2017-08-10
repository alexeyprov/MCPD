<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="Northwind.UI.Ado.OrderDetailsPage" Title="Order Details" MasterPageFile="~/Northwind/Master/Northwind.master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
	<h2>Order Details</h2>
	<p>This page illustrates usage of <i>LINQ to SQL</i> with data binding.</p>
	<div>
		<asp:Label ID="lblOrderInfo" runat="server" />
		<asp:GridView runat="server" ID="grdLines" AutoGenerateColumns="false">
			<Columns>
				<asp:BoundField DataField="ProductID" HeaderText="Product" />
				<asp:BoundField DataField="UnitPrice" HeaderText="Price per item" DataFormatString="{0:C}"/>
				<asp:BoundField DataField="Quantity" HeaderText="Quantity" />
				<asp:BoundField DataField="Discount" HeaderText="Discount" DataFormatString="{0:P}"/>
			</Columns>
		</asp:GridView>
		<br />
	</div>
</asp:Content>