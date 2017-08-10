<%@ Page Title="Classic Wizard" Language="C#" MasterPageFile="~/Master/Default.master"
	AutoEventWireup="true" CodeFile="NormalWizard.aspx.cs" Inherits="WebControlsDemo_NormalWizard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
	<asp:ScriptManager runat="server">
	</asp:ScriptManager>
	<asp:Wizard ID="wizSurvey" runat="server" Width="480px" BorderWidth="1px" 
		ActiveStepIndex="0" BackColor="#EFF3FB" BorderColor="#B5C7DE" 
		onfinishbuttonclick="wizSurvey_FinishButtonClick" Font-Names="Verdana" 
		Font-Size="0.8em">
		<NavigationButtonStyle BackColor="White" BorderColor="#507CD1" 
			BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
			ForeColor="#284E98" />
		<SideBarButtonStyle BackColor="#507CD1" Font-Names="Verdana" 
			ForeColor="White" />
		<SideBarStyle BackColor="#507CD1" Font-Size="0.9em" VerticalAlign="Top" />
		<StepStyle Font-Size="0.8em" ForeColor="#333333" />
		<WizardSteps>
			<asp:WizardStep runat="server" Title="Personal" ID="wstPersonal" StepType="Start">
				<h3>
					Personal Profile</h3>
				<span>Preferred programming language:</span>
				<asp:DropDownList ID="lstLanguage" runat="server">
					<asp:ListItem>C++</asp:ListItem>
					<asp:ListItem>C#</asp:ListItem>
					<asp:ListItem>JavaScript</asp:ListItem>
					<asp:ListItem>J#</asp:ListItem>
					<asp:ListItem>VB</asp:ListItem>
				</asp:DropDownList>
			</asp:WizardStep>
			<asp:WizardStep runat="server" Title="Company" ID="wstCompany" StepType="Step">
				<h3>
					Company Profile</h3>
				<table width="80%">
					<tr>
						<td width="100%">
							Number of Employees
						</td>
						<td width="100px">
							<asp:TextBox ID="txtEmployeeCount" runat="server"></asp:TextBox>
							<ajaxtoolkit:AutoCompleteExtender runat="server" TargetControlID="txtEmployeeCount"
								 ServicePath="NumberAutoCompletionService.svc" ServiceMethod="GetNextChunk" MinimumPrefixLength="1" />
						</td>
					</tr>
					<tr>
						<td width="100%">
							Number of Locations
						</td>
						<td width="100px">
							<asp:TextBox ID="txtLocationCount" runat="server"></asp:TextBox>
							<ajaxtoolkit:AutoCompleteExtender runat="server" TargetControlID="txtLocationCount"
								 ServicePath="NumberAutoCompletionService.svc" ServiceMethod="GetNextChunk" MinimumPrefixLength="1" 
								 CompletionSetCount="15"/>
						</td>
					</tr>
				</table>
			</asp:WizardStep>
			<asp:WizardStep runat="server" Title="Software" ID="wstSoftware" StepType="Step">
				<h3>
					Software Profile</h3>
				<span>Licenses required:</span>
				<asp:CheckBoxList runat="server" ID="cblLicenses">
					<asp:ListItem>Visual Studio 2010</asp:ListItem>
					<asp:ListItem>Office 2010</asp:ListItem>
					<asp:ListItem>SQL Server 2008</asp:ListItem>
					<asp:ListItem>BizTalk Server 2009</asp:ListItem>
				</asp:CheckBoxList>
			</asp:WizardStep>
			<asp:WizardStep runat="server" Title="Complete" ID="wstComplete" StepType="Finish" >
				<p>
					Thank you for completing our survey!<br />Your products will be delivered 
					shortly.
				</p>
				<asp:Label ID="lblSummaryInfo" runat="server"></asp:Label>
			</asp:WizardStep>
		</WizardSteps>
		<HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" 
			BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" 
			HorizontalAlign="Center" />
		<HeaderTemplate>
			<i>Online Survey</i> - <b><%# wizSurvey.ActiveStep.Title %></b>
			<br />
		</HeaderTemplate>
	</asp:Wizard>
</asp:Content>
