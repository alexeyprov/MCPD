<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Templating.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.TemplatingPage" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>ASP.NET Basics</title>
	<style type="text/css">
		.sys-template
		{
			visibility: hidden;
			display: none;
		}
	</style>
	<script language="javascript" type="text/javascript">
		var authors = [
			{
				FirstName: "Piyush",
				LastName: "Shah",
				Url: "http://blogs.msdn.com/shahpiyush"
			},
			{
				FirstName: "Jon",
				LastName: "Gallant",
				Url: "http://blogs.msdn.com/jongallant"
			},
			{
				FirstName: "Scott",
				LastName: "Guthrie",
				Url: "http://webblogs.asp.com/scottgu"
			}
		];

		function pageLoad() {
			/*
			Sys.require([Sys.scripts.Templates]);

			var template = new Sys.UI.Template($get("authorTemplate"));
			for (var i in authors) {
				template.instantiateIn($get("targetDiv"), authors[i]);
			}
			*/
		}
	</script>
	<link href="~/App_Themes/MasterStyleSheet.css" rel="Stylesheet" type="text/css" runat="server" />
	<style type="text/css">
		body
		{
			background-color: #FFFFFF;
		}
		h1
		{
			color: #000000;
		}
		h2
		{
			color: #000000;
		}
	</style>
</head>
<body runat="server" id="body" xmlns:sys="javascript:Sys" xmlns:dataview="javascript:Sys.UI.DataView" sys:activate="authorTemplate">
	<form id="form1" runat="server">
	<asp:XmlDataSource ID="srcMasterPages" runat="server" DataFile="~/App_Data/MasterPages.xml"
		CacheExpirationPolicy="Sliding" />
	<asp:SiteMapDataSource ID="srcSiteMap" runat="server" 
		SiteMapProvider="RootSiteMapProvider" ShowStartingNode="False" />
	<table width="100%" cellspacing="5px">
		<tr>
			<td width="100%" align="left">
				<h1 id="H1" runat="server" innertext='<%$ Resources: Resource, ApplicationName %>'>
				</h1>
			</td>
			<td width="100px">
				&nbsp;
				<asp:Image ID="imgLogo" runat="server" SkinID="LogoImage"/>
			</td>
			<td width="120px">
				<asp:DropDownList ID="ddlLanguages" runat="server" 
					AutoPostBack="true" 
				                  Width="120px">
					<asp:ListItem Selected="True" 
						Value="en-US">English</asp:ListItem>
					<asp:ListItem Value="ru-RU">Russian</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td width="100%" align="left">
				A web-site for MS exam <a href="http://www.microsoft.com/learning/en/us/exam.aspx?ID=70-515"
					target="blank">70-515</a> training
			</td>
			<td width="100px" align="right">
				Master&nbsp;Page/Theme:
			</td>
			<td width="120px">
				<asp:DropDownList ID="ddlMasters" runat="server" Width="120px" AutoPostBack="True"
					DataSourceID="srcMasterPages" DataTextField="Name" DataValueField="Path">
				</asp:DropDownList>
			</td>
		</tr>
	</table>
	<asp:Menu ID="mnuMain" runat="server" DataSourceID="srcSiteMap" 
		Orientation="Horizontal" />
	<!--
		Orientation="Horizontal" StaticTopSeparatorImageUrl="~/App_Themes/Blue/img/diamond.gif">
	-->
	<hr />
	<div class="mainBody">
	<ajaxToolkit:ToolkitScriptManager runat="server">
		<Scripts>
			<asp:ScriptReference Name="MicrosoftAjaxTemplates.js" />
			<asp:ScriptReference Name="MicrosoftAjax.js" />
		</Scripts>
	</ajaxToolkit:ToolkitScriptManager>
	<div id="authorTemplate" >
		<ul class="sys-template" sys:attach="dataview" dataview:data="{{ authors }}">
			<li>First: {{FirstName}}</li>
			<li>Last: {{LastName}}</li>
			<li>Url: <a href="{{Url}}">{{Url}}</a></li>
		</ul>
		<br />
	</div>
	<div id="targetDiv">
	</div>
	</div>
	<br />
	<asp:SiteMapPath ID="smpMain" runat="server" />
	</form>
	<br />
	<center>
		<small>Copyright (C) 2009-2011 by Alexey Provotorov</small>
	</center>
</body>
</html>


