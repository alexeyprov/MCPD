<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="CustomControlTest.aspx.cs" Inherits="CustomControlTest" %>
<%@ Register Assembly="ControlExtensions" Namespace="ControlExtensions" TagPrefix="ce" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<ce:NumericUpDown Value="7" runat="server" ID="udTest" 
		ondecremented="udTest_Decremented" onincremented="udTest_Incremented" 
		onvaluechanged="udTest_ValueChanged"></ce:NumericUpDown>
	<br />
	<br />
	<asp:Button ID="btnSubmit" runat="server" Text="Submit" />
</asp:Content>

