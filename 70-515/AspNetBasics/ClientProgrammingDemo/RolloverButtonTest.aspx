<%@ Page Title="Rollover Button" Language="C#" MasterPageFile="~/Master/Default.master"
	AutoEventWireup="true" CodeFile="RolloverButtonTest.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.RolloverButtonTestPage" %>
<%@ Register Assembly="ControlExtensions" Namespace="ControlExtensions" TagPrefix="ce" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<ce:RolloverButton runat="server" DefaultImageUrl="~/Images/ButtonNormal.gif" 
		HighlightedImageUrl="~/Images/ButtonHovered.gif" />
</asp:Content>

