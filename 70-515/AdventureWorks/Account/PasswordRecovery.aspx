<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="PasswordRecovery.aspx.cs" Inherits="AdventureWorks.Account.PasswordRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		Password recovery
	</h2>
	<p>
		Please follow the steps to recover your password.
		<asp:PasswordRecovery ID="pwdRecovery" runat="server" RenderOuterTable="false" 
			EnableViewState="false" onsendingmail="pwdRecovery_SendingMail">
			<MailDefinition From="admin@adventureworks.net" Subject="Your new password" />
			<UserNameTemplate>
				<span class="failureNotification">
					<asp:Literal ID="FailureText" runat="server"></asp:Literal>
				</span>
				<asp:ValidationSummary ID="UserNameValidationSummary" runat="server" CssClass="failureNotification"
					ValidationGroup="UserNameValidationGroup" />
				<div class="accountInfo">
					<fieldset class="login">
						<legend>Account Information</legend>
						<p>
							<asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" AccessKey="U">Username:</asp:Label>
							<asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
							<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
								CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
								ValidationGroup="UserNameValidationGroup">*</asp:RequiredFieldValidator>
						</p>
					</fieldset>
					<p class="submitButton">
						<asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Start Recovery"
							ValidationGroup="UserNameValidationGroup" />
					</p>
				</div>
			</UserNameTemplate>
			<QuestionTemplate>
				<span class="failureNotification">
					<asp:Literal ID="FailureText" runat="server"></asp:Literal>
				</span>
				<asp:ValidationSummary ID="QuestionValidationSummary" runat="server" CssClass="failureNotification"
					ValidationGroup="QuestionValidationGroup" />
				<div class="accountInfo">
					<fieldset class="login">
						<legend>Account Information</legend>
						<p>
							<asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
							<asp:Label ID="UserName" runat="server"></asp:Label>
						</p>
						<p>
							<asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Question:</asp:Label>
							<asp:Label ID="Question" runat="server"></asp:Label>
						</p>
						<p>
							<asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" AccessKey="A">Answer:</asp:Label>
							<asp:TextBox ID="Answer" runat="server" CssClass="textEntry"></asp:TextBox>
							<asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
								CssClass="failureNotification" ErrorMessage="Answer is required." ToolTip="Answer is required."
								ValidationGroup="AnswerValidationGroup">*</asp:RequiredFieldValidator>
						</p>
					</fieldset>
					<p class="submitButton">
						<asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Continue" ValidationGroup="AnswerValidationGroup" />
					</p>
				</div>
			</QuestionTemplate>
			<SuccessTemplate>
				<p>New password has been sent out to: <asp:Label ID="DestinationEmail" runat="server"></asp:Label></p>
			</SuccessTemplate>
		</asp:PasswordRecovery>
	</p>
</asp:Content>
