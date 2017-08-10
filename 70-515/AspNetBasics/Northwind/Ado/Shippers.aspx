<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shippers.aspx.cs" Inherits="Northwind.UI.Ado.ShippersPage" Title="Shippers" MasterPageFile="~/Northwind/Master/Northwind.master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
	<h2>Shippers</h2>
	<p>This page demonstrates usage of <a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.datalist.aspx" target="_blank">DataList</a>.
	</p>
	<asp:SqlDataSource ID="srcSuppliers" runat="server" ProviderName="System.Data.SqlClient"
		ConnectionString="<%$ ConnectionStrings: Northwind %>" DataSourceMode="DataReader"
		SelectCommand="
SELECT SHIPPERID,
	   COMPANYNAME,
	   PHONE
  FROM [DBO].SHIPPERS"
		/>

	<div>
		<asp:DataList ID="dlSuppliers" runat="server" DataSourceID="srcSuppliers" 
			RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table"
			DataKeyField="SHIPPERID"
			OnSelectedIndexChanged="dlSuppliers_SelectedIndexChanged" >
		<HeaderTemplate>
			List of Suppliers
		</HeaderTemplate>
		<ItemTemplate>
			<asp:Label ID="lblCompanyName" runat="server" Font-Bold="True" 
				Font-Size="Large" Font-Underline="True" Text='<%# Eval("COMPANYNAME") %>'></asp:Label>
			<br />
			<table class="max_width">
				<tr>
					<td>
						<asp:LinkButton ID="cmdSelect" runat="server" CommandName="Select"
							CommandArgument='<%# Eval("SHIPPERID") %>'>Select</asp:LinkButton>
					</td>
					<td width="20px" />
					<td align="right">
						<%# Eval("PHONE") %>
					</td>
				</tr>
			</table>
		</ItemTemplate>
		</asp:DataList>
    </div>
	<asp:Label ID="lblOutput" runat="server" />
</asp:Content>