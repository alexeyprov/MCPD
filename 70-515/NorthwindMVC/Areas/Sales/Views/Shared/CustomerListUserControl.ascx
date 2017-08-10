<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Northwind.Data.Entities.Customer>>" %>
<script runat="server">
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			grdCustomers.DataSource = Model;
			grdCustomers.DataBind();
		}
	}
	
	protected void grdCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		//TODO: customize
	}
</script>

<form runat="server">
	<asp:GridView ID="grdCustomers" runat="server" AutoGenerateColumns="False" 
		onrowdatabound="grdCustomers_RowDataBound">
		<Columns>
			<asp:BoundField HeaderText="Company" DataField="CompanyName" />
			<asp:BoundField HeaderText="Country" DataField="Country" />
			<asp:TemplateField>
				<ItemTemplate>
					<a href="/Customer/Delete/<%# Eval("CustomerID")%>" 
						onclick="confirm('Are you sure you want to delete this customer?')">
						<img style="border:0px" alt="Delete customer" src="../../Content/images/DeleteHS.png" />
					</a>
					<img style="border:0px" alt="Edit customer" src="../../Content/images/EditTask.png" 
						onclick='OnEditCustomer("<%# Eval("CustomerID")%>", "<%# Eval("CompanyName")%>", "<%# Eval("ContactName")%>", "<%# Eval("Address")%>", "<%# Eval("City")%>", "<%# Eval("Country")%>")' />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
		<HeaderStyle BackColor="#9999FF" />
	</asp:GridView>
</form>
