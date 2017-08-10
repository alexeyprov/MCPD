<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AdventureWorks.Admin.UserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="false" DataKeyNames="UserName"
			AllowPaging="true" PageSize="10"
			onselectedindexchanged="grdUsers_SelectedIndexChanged">
			<Columns>
				<asp:CommandField ShowSelectButton="true" SelectText="&gt;" />
				<asp:BoundField DataField="UserName" HeaderText="Login" />
				<asp:BoundField DataField="Email" HeaderText="Email" />
				<asp:BoundField DataField="CreationDate" HeaderText="Created on" />
			</Columns>
		</asp:GridView>
	</div>
	<asp:Panel ID="pnlUserInfo" runat="server" Visible="false">
		Selected User:<br />
		<table border="1" style="border-color: Blue">
			<tr>
				<td>
					User Name:
				</td>
				<td>
					<asp:Label ID="lblUsername" runat="server" />
				</td>
			</tr>
			<tr>
				<td>
					Email:
				</td>
				<td>
					<asp:TextBox ID="txtEmail" runat="server" />
				</td>
			</tr>
			<tr>
				<td>
					Password Question:
				</td>
				<td>
					<asp:Label ID="lblPasswordQuestion" runat="server" />
				</td>
			</tr>
			<tr>
				<td>
					Last Login Date:
				</td>
				<td>
					<asp:Label ID="lblLastLoginOn" runat="server" />
				</td>
			</tr>
			<tr>
				<td>
					Comment:
				</td>
				<td>
					<asp:TextBox ID="txtComment" runat="server" TextMode="multiline" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" />
				</td>
				<td>
					<asp:CheckBox ID="chkIsLockedOut" runat="Server" Text="Locked Out" 
						Enabled="False" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Button ID="btnUpdateUser" runat="server" Text="Update" 
						onclick="btnUpdateUser_Click" />
				</td>
				<td>
					<asp:Button ID="btnCancel" runat="server" Text="Cancel" 
						onclick="btnCancel_Click" />
				</td>
			</tr>
		</table>
	</asp:Panel>
	</form>
</body>
</html>
