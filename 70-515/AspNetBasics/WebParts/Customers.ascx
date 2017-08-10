<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Customers.ascx.cs" Inherits="WebParts.UI.CustomersControl" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
	DataKeyNames="CustomerID" DataSourceID="dsCustomers" 
	EmptyDataText="There are no data records to display.">
	<Columns>
		<asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" 
			SortExpression="CustomerID" />
		<asp:BoundField DataField="CompanyName" HeaderText="CompanyName" 
			SortExpression="CompanyName" />
		<asp:BoundField DataField="ContactFirstname" HeaderText="ContactFirstname" 
			SortExpression="ContactFirstname" />
		<asp:BoundField DataField="ContactLastname" HeaderText="ContactLastname" 
			SortExpression="ContactLastname" />
		<asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" 
			SortExpression="PhoneNumber" />
		<asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" 
			SortExpression="EmailAddress" />
		<asp:BoundField DataField="Address" HeaderText="Address" 
			SortExpression="Address" />
		<asp:BoundField DataField="ZipCode" HeaderText="ZipCode" 
			SortExpression="ZipCode" />
		<asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
		<asp:BoundField DataField="Country" HeaderText="Country" 
			SortExpression="Country" />
		<asp:BoundField DataField="WebSite" HeaderText="WebSite" 
			SortExpression="WebSite" />
	</Columns>
</asp:GridView>
<asp:SqlDataSource ID="dsCustomers" runat="server" 
	ConnectionString="<%$ ConnectionStrings:WebPartsTest %>" 
	DeleteCommand="DELETE FROM [Customer] WHERE [CustomerID] = @CustomerID" 
	InsertCommand="INSERT INTO [Customer] ([CustomerID], [CompanyName], [ContactFirstname], [ContactLastname], [PhoneNumber], [EmailAddress], [Address], [ZipCode], [City], [Country], [WebSite]) VALUES (@CustomerID, @CompanyName, @ContactFirstname, @ContactLastname, @PhoneNumber, @EmailAddress, @Address, @ZipCode, @City, @Country, @WebSite)" 
	ProviderName="<%$ ConnectionStrings:WebPartsTest.ProviderName %>" 
	SelectCommand="SELECT [CustomerID], [CompanyName], [ContactFirstname], [ContactLastname], [PhoneNumber], [EmailAddress], [Address], [ZipCode], [City], [Country], [WebSite] FROM [Customer]" 
	UpdateCommand="UPDATE [Customer] SET [CompanyName] = @CompanyName, [ContactFirstname] = @ContactFirstname, [ContactLastname] = @ContactLastname, [PhoneNumber] = @PhoneNumber, [EmailAddress] = @EmailAddress, [Address] = @Address, [ZipCode] = @ZipCode, [City] = @City, [Country] = @Country, [WebSite] = @WebSite WHERE [CustomerID] = @CustomerID">
	<DeleteParameters>
		<asp:Parameter Name="CustomerID" Type="String" />
	</DeleteParameters>
	<InsertParameters>
		<asp:Parameter Name="CustomerID" Type="String" />
		<asp:Parameter Name="CompanyName" Type="String" />
		<asp:Parameter Name="ContactFirstname" Type="String" />
		<asp:Parameter Name="ContactLastname" Type="String" />
		<asp:Parameter Name="PhoneNumber" Type="String" />
		<asp:Parameter Name="EmailAddress" Type="String" />
		<asp:Parameter Name="Address" Type="String" />
		<asp:Parameter Name="ZipCode" Type="String" />
		<asp:Parameter Name="City" Type="String" />
		<asp:Parameter Name="Country" Type="String" />
		<asp:Parameter Name="WebSite" Type="String" />
	</InsertParameters>
	<UpdateParameters>
		<asp:Parameter Name="CompanyName" Type="String" />
		<asp:Parameter Name="ContactFirstname" Type="String" />
		<asp:Parameter Name="ContactLastname" Type="String" />
		<asp:Parameter Name="PhoneNumber" Type="String" />
		<asp:Parameter Name="EmailAddress" Type="String" />
		<asp:Parameter Name="Address" Type="String" />
		<asp:Parameter Name="ZipCode" Type="String" />
		<asp:Parameter Name="City" Type="String" />
		<asp:Parameter Name="Country" Type="String" />
		<asp:Parameter Name="WebSite" Type="String" />
		<asp:Parameter Name="CustomerID" Type="String" />
	</UpdateParameters>
</asp:SqlDataSource>

