<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Suppliers.aspx.cs" Inherits="Northwind.UI.Entities.SuppliersPage" Title="Suppliers" MasterPageFile="~/Northwind/Master/Northwind.master"%>
<%@ Register Assembly="ControlExtensions" Namespace="ControlExtensions" TagPrefix="ce" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
		<h2>Suppliers</h2>
			<p>This page demonstrates usage of <a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.listview.aspx" target="_blank">ListView</a>.
				The ListView is bound to an 
				<a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.entitydatasource.aspx" 
					target="_blank">EntityDataSource</a>.
			</p>
		<div>
			<span>Auto-search:</span>
			<asp:TextBox ID="txtAutoSearch" runat="server" AutoPostBack="true" 
				ontextchanged="txtAutoSearch_TextChanged" />

			<%--
			BUG: QueryExtender doesn't work with projections, so this property was taken out:
			Select="it.SupplierID, it.CompanyName, it.ContactName, it.ContactTitle, it.Address, it.City, it.Region, it.PostalCode, it.Country, it.Phone" 
			--%>

			<asp:EntityDataSource ID="srcSuppliers" runat="server" 
				ContextTypeName="Northwind.Data.Entities.NorthwindObjectContext" 
				ConnectionString="name=NorthwindObjectContext" 
				EntitySetName="Suppliers" EnableViewState="false" StoreOriginalValuesInViewState="false"
				OrderBy="it.SupplierID" >
			</asp:EntityDataSource>

			<asp:QueryExtender ID="qexSuppliers" runat="server" TargetControlID="srcSuppliers">
				<asp:SearchExpression DataFields="CompanyName, ContactName" SearchType="StartsWith">
					<asp:ControlParameter ControlID="txtAutoSearch" />
				</asp:SearchExpression>
			</asp:QueryExtender>

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