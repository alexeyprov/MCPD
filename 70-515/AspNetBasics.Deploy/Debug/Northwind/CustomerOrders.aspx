<%@ page language="C#" autoeventwireup="true" inherits="Northwind_CustomerOrders, AspNetBasics.Deploy" theme="Blue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Customer Orders</title>
</head>
<body>
	<h1>Customer Orders</h1>
	<p>This page illustrates usage of <i>LINQ to objects</i> with data binding.</p>
	<form id="form1" runat="server">
	<div>
		<asp:Label ID="lblCustomerOrders" runat="server" />
		<asp:GridView runat="server" ID="grdOrders" AutoGenerateColumns="False" DataKeyNames="ID" 
			onselectedindexchanged="grdOrders_SelectedIndexChanged">
			<Columns>
				<asp:CommandField SelectText="&gt;" ShowCancelButton="False" 
					ShowSelectButton="True" />
				<asp:BoundField DataField="OrderedDate" HeaderText="OrderedOn" DataFormatString="{0:d}"/>
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
	</form>
</body>
</html>
