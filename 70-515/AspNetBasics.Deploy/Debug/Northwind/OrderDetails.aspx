<%@ page language="C#" autoeventwireup="true" inherits="OrderDetails, AspNetBasics.Deploy" theme="Blue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Order Details</title>
</head>
<body>
	<h1>Order Details</h1>
	<p>This page illustrates usage of <i>LINQ to SQL</i> with data binding.</p>
	<form id="form1" runat="server">
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
	</form>
</body>
</html>
