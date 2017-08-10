<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employees.aspx.cs" Inherits="Northwind_Employees" %>
<%@ Import Namespace="System.Drawing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Employees</title>
</head>
<body>
	<form id="form1" runat="server">
	<h1>Employees</h1>
	<p>This page demonstrates usage of GridView with template fields and data binding 
		expressions. The child grid also shows an example of row commands and in-place 
		edit processing.
	</p>
	<p>Both parent and child SqlDataSource's use caching. Besides, parent data source use conflict detection mechanism,
		and child data source uses parameter-based filtering.
	</p>
	<div>
		<asp:SqlDataSource ID="srcEmployees" runat="server" ConnectionString="<%$ ConnectionStrings: Northwind %>"
			ProviderName="System.Data.SqlClient" ConflictDetection="CompareAllValues" 
			OldValuesParameterFormatString="ORIGINAL_{0}" EnableCaching="true"
			SelectCommand="
SELECT EMPLOYEEID,
       FIRSTNAME,
       LASTNAME,
       BIRTHDATE,
       TITLE,
       CITY,
       COUNTRY,
       NOTES
  FROM DBO.EMPLOYEES"
			UpdateCommand="
UPDATE EMPLOYEES
   SET FIRSTNAME = @FIRSTNAME,
       LASTNAME = @LASTNAME,
       BIRTHDATE = @BIRTHDATE,
       TITLE = @TITLE,
       CITY = @CITY,
       COUNTRY = @COUNTRY,
       NOTES = @NOTES
 WHERE EMPLOYEEID = @ORIGINAL_EMPLOYEEID
   AND FIRSTNAME = @ORIGINAL_FIRSTNAME
   AND LASTNAME = @ORIGINAL_LASTNAME
   AND BIRTHDATE = @ORIGINAL_BIRTHDATE
   AND TITLE = @ORIGINAL_TITLE
   AND CITY = @ORIGINAL_CITY
   AND COUNTRY = @ORIGINAL_COUNTRY
   AND CAST(NOTES AS NVARCHAR(4000)) = @ORIGINAL_NOTES" />

		<asp:SqlDataSource ID="srcOrders" runat="server" ConnectionString="<%$ ConnectionStrings:Northwind %>"
			ProviderName="System.Data.SqlClient" EnableCaching="true"
			FilterExpression="EMPLOYEEID = {0}"
			SelectCommand="
SELECT O.ORDERID, 
       O.CUSTOMERID,
       C.COMPANYNAME,
       O.ORDERDATE,
       O.REQUIREDDATE,
       O.SHIPPEDDATE,
       O.FREIGHT,
       O.SHIPVIA,
       S.COMPANYNAME AS SHIPPERNAME,
       O.EMPLOYEEID
  FROM DBO.ORDERS O 
 INNER JOIN CUSTOMERS C ON C.CUSTOMERID = O.CUSTOMERID
  LEFT JOIN SHIPPERS S ON S.SHIPPERID = O.SHIPVIA"
			UpdateCommand="
