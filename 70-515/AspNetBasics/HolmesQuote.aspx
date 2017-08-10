<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolmesQuote.aspx.cs" Inherits="HolmesQuote"
	MasterPageFile="~/Master/Default.master" Title="Sherlock Holmes Quote" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<asp:Label ID="lblSource" runat="server" Font-Bold="True" />
	&nbsp;(
	<asp:Label ID="lblDate" runat="server" Font-Italic="true" />
	)<br />
	<blockquote>
		<asp:Label ID="lblQuote" runat="server" />
	</blockquote>
</asp:Content>
