<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shippers.aspx.cs" Inherits="Northwind.UI.Entities.ShippersPage" Title="Shippers" MasterPageFile="~/Northwind/Master/Northwind.master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
	<h2>Shippers</h2>
	<p>This page demonstrates usage of <a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.datalist.aspx" target="_blank">DataList</a>.
	</p>
	<asp:EntityDataSource ID="srcShippers" runat="server" ContextTypeName="Northwind.Data.Entities.NorthwindObjectContext"
		EntitySetName="Shippers" Select="it.[ShipperID], it.[CompanyName], it.[Phone]"
		ConnectionString="name=NorthwindObjectContext" EnableViewState="False" StoreOriginalValuesInViewState="False" />

	<div>
		<asp:DataList ID="dlShippers" runat="server" DataSourceID="srcShippers" 
			RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table"
			DataKeyField="ShipperID"
			OnSelectedIndexChanged="dlShippers_SelectedIndexChanged" >
		<HeaderTemplate>
			List of Shippers
		</HeaderTemplate>
		<ItemTemplate>
			<asp:Label ID="lblCompanyName" runat="server" Font-Bold="True" 
				Font-Size="Large" Font-Underline="True" Text='<%# Eval("CompanyName") %>'></asp:Label>
			<br />
			<table class="max_width">
				<tr>
					<td>
						<asp:LinkButton ID="cmdSelect" runat="server" CommandName="Select"
							CommandArgument='<%# Eval("ShipperID") %>'>Select</asp:LinkButton>
					</td>
					<td width="20px" />
					<td align="right">
						<%# Eval("Phone") %>
					</td>
				</tr>
			</table>
		</ItemTemplate>
		</asp:DataList>
    </div>
	<asp:Label ID="lblOutput" runat="server" />
</asp:Content>