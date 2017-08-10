<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SampleCacheControl.ascx.cs" Inherits="UserControls_SampleCacheControl" %>
<%@ OutputCache Duration="90" VaryByParam="None" %>
<asp:Label ID="lblDateTime" runat="server" Text=""></asp:Label>