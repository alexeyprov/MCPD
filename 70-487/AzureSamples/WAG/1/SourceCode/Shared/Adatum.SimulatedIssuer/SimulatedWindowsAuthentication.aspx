<%--
===============================================================================
 Microsoft patterns & practices
 Windows Azure Architecture Guide
===============================================================================
 Copyright © Microsoft Corporation.  All rights reserved.
 This code released under the terms of the 
 Microsoft patterns & practices license (http://wag.codeplex.com/license)
===============================================================================
--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Styling/Site.Master" AutoEventWireup="true" CodeBehind="SimulatedWindowsAuthentication.aspx.cs" Inherits="Adatum.SimulatedIssuer.SimulatedWindowsAuthentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server" />

<asp:Content ID="Content2" ContentPlaceholderID="ContentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <div id="login">
        Adatum issuer is logging you in using Windows Integrated Authentication. Please select a User to continue:
        <div id="UserOptions">
            <asp:RadioButtonList ID="UserList" runat="server">
                <asp:ListItem Text="ADATUM\johndoe (Group: 'Employee')" Value="ADATUM\johndoe" Selected="True" />
                <asp:ListItem Text="ADATUM\mary (Group: 'Employee'; Group:'Manager')" Value="ADATUM\mary" />
                <asp:ListItem Text="ADATUM\newuser (Group: 'Employee')" Value="ADATUM\newuser" />
            </asp:RadioButtonList>
        </div>
        <asp:Button ID="ContinueButton" runat="server" class="tooltip" Text="Continue with login..." OnClick="ContinueButtonClick"  />
    </div>
</asp:Content>
