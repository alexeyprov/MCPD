<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" 
	CodeFile="DynamicPanelTest.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.DynamicPanelTestPage" %>
<%@ Register Assembly="ControlExtensions" Namespace="ControlExtensions" TagPrefix="ce" %>

<asp:Content ID="cnt" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<table width="300px">
		<tr>
			<td>
				<img src="../Images/Hourglass.gif" alt="Hourglass"/>
			</td>
			<td>
				<span>
					Since call is done asynchronously, the animated image never stops
				</span>
			</td>
		</tr>
	</table>
	<br />
	<ce:DynamicPanel ID="dynaPanel" runat="server" onrefresh="dynaPanel_Refresh">
		<span>
			First value:&nbsp;
		</span>
		<asp:TextBox runat="server" id="op1" maxlength="10" value="0" />
		<br />
		<span>
			Second value:&nbsp;
		</span>
		<asp:TextBox runat="server" id="op2" maxlength="10" value="0" />
		<br />
		<br />
		<asp:Label ID="lbl" runat="server" />
	</ce:DynamicPanel>

	<br />
	<br />

	<ce:CallbackPanelRefreshLink PanelID="dynaPanel" runat="server" Text="Refresh"/>

</asp:Content>

