<%@ control language="C#" autoeventwireup="true" inherits="AddressControl, AspNetBasics.Deploy" %>
<style type="text/css">
	.style1
	{
		width: 100%;
	}
	.style2
	{
		width: 146px;
	}
</style>
<table class="style1">
	<tr>
		<td class="style2">
			Street address 1:</td>
		<td>
			<asp:TextBox ID="txtAddressOne" runat="server" MaxLength="200" Width="297px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="style2">
			Street address 2:</td>
		<td>
			<asp:TextBox ID="txtAddressTwo" runat="server" MaxLength="200" Width="297px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="style2">
			City:</td>
		<td>
			<asp:TextBox ID="txtCity" runat="server" MaxLength="80" Width="297px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="style2">
			State:</td>
		<td>
			<asp:DropDownList ID="cmbState" runat="server">
				<asp:ListItem Value="AB">Alabama</asp:ListItem>
				<asp:ListItem Value="AL">Alaska</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td class="style2">
			Zip Code:</td>
		<td>
			<asp:TextBox ID="txtZipCode" runat="server" MaxLength="10"></asp:TextBox>
			<asp:Button ID="btnGetCityAndState" runat="server" 
				onclick="btnGetCityAndState_Click" Text="Get City/State" />
		</td>
	</tr>
</table>
