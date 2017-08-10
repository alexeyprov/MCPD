<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Suppliers.aspx.cs" Inherits="Northwind.UI.Linq.SuppliersPage" Title="Suppliers" MasterPageFile="~/Northwind/Master/Northwind.master"%>
<%@ Register Assembly="ControlExtensions" Namespace="ControlExtensions" TagPrefix="ce" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
		<h2>Suppliers</h2>
			<p>This page demonstrates usage of <a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.listview.aspx" target="_blank">ListView</a>.
				The ListView is bound to a 
				<a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.linqdatasource.aspx" 
					target="_blank">LinqDataSource</a>.
			</p>
		<div>
			<asp:LinqDataSource ID="srcSuppliers" runat="server" 
				ContextTypeName="Northwind.Data.Linq.NorthwindDataContext" EntityTypeName="" 
				Select="new (SupplierID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone)" 
				TableName="Suppliers" oncontextcreating="srcSuppliers_ContextCreating">
			</asp:LinqDataSource>
			<br />
			<asp:ListView ID="lstSuppliers" runat="server" DataSourceID="srcSuppliers" 
				DataKeyNames="SupplierID" GroupItemCount="3">
				<LayoutTemplate>
					<table cellpadding="2 px">
						<tr id="groupPlaceholder" runat="server" />
					</table>
					<asp:DataPager runat="server" ID="pgrSuppliers" PageSize="6">
						<Fields>
							<asp:NextPreviousPagerField ShowFirstPageButton="true" ShowLastPageButton="true"
								FirstPageText="&lt;&lt; " LastPageText=" &gt;&gt;" NextPageText=" &gt; " PreviousPageText=" &lt; " />
						</Fields>
					</asp:DataPager>
				</LayoutTemplate>
				<GroupTemplate>
					<tr>
						<td runat="server" id="itemPlaceholder" />
					</tr>
				</GroupTemplate>
				<ItemTemplate>
					<td>
						<b>
							<%# Eval("SupplierID")%>
							-
							<%# Eval("CompanyName") %>
							(
							<%# Eval("ContactName") %>
							,
							<%# Eval("ContactTitle") %>
							)
						</b>
						<hr />
						<small>
							<i>
								<%# Eval("Address") %><br />
								<%# Eval("City")%>,
								<%# Eval("Country")%>,
								<%# Eval("PostalCode")%><br />
								<%# Eval("Phone") %>
							</i>
							<br />
							<br />
							<%# Eval("Region")%>
							<br />
							<br />
						</small>
					</td>
				</ItemTemplate>
			</asp:ListView>
		</div>
		<asp:Label ID="lblOutput" runat="server" Visible="False"></asp:Label>
</asp:Content>