UPDATE ORDERS
   SET FREIGHT = @FREIGHT,
       ORDERDATE = @ORDERDATE,
       REQUIREDDATE = @REQUIREDDATE,
       SHIPPEDDATE = @SHIPPEDDATE,
       SHIPVIA = @SHIPVIA
 WHERE ORDERID = @ORDERID">
			<FilterParameters>
				<asp:ControlParameter ControlID="grdEmployees" Name="EMPLOYEE_ID" 
					PropertyName="SelectedValue" />
			</FilterParameters>
			<UpdateParameters>
				<asp:Parameter Name="FREIGHT" />
				<asp:Parameter Name="ORDERDATE" />
				<asp:Parameter Name="REQUIREDDATE" />
				<asp:Parameter Name="SHIPPEDDATE" />
				<asp:Parameter Name="ORDERID" />
				<asp:Parameter Name="SHIPVIA" />
			</UpdateParameters>
		</asp:SqlDataSource>
		
		<asp:SqlDataSource ID="srcShippers" runat="server" ConnectionString="<%$ ConnectionStrings:Northwind %>"
			ProviderName="System.Data.SqlClient" SelectCommand="
		SELECT SHIPPERID,
		       COMPANYNAME
		  FROM SHIPPERS" />
		
		<b>Select an employee</b>
		<br />
		<asp:GridView ID="grdEmployees" runat="server" DataSourceID="srcEmployees" 
			AutoGenerateColumns="False" DataKeyNames="EMPLOYEEID"
			Font-Names="Verdana" Font-Size="X-Small"
			AllowSorting="True"
			OnRowDataBound="grdEmployees_RowDataBound" 
			OnSelectedIndexChanged="grdEmployees_SelectedIndexChanged" 
			OnSorted="grdEmployees_Sorted"
			OnRowEditing="grdEmployees_RowEditing"
			OnRowUpdated="grdEmployees_RowUpdated">
			<Columns>
				<asp:ButtonField DataTextField="EMPLOYEEID" HeaderText="ID" InsertVisible="False" 
					SortExpression="EMPLOYEEID" CommandName="Select" >
					<ItemStyle BorderWidth="1px" Font-Bold="True" />
				</asp:ButtonField>
				<asp:BoundField DataField="FIRSTNAME" HeaderText="First Name" 
					SortExpression="FIRSTNAME" />
				<asp:BoundField DataField="LASTNAME" HeaderText="Last Name" 
					SortExpression="LASTNAME" />
				<asp:BoundField DataField="TITLE" HeaderText="Title" />
				<asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY" >
				<ItemStyle BackColor="LightSteelBlue" />
				</asp:BoundField>
				<asp:BoundField DataField="COUNTRY" HeaderText="Country" 
					SortExpression="COUNTRY">
				<ItemStyle BackColor="LightSteelBlue" />
				</asp:BoundField>
				<asp:BoundField DataField="BIRTHDATE" DataFormatString="{0:d}" 
					HeaderText="Birthday" SortExpression="BIRTHDATE" />
				<asp:TemplateField HeaderText="Age">
					<ItemTemplate>
						<div><%# CalculateAge(Container.DataItem) %></div>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
						<img src='EmployeePhoto.ashx?ID=<%# Eval("EMPLOYEEID") %>' alt="Employee Photo"/>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:BoundField DataField="NOTES" HeaderText="Notes" >
					<ItemStyle Width="400px" />
				</asp:BoundField>
				<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowCancelButton="true" />
			</Columns>
		</asp:GridView>
		
		<asp:Panel ID="pnlError" runat="server" Visible="false" EnableViewState="false">
			There is a newer version of this record in the database.<br />
			The current record has the values shown below.<br />
			<br />
			<asp:DetailsView ID="dvwConflictingEmployee" runat="server" AutoGenerateRows="False"
				DataSourceID="srcConflictingEmployee" >
				<Fields>
					<asp:BoundField DataField="FIRSTNAME" HeaderText="First Name" />
					<asp:BoundField DataField="LASTNAME" HeaderText="Last Name" />
					<asp:BoundField DataField="TITLE" HeaderText="Title" />
					<asp:BoundField DataField="CITY" HeaderText="City" />
					<asp:BoundField DataField="COUNTRY" HeaderText="Country" />
					<asp:BoundField DataField="BIRTHDATE" HeaderText="Birth Date" />
					<asp:BoundField DataField="NOTES" HeaderText="Notes" />
				</Fields>
			</asp:DetailsView>
			<br />
			<span>
				* Click <b>Update</b> to override these values with your changes.
				<br />
				* Click <b>Cancel</b> to abandon your edit.
			</span>
			&nbsp;
			<asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:Northwind %>" ID="srcConflictingEmployee"
				runat="server" 
				SelectCommand="
