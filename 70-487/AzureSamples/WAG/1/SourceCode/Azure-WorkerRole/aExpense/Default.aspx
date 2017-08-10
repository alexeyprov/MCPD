﻿<%--
===============================================================================
 Microsoft patterns & practices
 Windows Azure Architecture Guide
===============================================================================
 Copyright © Microsoft Corporation.  All rights reserved.
 This code released under the terms of the 
 Microsoft patterns & practices license (http://wag.codeplex.com/license)
===============================================================================
--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Styling/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AExpense.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server" />

<asp:Content ID="Content2" ContentPlaceholderID="ContentPlaceholder" runat="server">
    <div id="expenselist">
        <asp:GridView ID="MyExpensesGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="MyExpensesGridViewOnRowCommand" EnableViewState="true" OnRowDataBound="MyExpensesGridViewOnRowDataBound">
            <Columns>
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"  HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CostCenter" HeaderText="Cost Center" SortExpression="CostCenter"  HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ReimbursementMethod" HeaderText="Reimb." SortExpression="ReimbursementMethod" HeaderStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Status" SortExpression="Approved" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate><%# (Boolean.Parse(Eval("Approved").ToString())) ? "Ready for Processing" : "Pending for Approval"%></ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="true" SelectText="»" HeaderStyle-Width="25px"
                    CausesValidation="false" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <HeaderStyle BackColor="#e6e6e6" />
            <EmptyDataTemplate>
                There are no expenses registered for <i>
                    <%= this.User.Identity.Name %></i>.
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
