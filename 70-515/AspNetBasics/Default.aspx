<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" MasterPageFile="~/Master/Default.master" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<p>Welcome to the main section of the site. The following code examples are available</p>
    <ul>
        <li><a href="PageLifeCycle.aspx">ASP.NET page life cycle</a></li>
        <li><a href="CrossPageWizardStart.aspx">Cross-page posting</a></li>
		<li><a href="DivideByZero.aspx">Error handling</a></li>
        <li><a href="HolmesQuote.aspx">Component-based programming</a></li>
        <li><a href="SessionStateTest.aspx">Session state test</a></li>
		<li><a href="OutputCaching.aspx">Output caching</a></li>
    </ul>
    
</asp:Content>
