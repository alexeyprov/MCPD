﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComplexTypes.aspx.cs" Inherits="ComplexTypes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 99px">
                    Name:</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" OnTextChanged="txt_TextChanged"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 99px">
                    Street:</td>
                <td>
                    <asp:TextBox ID="txtStreet" runat="server" OnTextChanged="txt_TextChanged"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 99px">
                    City:</td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" OnTextChanged="txt_TextChanged"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 99px">
                    Zip Code:</td>
                <td>
                    <asp:TextBox ID="txtZip" runat="server" OnTextChanged="txt_TextChanged"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 99px">
                    State:</td>
                <td>
                    <asp:TextBox ID="txtState" runat="server" OnTextChanged="txt_TextChanged"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 99px">
                    Country:</td>
                <td>
                    <asp:TextBox ID="txtCountry" runat="server" OnTextChanged="txt_TextChanged"></asp:TextBox></td>
            </tr>
        </table>
    
    </div>
        <br />
        <asp:Button ID="cmdGet" runat="server" OnClick="cmdGet_Click" Text="Get" />
        <asp:Button ID="cmdSave" runat="server" OnClick="cmdSave_Click" Text="Save" />
    </form>
</body>
</html>
