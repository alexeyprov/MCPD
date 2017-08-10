<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="UserControlTest.aspx.cs" Inherits="UserControlTestPage" %>

<%@ Register src="UserControls/LinkTable.ascx" tagname="LinkTable" tagprefix="uc1" %>

<%@ Reference Control="~/UserControls/AddressControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	
	<uc1:LinkTable ID="lnkTable" runat="server" Caption="Useful Links" OnClick="lnkTable_Click"/>
	<br />
	<asp:PlaceHolder ID="phAddress" runat="server" />
	
</asp:Content>

