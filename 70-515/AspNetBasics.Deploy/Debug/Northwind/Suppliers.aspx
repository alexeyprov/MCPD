<%@ page language="C#" autoeventwireup="true" inherits="Northwind_Suppliers, AspNetBasics.Deploy" theme="Blue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Suppliers</title>
</head>
<body>
    <form id="frm" runat="server">
		<h1>Suppliers</h1>
		<p>This page demonstrates usage of ListView.</p>
		<div>
			<asp:SqlDataSource ID="srcSuppliers" runat="server" ConnectionString="<%$ ConnectionStrings: Northwind %>"
				 ProviderName="System.Data.SqlClient" 
				 SelectCommand="
SELECT SUPPLIERID,
       COMPANYNAME,
       CONTACTNAME,
       CONTACTTITLE,
       ADDRESS,
       CITY,
       COUNTRY,
       POSTALCODE,
       PHONE,
       REGION
  FROM DBO.SUPPLIERS" />
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
							<%# Eval("SupplierID") %>
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
								<%# Eval("City") %>,
								<%# Eval("Country") %>,
								<%# Eval("PostalCode") %><br />
								<%# Eval("Phone") %>
							</i>
							<br />
							<br />
							<%# Eval("Region") %>
							<br />
							<br />
						</small>
					</td>
				</ItemTemplate>
			</asp:ListView>
		</div>
		<asp:Label ID="lblOutput" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>