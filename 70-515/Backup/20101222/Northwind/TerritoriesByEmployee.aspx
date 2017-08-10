<%@ Page Language="C#" AutoEventWireup="true" Async="true" AsyncTimeout="60" CodeFile="TerritoriesByEmployee.aspx.cs" Inherits="Northwind_TerritoriesByEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Territories by Employee</title>
</head>
<body>
	<h1>Territiories by Employee</h1>
	<p>This is a sample asynchronous page.</p>
    <form id="form1" runat="server">
    <div>
		<asp:GridView ID="gvwTerritories" runat="server" AutoGenerateColumns="false" >
			<Columns>
				<asp:BoundField DataField="FIRSTNAME" HeaderText="Employee First Name" 
					SortExpression="FIRSTNAME" />
				<asp:BoundField DataField="LASTNAME" HeaderText="Employee Last Name" 
					SortExpression="LASTNAME" />
				<asp:BoundField DataField="TERRITORYID" HeaderText="Territory Code" 
					SortExpression="TERRITORYID" />
				<asp:BoundField DataField="TERRITORYDESCRIPTION" HeaderText="Territory Name" 
					SortExpression="TERRITORYDESCRIPTION" />
			</Columns>
		</asp:GridView>
		<br />
		<asp:Label ID="lblCityFacts" runat="server" Font-Size="Medium" BorderColor="AliceBlue" BorderStyle="Outset" ForeColor="BlueViolet" />
		<br />
		<asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red" />
    </div>
    </form>
</body>
</html>
