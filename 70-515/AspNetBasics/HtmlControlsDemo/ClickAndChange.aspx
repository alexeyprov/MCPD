<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClickAndChange.aspx.cs" Inherits="HtmlControlsDemo_ClickAndChange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>ServerClick vs ServerChange Demo</title>
</head>
<body>
	<h1>
		ServerClick vs ServerChange Demo</h1>
	<form runat="server">
	<div>
		<select runat="server" id="lstOptions" size="5" multiple="true" onserverchange="lstOptions_ServerChange">
			<option>Option 1</option>
			<option>Option 2</option>
		</select>
		<br />
		<input type="text" runat="server" id="txtText" size="10" onserverchange="ctrl_ServerChange"/>
		<br />
		<input type="checkbox" runat="server" id="chkCheckBox" onserverchange="ctrl_ServerChange"/>
		Option text
		<br />
		<input type="submit" runat="server" id="cmdSubmit" value="Submit Query" onserverclick="cmdSubmit_ServerClick"/>
	</div>
	</form>
</body>
</html>
