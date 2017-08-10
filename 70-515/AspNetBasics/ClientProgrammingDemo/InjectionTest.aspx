<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true"
	CodeFile="InjectionTest.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.InjectionTestPage" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<span style="margin-right: 5px">
		Enter your name:
	</span>
	<asp:TextBox ID="txtInput" runat="server" />
	<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_OnClick" Text="Sign up" />
	<br />
	<asp:Label ID="lblOutput" runat="server" />
</asp:Content>

