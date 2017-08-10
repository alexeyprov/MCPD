<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Title="Personal Information" Inherits="AdventureWorks.Account.PersonalInfo" MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
		.style1
		{
			width: 100%;
		}
		.style2
		{
			width: 220px;
		}
	</style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<table class="style1">
			<tr>
				<td width="150px">
					First name:</td>
				<td>
					<asp:TextBox ID="txtFirstName" runat="server" MaxLength="32" Width="200px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					Last name:</td>
				<td>
					<asp:TextBox ID="txtLastName" runat="server" MaxLength="32" Width="200px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					Date of birth:</td>
				<td>
					<asp:Calendar ID="ddlDateOfBirth" runat="server" BackColor="White" 
						BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
						Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
						ondayrender="ddlDateOfBirth_DayRender" Width="200px">
						<DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
						<NextPrevStyle VerticalAlign="Bottom" />
						<OtherMonthDayStyle ForeColor="#808080" />
						<SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
						<SelectorStyle BackColor="#CCCCCC" />
						<TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
						<TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
						<WeekendDayStyle BackColor="#FFFFCC" />
					</asp:Calendar>
				</td>
			</tr>
		</table>
    
    </div>
    <table cellspacing="20" class="style2">
		<tr>
			<td>
				<asp:Button ID="cmdUpdate" runat="server" onclick="cmdUpdate_Click" 
					Text="Update" Width="100px" />
			</td>
			<td>
				<asp:Button ID="cmdCancel" runat="server" Text="Cancel" Width="100px" />
			</td>
			<td>
				<asp:Button ID="cmdReset" runat="server" Text="Reset" Width="100px" 
					onclick="cmdReset_Click" />
			</td>
		</tr>
	</table>
</asp:Content>