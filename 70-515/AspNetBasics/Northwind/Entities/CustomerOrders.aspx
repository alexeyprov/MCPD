<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerOrders.aspx.cs" Inherits="Northwind.UI.Entities.CustomerOrdersPage" MasterPageFile="~/Northwind/Master/Northwind.master" Title="Customer Orders" %>

<asp:Content runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
	<h2>Customer Orders</h2>
	<p>This page illustrates usage of <i>LINQ to Entities</i> with data binding.</p>
	<div>
		<asp:Label ID="lblCustomerOrders" runat="server" />
		<asp:GridView runat="server" ID="grdOrders" AutoGenerateColumns="False" DataKeyNames="OrderID" 
			onselectedindexchanged="grdOrders_SelectedIndexChanged">
			<Columns>
				<asp:CommandField SelectText="&gt;" ShowCancelButton="False" 
					ShowSelectButton="True" />
				<asp:BoundField DataField="OrderDate" HeaderText="Ordered On" DataFormatString="{0:d}"/>
				<asp:BoundField DataField="Freight" HeaderText="Freight" DataFormatString="{0:C}"/>
			</Columns>
		</asp:GridView>
		<br />
	</div>
	<div>
		<span>Orders by year</span>
		<asp:GridView runat="server" ID="grdOrderHistory"
			AutoGenerateColumns="false" >
			<Columns>
				<asp:BoundField DataField="Year" HeaderText="Year" />
				<asp:BoundField DataField="Count" HeaderText="Order Count" />
				<asp:BoundField DataField="Average" HeaderText="Average Freight" DataFormatString="{0:C}"/>
				<asp:BoundField DataField="Maximum" HeaderText="Maximum Freight" DataFormatString="{0:C}"/>
			</Columns>
		</asp:GridView>
	</div>
</asp:Content>