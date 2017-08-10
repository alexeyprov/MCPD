<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutputCaching.aspx.cs" Inherits="OutputCaching"
	MasterPageFile="~/Master/Default.master" Title="Output Caching Sample" %>

<%@ Register Src="~/UserControls/SampleCacheControl.ascx" TagName="SampleCacheControl"
	TagPrefix="uc" %>
<%@ OutputCache Duration="60" VaryByParam="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<span>Cached time:</span>
	<br />
	<asp:Label ID="lblCachedTime" runat="server" />
	<br />
	<span>Non-cached time:</span>
	<br />
	<asp:Substitution ID="lblNonCachedTime" runat="server" MethodName="GetDate" />
	<br />
	<span>User control time:</span>
	<br />
	<uc:SampleCacheControl ID="ucSampleCacheControl" runat="server" />
</asp:Content>
