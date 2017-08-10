<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true"
	CodeFile="ThreePanels.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.ThreePanelsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="Server">
	<style type="text/css">
		div.status_div
		{
			background-color: #FFC080;
			top: 95%;
			left: 1%;
			height: 20px;
			width: 270px;
			position: absolute;
			visibility: hidden;
		}
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
	<asp:ScriptManager runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/Scripts/PartialRendering.js" />
		</Scripts>
	</asp:ScriptManager>
	<%-- 
	Default value of UpdateMode=Always means a panel will be updated no matter the control triggered the postback
	UpdateMode=Conditional means a panel will be updated only if it contains the control triggered the postback
	--%>

	<asp:UpdatePanel ID="FirstPanel" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:Label ID="FirstLabel" runat="server" />
			<asp:Button ID="FirstButton" runat="server" Text="Full Postback" />
		</ContentTemplate>
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="FirstOuterButton" EventName="Click" />
			<asp:PostBackTrigger ControlID="FirstButton" />
		</Triggers>
	</asp:UpdatePanel>

	<asp:UpdatePanel ID="SecondPanel" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:Label ID="SecondLabel" runat="server" />
			<asp:Button ID="SecondButton" runat="server" Text="Update" />
		</ContentTemplate>
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="SecondTimer" EventName="Tick" />
		</Triggers>
	</asp:UpdatePanel>

	<asp:UpdatePanel ID="ThirdPanel" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:Label ID="ThirdLabel" runat="server" />
			<asp:Button ID="ThirdButton" runat="server" Text="Update" />
		</ContentTemplate>
	</asp:UpdatePanel>

	<asp:Button ID="FirstOuterButton" runat="server" Text="Update First Panel" />

	<asp:Timer ID="SecondTimer" runat="server" Interval="10000">
	</asp:Timer>

	<div id="StatusDiv" class="status_div">
	</div>

</asp:Content>
