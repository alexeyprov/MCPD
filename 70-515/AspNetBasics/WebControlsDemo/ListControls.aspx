<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListControls.aspx.cs" Inherits="WebControlsDemo_ListControls"
	Title="List Controls Demo" MasterPageFile="~/Master/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<h2>
		Data Binding - List Controls</h2>
	<div>
		<asp:ListBox ID="lstMenu" runat="server" DataTextField="Value" DataValueField="Key"
			SelectionMode="Multiple"></asp:ListBox>
		&nbsp;
		<asp:DropDownList ID="cmbMenu" runat="server" DataTextField="Value" DataValueField="Key">
		</asp:DropDownList>
		&nbsp;
		<asp:CheckBoxList ID="cblMenu" runat="server" DataTextField="Value" DataValueField="Key">
		</asp:CheckBoxList>
		&nbsp;
		<asp:RadioButtonList ID="rblMenu" runat="server" DataTextField="Value" DataValueField="Key">
		</asp:RadioButtonList>
		<br />
		<asp:Button ID="btnGetSelection" runat="server" Text="Get Selection" OnClick="btnGetSelection_Click" />
		<hr />
		<asp:Label ID="lblSelection" runat="server"></asp:Label>
	</div>
</asp:Content>
