<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ValidationGroups.aspx.cs"
	Inherits="WebControlsDemo_ValidationGroups" Title="Validation Groups Demo" MasterPageFile="~/Master/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<asp:Panel ID="pnlLogin" runat="server" DefaultButton="cmdLogIn">
		<span>User name:</span>
		<asp:TextBox ID="txtLogIn" runat="server" ValidationGroup="Existing User">
		</asp:TextBox>
		<asp:RequiredFieldValidator ID="valLogIn" runat="server" ValidationGroup="Existing User"
			ControlToValidate="txtLogIn">*
		</asp:RequiredFieldValidator>
		<br />
		<asp:Button ID="cmdLogIn" runat="server" Text="Log In" ValidationGroup="Existing User" />
	</asp:Panel>
	<hr />
	<asp:Panel ID="pnlNewUser" runat="server" DefaultButton="cmdNewUser">
		<span>Register as:</span>
		<asp:TextBox ID="txtNewUser" runat="server" ValidationGroup="New User">
		</asp:TextBox>
		<asp:RequiredFieldValidator ID="valNewUser" runat="server" ValidationGroup="New User"
			ControlToValidate="txtNewUser">*
		</asp:RequiredFieldValidator>
		<br />
		<asp:Button ID="cmdNewUser" runat="server" Text="Register" ValidationGroup="New User" />
	</asp:Panel>
	<hr />
	<asp:Panel ID="pnlCommon" runat="server" DefaultButton="cmdValidateAll">
		<asp:Button ID="cmdValidateAll" runat="server" Text="Validate All" OnClick="cmdValidateAll_Click" />
		<br />
		<asp:Label ID="lblValidationResult" runat="server"></asp:Label>
	</asp:Panel>
</asp:Content>
