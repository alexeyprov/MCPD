﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="Northwind_Customers" %>
<%@ Register Assembly="ControlExtensions" Namespace="ControlExtensions" TagPrefix="ce" %>
<%@ Import Namespace="Northwind" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Customers</title>
</head>
<body>
	<h1>Customers</h1>
	<p>This page demonstrates usage of GridView with the ObjectDataSource. DetailsView is used to
	enter new data.</p>
    <form id="frm" runat="server">
    <div>
		<asp:Panel ID="pnlData" runat="server">
		
			<ce:CompositeObjectDataSource ID="srcCustomers" runat="server" 
				TypeName="Northwind.CustomerData" DataObjectTypeName="Northwind.Customer" 
				DeleteMethod="DeleteCustomer" InsertMethod="InsertCustomer" 
				SelectMethod="GetAllCustomers" UpdateMethod="UpdateCustomer" 
				ondeleted="dataSource_SqlCommandDone" oninserted="dataSource_SqlCommandDone" 
				onobjectcreating="srcCustomers_ObjectCreating" 
				onselected="dataSource_SqlCommandDone" onupdated="dataSource_SqlCommandDone" 
				SortParameterName="sortExpression" 
				SelectCountMethod="GetCustomerCount">
				<DeleteParameters>
					<asp:Parameter Name="id" Type="String" />
				</DeleteParameters>
			</ce:CompositeObjectDataSource>
			
			<asp:GridView ID="grdCustomers" runat="server" AutoGenerateColumns="False" 
				DataSourceID="srcCustomers" DataKeyNames="ID" AutoGenerateDeleteButton="True" 
				AutoGenerateEditButton="True" AllowSorting="True"
				AllowPaging="True" PageSize="20"
				EnableSortingAndPagingCallbacks="false" 
				onselectedindexchanged="grdCustomers_SelectedIndexChanged">
				<PagerSettings Mode="NextPrevious" NextPageText="Forward &gt;" 
					PreviousPageText="&lt; Back" />
				<Columns>
					<asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="&gt;" />
					<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
					<asp:BoundField DataField="CompanyName" HeaderText="Company" 
						SortExpression="CompanyName" />
					<ce:CompositeBoundField DataField="Contact.Title" HeaderText="Contact Title"
						SortExpression="Contact.Title" />
					<ce:CompositeBoundField DataField="Contact.Name" HeaderText="Contact Name"
						SortExpression="Contact.Name" />
					<ce:CompositeBoundField DataField="Address.StreetAddress" HeaderText="Address"
						SortExpression="Address.StreetAddress" />						
					<ce:CompositeBoundField DataField="Address.City" HeaderText="City"
						SortExpression="Address.City" />
					<ce:CompositeBoundField DataField="Address.Region" HeaderText="Region"
						SortExpression="Address.Region" />
					<ce:CompositeBoundField DataField="Address.PostalCode" HeaderText="Postal Code"
						SortExpression="Address.PostalCode" />
					<ce:CompositeBoundField DataField="Address.Country" HeaderText="Country"
						SortExpression="Address.Country" />
					<ce:CompositeBoundField DataField="Contact.Phone" HeaderText="Phone"
						SortExpression="Contact.Phone" />
					<ce:CompositeBoundField DataField="Contact.Fax" HeaderText="Fax"
						SortExpression="Contact.Fax" />
				</Columns>
			</asp:GridView>
			
			<br />
			New Customer:
			<asp:DetailsView ID="dvwNewCustomer" runat="server" 
				AutoGenerateInsertButton="True" AutoGenerateRows="False"
				DataKeyNames="ID" DataSourceID="srcCustomers" DefaultMode="Insert" 
				Height="50px" Width="125px">
				<Fields>
					<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
					<asp:BoundField DataField="CompanyName" HeaderText="CompanyName" 
						SortExpression="CompanyName" />
					<ce:CompositeBoundField DataField="Contact.Title" HeaderText="Contact Title"
						SortExpression="Contact.Title" />
					<ce:CompositeBoundField DataField="Contact.Name" HeaderText="Contact Name"
						SortExpression="Contact.Name" />
					<ce:CompositeBoundField DataField="Address.StreetAddress" HeaderText="Address"
						SortExpression="Address.StreetAddress" />						
					<ce:CompositeBoundField DataField="Address.City" HeaderText="City"
						SortExpression="Address.City" />
					<ce:CompositeBoundField DataField="Address.Region" HeaderText="Region"
						SortExpression="Address.StreetAddress" />
					<ce:CompositeBoundField DataField="Address.PostalCode" HeaderText="Postal Code"
						SortExpression="Address.PostalCode" />
					<ce:CompositeBoundField DataField="Address.Country" HeaderText="Country"
						SortExpression="Address.Country" />
					<ce:CompositeBoundField DataField="Contact.Phone" HeaderText="Phone"
						SortExpression="Contact.Phone" />
					<ce:CompositeBoundField DataField="Contact.Fax" HeaderText="Fax"
						SortExpression="Contact.Fax" />
				</Fields>
			</asp:DetailsView>
			
		</asp:Panel>
		<asp:Label runat="server" ID="lblError" 
			Text="An error ocurred during accessing the database. Please try again later." 
			ForeColor="Red"
			Visible="false" />    
    </div>
    </form>
</body>
</html>