SELECT EMPLOYEEID,
       FIRSTNAME,
       LASTNAME,
       BIRTHDATE,
       TITLE,
       CITY,
       COUNTRY,
       NOTES
  FROM EMPLOYEES
 WHERE EMPLOYEEID = @EMPLOYEEID"
				OnSelecting="srcConflictingEmployee_Selecting">
				<SelectParameters>
					<asp:SessionParameter Name="EMPLOYEEID" Type="Int32" 
					 SessionField="EDITED_EMPLOYEE_ID" />
				</SelectParameters>
			</asp:SqlDataSource>
		</asp:Panel>
		
		<asp:Panel runat="server" ID="pnlOrders" Visible="false">
			<br />
			<asp:Label runat="server" ID="lblEmployeeOrders" Font-Bold="true" />
			<br />
			<asp:GridView ID="grdOrders" runat="server" AutoGenerateColumns="False" 
				Font-Names="Verdana" Font-Size="X-Small"
				DataKeyNames="ORDERID" DataSourceID="srcOrders" AllowPaging="True" 
				PageSize="20" AutoGenerateEditButton="True"
				onrowcommand="grdOrders_RowCommand">
				<Columns>
					<asp:BoundField DataField="ORDERID" HeaderText="ID" InsertVisible="False" 
						ReadOnly="True" SortExpression="ORDERID" />
					<asp:TemplateField HeaderText="Company" 
						SortExpression="COMPANYNAME" >
						<ItemTemplate>
							<asp:LinkButton Text='<%# Eval("COMPANYNAME") %>' runat="server"
								 CommandName="ShowCompany" CommandArgument='<%# Eval("CUSTOMERID") %>'/>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:BoundField DataField="FREIGHT" HeaderText="Freight" 
						SortExpression="FREIGHT" />
					<asp:TemplateField HeaderText="History">
						<EditItemTemplate>
							<table class="style1">
								<tr>
									<td>
										Ordered on:</td>
									<td>
										<asp:Calendar ID="dtOrderedDate" runat="server" BackColor="White" 
											BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
											Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
											SelectedDate='<%# Bind("ORDERDATE") %>' VisibleDate='<%# Eval("ORDERDATE") %>' 
											Width="200px">
											<SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
											<SelectorStyle BackColor="#CCCCCC" />
											<WeekendDayStyle BackColor="#FFFFCC" />
											<TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
											<OtherMonthDayStyle ForeColor="#808080" />
											<NextPrevStyle VerticalAlign="Bottom" />
											<DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
											<TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
										</asp:Calendar>
									</td>
								</tr>
								<tr>
									<td>
										Shipped on:</td>
									<td>
										<asp:Calendar ID="dtShippedDate" runat="server" BackColor="White" 
											BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
											Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
											SelectedDate='<%# Bind("SHIPPEDDATE") %>' 
											VisibleDate='<%# Eval("SHIPPEDDATE") %>' Width="200px">
											<SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
											<SelectorStyle BackColor="#CCCCCC" />
											<WeekendDayStyle BackColor="#FFFFCC" />
											<TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
											<OtherMonthDayStyle ForeColor="#808080" />
											<NextPrevStyle VerticalAlign="Bottom" />
											<DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
											<TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
										</asp:Calendar>
									</td>
								</tr>
								<tr>
									<td>
										Required on:</td>
									<td>
										<asp:Calendar ID="dtOrderedDate1" runat="server" BackColor="White" 
											BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
											Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
											SelectedDate='<%# Bind("REQUIREDDATE") %>' 
											VisibleDate='<%# Eval("REQUIREDDATE") %>' Width="200px">
											<SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
											<SelectorStyle BackColor="#CCCCCC" />
											<WeekendDayStyle BackColor="#FFFFCC" />
											<TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
											<OtherMonthDayStyle ForeColor="#808080" />
											<NextPrevStyle VerticalAlign="Bottom" />
											<DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
											<TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
										</asp:Calendar>
									</td>
								</tr>
							</table>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:Label ID="lblOrderedDate" runat="server" Text='<%# Eval("ORDERDATE", "{0:d}") %>'></asp:Label>
							-&gt;<asp:Label ID="lblShippedDate" runat="server" Text='<%# Eval("SHIPPEDDATE", "{0:d}") %>'></asp:Label>
							-&gt;<asp:Label ID="lblRequiredDate" runat="server" Text='<%# Eval("REQUIREDDATE", "{0:d}") %>'
								ForeColor='<%# GetRequiredDateColor(Eval("SHIPPEDDATE"), Eval("REQUIREDDATE")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Shipped Via">
						<EditItemTemplate>
							<asp:DropDownList ID="cmbShipper" runat="server" DataSourceID="srcShippers" 
								DataTextField="COMPANYNAME" DataValueField="SHIPPERID" 
								SelectedValue='<%# Bind("SHIPVIA") %>'>
							</asp:DropDownList>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:Label ID="lblShipper" runat="server" Text='<%# Eval("SHIPPERNAME") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
			
			<asp:Label ID="lblSelectedCompany" runat="server" Visible="False"></asp:Label>
		</asp:Panel>
		
	</div>

	</form>
</body>
</html>
