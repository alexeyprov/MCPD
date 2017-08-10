<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" 
	CodeFile="CrossPageWizardStart.aspx.cs" Inherits="CrossPageWizardStart" Title="Cross Page Wizard - Step 1" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <h2>
		<asp:Localize runat="server" meta:resourceKey="LocalizeResource1">Personal information</asp:Localize>
	</h2>
    <p>
		<asp:Localize runat="server" meta:resourceKey="LocalizeResource2">Please enter first name and last name, then click Next</asp:Localize>
	</p>
    <table>
		<tr>
			<td align="right">
				<asp:Label runat="server" Text="First name:" 
					AccessKey="F" AssociatedControlID="txtFirstName" meta:resourcekey="LabelResource1" />
			</td>
			<td>
				<asp:TextBox ID="txtFirstName" runat="server" 
					meta:resourcekey="txtFirstNameResource1"/>
				<asp:RequiredFieldValidator ID="valFirstName" runat="server" 
					ControlToValidate="txtFirstName" ErrorMessage="*" 
					meta:resourcekey="valFirstNameResource1"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td align="right">
				<asp:Label runat="server" Text="Last name:" 
					AccessKey="L" AssociatedControlID="txtLastName" meta:resourcekey="LabelResource2" />
			</td>
			<td>
				<asp:TextBox ID="txtLastName" runat="server" 
					meta:resourcekey="txtLastNameResource1"/>
				<asp:RequiredFieldValidator ID="valLastName" runat="server" 
					ControlToValidate="txtLastName" EnableClientScript="False" ErrorMessage="*" 
					meta:resourcekey="valLastNameResource1"></asp:RequiredFieldValidator>
			</td>
		</tr>
    </table>
    <asp:LinkButton ID="lnkNext" PostBackUrl="~/CrossPageWizardEnd.aspx" 
		runat="server" meta:resourcekey="lnkNextResource1">Next</asp:LinkButton>
    &nbsp;
    <asp:Button ID="btnNext" runat="server" Text="Next (via Transfer)" 
			onclick="btnNext_Click" meta:resourcekey="btnNextResource1" />
</asp:Content>