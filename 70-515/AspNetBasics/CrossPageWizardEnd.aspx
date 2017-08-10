<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true"
	CodeFile="CrossPageWizardEnd.aspx.cs" Inherits="CrossPageWizardEnd" Title="Cross Page Wizard - Step 2" %>
<%@ PreviousPageType VirtualPath="~/CrossPageWizardStart.aspx" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<h2>Summary</h2>
	<p>Congratulations! You have completed the Cross Page Wizard. Please review the collected information below</p>
	<asp:TextBox ID="txtSummary" ReadOnly="true" TextMode="MultiLine" Rows="4" runat="server" />
	<br />
	You've been redirected to this page via <asp:Label ID="lblRedirectMethod" runat="server"/>
	<br />
	<a href="CrossPageWizardStart.aspx">Back</a>
</asp:Content>

