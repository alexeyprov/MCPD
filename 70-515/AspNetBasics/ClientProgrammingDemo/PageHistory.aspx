<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true"
	CodeFile="PageHistory.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.PageHistoryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<asp:ScriptManager ID="smAjax" runat="server" EnableHistory="true" 
		onnavigate="smAjax_Navigate" />
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<asp:Wizard ID="wizSimple" runat="server" ActiveStepIndex="0" 
				BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderWidth="1px" 
				Font-Names="Verdana" Font-Size="0.8em" 
				onactivestepchanged="wizSimple_ActiveStepChanged">
				<FinishNavigationTemplate>
					<asp:Button ID="FinishPreviousButton" runat="server" BackColor="White" 
						BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" 
						CausesValidation="False" CommandName="MovePrevious" Font-Names="Verdana" 
						Font-Size="0.8em" ForeColor="#284E98" Text="Previous" />
					<asp:Button ID="FinishButton" runat="server" BackColor="White" 
						BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" 
						CommandName="MoveComplete" Font-Names="Verdana" Font-Size="0.8em" 
						ForeColor="#284E98" Text="Finish" />
				</FinishNavigationTemplate>
				<HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" 
					BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" 
					HorizontalAlign="Center" />
				<NavigationButtonStyle BackColor="White" BorderColor="#507CD1" 
					BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
					ForeColor="#284E98" />
				<SideBarButtonStyle BackColor="#507CD1" Font-Names="Verdana" 
					ForeColor="White" />
				<SideBarStyle BackColor="#507CD1" Font-Size="0.9em" VerticalAlign="Top" />
				<StartNavigationTemplate>
					<asp:Button ID="StartNextButton" runat="server" BackColor="White" 
						BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" 
						CommandName="MoveNext" Font-Names="Verdana" Font-Size="0.8em" 
						ForeColor="#284E98" Text="Next" />
				</StartNavigationTemplate>
				<StepStyle Font-Size="0.8em" ForeColor="#333333" />
				<WizardSteps>
					<asp:WizardStep runat="server" StepType="Start" title="Step 1">
						This is step #1
					</asp:WizardStep>
					<asp:WizardStep runat="server" title="Step 2">
						This is step #2
					</asp:WizardStep>
					<asp:WizardStep runat="server" StepType="Finish" Title="Step 3">
						This is step #3
					</asp:WizardStep>
				</WizardSteps>
			</asp:Wizard>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>

