<%@ Page Title="Client-Side Control Demo" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" 
	CodeFile="HoverButtonTest.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.HoverButtonTestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" Runat="Server">
	<script type="text/javascript">
		<!--
		function pageLoad(sender, args)
		{
			$create(ControlExtensions.HoverButton, // type
				{
					text: "A HoverButton control",
					element: 
						{
							style: 
								{
									fontWeight: "bold",
									borderWidth: "2px"
								}
						}
				}, // properties
				{
					hover: btnClientButton_hover,
					unhover: btnClientButton_unhover
				}, // events
				null, // references
				$get("btnClientButton")); // element
		}

		function btnClientButton_hover(s, e)
		{
			var btn = $find("btnClientButton");

			if (typeof(btn) !== "undefined")
			{
				btn.set_text("In hover mode");
			}
		}

		function btnClientButton_unhover(s, e)
		{
			var btn = $find("btnClientButton");

			if (typeof (btn) !== "undefined")
			{
				btn.set_text("A HoverButton control");
			}
		}
		//-->
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<asp:ScriptManager runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/Scripts/HoverButton.js" />
		</Scripts>
	</asp:ScriptManager>
	<h2>Client-Side Control Demo</h2>
	<input type="button" value="Hover over me!" id="btnClientButton" >
	</input>
</asp:Content>

