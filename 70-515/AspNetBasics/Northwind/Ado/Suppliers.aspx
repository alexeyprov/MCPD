<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Suppliers.aspx.cs" Inherits="Northwind.UI.Ado.SuppliersPage" Title="Suppliers" MasterPageFile="~/Northwind/Master/Northwind.master"%>
<%@ Register Assembly="ControlExtensions" Namespace="ControlExtensions" TagPrefix="ce" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
		<h2>Suppliers</h2>
			<p>This page demonstrates usage of <a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.listview.aspx" target="_blank">ListView</a>.
				The ListView is bound to a <a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.objectdatasource.aspx" target="_blank">ObjectDataSource</a>
				that uses custom paging.
			</p>
		<div>
			<asp:ObjectDataSource ID="srcSuppliers" runat="server" 
				SelectMethod="GetAllSuppliers" SelectCountMethod="GetSupplierCount"
				TypeName="Northwind.Data.ClassicAdo.SupplierData" 
				DataObjectTypeName="Northwind.Supplier"
				EnablePaging="true"
				StartRowIndexParameterName="startRow" MaximumRowsParameterName="maxRows" 
				onobjectcreating="srcSuppliers_ObjectCreating" />
			<br />
			<asp:ListView ID="lstSuppliers" runat="server" DataSourceID="srcSuppliers" 
				DataKeyNames="ID" GroupItemCount="3">
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
							<%# Eval("ID") %>
							-
							<%# Eval("CompanyName") %>
							(
							<%# Eval("Contact.Name") %>
							,
							<%# Eval("Contact.Title") %>
							)
						</b>
						<hr />
						<small>
							<i>
								<%# Eval("Address.StreetAddress") %><br />
								<%# Eval("Address.City")%>,
								<%# Eval("Address.Country")%>,
								<%# Eval("Address.PostalCode")%><br />
								<%# Eval("Contact.Phone") %>
							</i>
							<br />
							<br />
							<%# Eval("Address.Region")%>
							<br />
							<br />
						</small>
					</td>
				</ItemTemplate>
			</asp:ListView>
		</div>
		<asp:Label ID="lblOutput" runat="server" Visible="False"></asp:Label>
</asp:Content>