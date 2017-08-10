<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsyncPersonalInfo.aspx.cs" Title="Personal Information" Inherits="AdventureWorks.Account.AsyncPersonalInfo" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:ScriptManager runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/Scripts/ProfileManagement.js" />
		</Scripts>
	</asp:ScriptManager>
	<div>
		<table class="style1">
			<tr>
				<td width="150px">
					First name:</td>
				<td>
					<asp:TextBox ID="txtFirstName" runat="server" MaxLength="32" Width="200px" ClientIDMode="Static"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					Last name:</td>
				<td>
					<asp:TextBox ID="txtLastName" runat="server" MaxLength="32" Width="200px" ClientIDMode="Static"></asp:TextBox>
				</td>
			</tr>
		</table>
    </div>
    <table cellspacing="20" class="style2">
		<tr>
			<td>
				<asp:Button ID="cmdUpdate" runat="server" OnClientClick="return OnUpdateClicked();"
					Text="Update" Width="100px" UseSubmitBehavior="false" />
			</td>
			<td>
				<asp:Button ID="cmdReset" runat="server" Text="Reset" Width="100px" 
					OnClientClick="return OnResetClicked();" UseSubmitBehavior="false" />
			</td>
		</tr>
	</table>
</asp:Content>