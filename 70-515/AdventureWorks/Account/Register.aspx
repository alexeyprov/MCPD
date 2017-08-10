<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeBehind="Register.aspx.cs" Inherits="AdventureWorks.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" RequireEmail="true"
		OnCreatedUser="RegisterUser_CreatedUser">
		<LayoutTemplate>
			<asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
			<asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
		</LayoutTemplate>
		<WizardSteps>
			<asp:WizardStep ID="NameStep" runat="server">
				<h2>
					Personal Info
				</h2>
				<p>
					Welcome to the registration form. Please provide personal info.
				</p>
				<asp:ValidationSummary ID="PersonalInfoValidationSummary" runat="server" CssClass="failureNotification"
					ValidationGroup="PersonalInfoValidationGroup" />
				<div class="accountInfo">
					<fieldset class="register">
						<legend>Personal Information</legend>
						<p>
							<asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstName" AccessKey="F">First Name:</asp:Label>
							<asp:TextBox ID="FirstName" runat="server" CssClass="textEntry"></asp:TextBox>
							<asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstName"
								CssClass="failureNotification" ErrorMessage="First Name is required." ToolTip="First Name is required."
								ValidationGroup="PersonalInfoValidationGroup">*</asp:RequiredFieldValidator>
						</p>
						<p>
							<asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastName" AccessKey="L">Last Name:</asp:Label>
							<asp:TextBox ID="LastName" runat="server" CssClass="textEntry"></asp:TextBox>
							<asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="LastName"
								CssClass="failureNotification" ErrorMessage="Last Name is required." ToolTip="Last Name is required."
								ValidationGroup="PersonalInfoValidationGroup">*</asp:RequiredFieldValidator>
						</p>
					</fieldset>
				</div>
			</asp:WizardStep>
			<asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
				<ContentTemplate>
					<h2>
						Create a New Account
					</h2>
					<p>
						Use the form below to create a new account.
					</p>
					<p>
						Passwords are required to be a minimum of
						<%= Membership.MinRequiredPasswordLength %>
						characters in length.
					</p>
					<span class="failureNotification">
						<asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
					</span>
					<asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
						ValidationGroup="RegisterUserValidationGroup" />
					<div class="accountInfo">
						<fieldset class="register">
							<legend>Account Information</legend>
							<p>
								<asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
								<asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
								<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
									CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
									ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
							</p>
							<p>
								<asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
								<asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
								<asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
									CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
									ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
							</p>
							<p>
								<asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
								<asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
								<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
									CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
									ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
							</p>
							<p>
								<asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
								<asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
								<asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
									Display="Dynamic" ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired"
									runat="server" ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
								<asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
									ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic"
									ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
							</p>
							<p>
								<asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question" AccessKey="Q">Securty Question:</asp:Label>
								<asp:TextBox ID="Question" runat="server" CssClass="textEntry"></asp:TextBox>
								<asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
									CssClass="failureNotification" ErrorMessage="Question is required." ToolTip="Question is required."
									ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
							</p>
							<p>
								<asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" AccessKey="A">Answer:</asp:Label>
								<asp:TextBox ID="Answer" runat="server" CssClass="textEntry"></asp:TextBox>
								<asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
									CssClass="failureNotification" ErrorMessage="Answer is required." ToolTip="Answer is required."
									ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
							</p>
						</fieldset>
						<p class="submitButton">
							<asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Create User"
								ValidationGroup="RegisterUserValidationGroup" />
						</p>
					</div>
				</ContentTemplate>
				<CustomNavigationTemplate>
				</CustomNavigationTemplate>
			</asp:CreateUserWizardStep>
		</WizardSteps>
	</asp:CreateUserWizard>
</asp:Content>
