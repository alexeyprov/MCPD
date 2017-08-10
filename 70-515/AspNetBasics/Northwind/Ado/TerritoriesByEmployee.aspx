<%@ Page Language="C#" AutoEventWireup="true" Async="true" AsyncTimeout="60" CodeFile="TerritoriesByEmployee.aspx.cs" Inherits="Northwind.UI.Ado.TerritoriesByEmployeePage" Title="Territories by Employee" MasterPageFile="~/Northwind/Master/Northwind.master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
	<h2>Territiories by Employee</h2>
	<p>This is a sample asynchronous page.</p>
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
</asp:Content>