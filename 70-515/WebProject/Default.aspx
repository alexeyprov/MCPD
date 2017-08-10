<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebProject._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
	<p>This is a default page of the WebProject application</p>
	<form id="form1" runat="server">
		<div>
			Please enter your name:
			<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
			<br />
			<asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" Text="Submit" />
		</div>
	</form>
</body>
</html>
