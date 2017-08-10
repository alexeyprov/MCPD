<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ControlList.aspx.cs" Inherits="ControlList"
	Title="Controls Tree" MasterPageFile="~/Master/Default.master" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<b runat="server">Please enter your password</b>
	<input id="txtPassword" type="password" runat="server" />
	<br />
	<span>File name:</span>
	<asp:FileUpload ID="txtFileName" runat="server" />
	&nbsp;
	<asp:Button ID="btnCountLines" runat="server" Text="Count Lines" OnClick="btnCountLines_Click" />
	<br />
	<span>Select a state:</span>
	<asp:ListBox ID="lstStates" runat="server">
		<asp:ListItem Value="AA">Alaska</asp:ListItem>
		<asp:ListItem Value="AL">Alabama</asp:ListItem>
	</asp:ListBox>
	<br />
	<asp:Label ID="lblOutput" runat="server"></asp:Label>
	<br />
	<asp:PlaceHolder ID="phDynaButtons" runat="server">
		<asp:Button ID="btnClear" runat="server" Text="Reset Output" OnClick="btnClear_Click" />
		<asp:Button ID="btnCreate" runat="server" Text="Create Dynamic Button" OnClick="btnCreate_Click" />
		<asp:Button ID="btnRemove" runat="server" Text="Remove Dynamic Button" Visible="false"
			OnClick="btnRemove_Click" />
	</asp:PlaceHolder>
</asp:Content>
