<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Microsoft.ServiceBus.Samples._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Service Bus with Windows Azure</h1>
        <p>Enter some text to send to the 'listening' service: <asp:Label ID="serviceAddressLabel" runat="server" Font-Bold="true" Font-Italic="true" /></p>
        <asp:TextBox ID="echoTextBox" runat="server" Width="250px"></asp:TextBox>
        <br /><br />
        <asp:Button ID="sendButton" runat="server" Text="Send" 
            onclick="sendButton_Click" />
        <br /><br />
        <asp:Label ID="msgLabel" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
