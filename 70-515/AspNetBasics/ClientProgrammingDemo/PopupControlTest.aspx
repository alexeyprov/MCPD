<%@ Page Title="Popup Control" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true"
	 CodeFile="PopupControlTest.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.PopupControlTestPage" %>
<%@ Register Assembly="ControlExtensions" Namespace="ControlExtensions" TagPrefix="ce" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<ce:PopUp ID="puTest" runat="server" Url="http://www.epam-group.ru" WindowHeight="300" WindowWidth="400" 
		Resizable="true" ScrollBars="true" PopUnder="true">
	</ce:PopUp>
</asp:Content>

