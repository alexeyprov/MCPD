<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SessionStateTest.aspx.cs"
	Inherits="SessionStateTest" MasterPageFile="~/Master/Default.master" Title="Session State Test Page" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<asp:Button ID="btnAddObject" runat="server" Text="Add Nonserializable Object" OnClick="btnAddObject_Click" />
	<br />
	<asp:LinkButton ID="lnkSignOut" runat="server" Text="Sign Out" OnClick="lnkSignOut_Click" />
</asp:Content>
