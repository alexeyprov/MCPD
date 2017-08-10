<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageLifeCycle.aspx.cs" Inherits="PageLifeCycle"
	Trace="true" TraceMode="SortByCategory" MasterPageFile="~/Master/Default.master"
	Title="Page Flow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<asp:Label ID="lblInfo" runat="server" EnableViewState="False"></asp:Label>
	<asp:Button ID="btnAction" runat="server" OnClick="btnAction_Click" Text="Button" />
</asp:Content>
