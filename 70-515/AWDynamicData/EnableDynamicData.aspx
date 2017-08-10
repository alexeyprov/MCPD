<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnableDynamicData.aspx.cs" Inherits="AWDynamicData.EnableDynamicData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    	<asp:ObjectDataSource ID="odsOrderDetails" runat="server" 
			DataObjectTypeName="AdventureWorks.Data.Linq.SalesOrderDetail" 
			SelectMethod="GetOrderDetails" 
			TypeName="AdventureWorks.Data.Linq.OrderDetailsComponent" 
			UpdateMethod="UpdateOrderDetail"></asp:ObjectDataSource>
		<asp:GridView ID="gvOrderDetails" runat="server" AllowPaging="True" 
			AutoGenerateColumns="True" DataSourceID="odsOrderDetails">
			<Columns>
				<asp:CommandField ShowEditButton="True" ShowSelectButton="True" />
			</Columns>
		</asp:GridView>
    </div>
    </form>
</body>
</html>
