<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BootDetails.aspx.cs" Inherits="BootCloset_BootDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
		<table style="border-style: none; border-width: 0px">
			<tr>
				<td><b>Item name:</b></td>
				<td><asp:Label runat="server" ID="lblName"></asp:Label></td>
			</tr>
			<tr>
				<td><b>SKU:</b></td>
				<td><asp:Label runat="server" ID="lblSKU"></asp:Label></td>
			</tr>
			<tr>
				<td><b>Height:</b></td>
				<td><asp:Label runat="server" ID="lblHeight"></asp:Label></td>
			</tr>
			<tr>
				<td><b>Color:</b></td>
				<td><asp:Label runat="server" ID="lblColor"></asp:Label></td>
			</tr>
			<tr>
				<td><b>Price:</b></td>
				<td><asp:Label runat="server" ID="lblPrice"></asp:Label></td>
			</tr>
		</table>
    </form>
</body>
</html>
