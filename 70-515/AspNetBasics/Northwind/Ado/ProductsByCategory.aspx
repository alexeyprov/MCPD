<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductsByCategory.aspx.cs" Inherits="Northwind.UI.Ado.ProductsByCategoryPage" Title="Products by Category" MasterPageFile="~/Northwind/Master/Northwind.master"%>

<asp:Content runat="server" ContentPlaceHolderID="NorthwindContentPlaceHolder" >
    <h2>Categories and Products</h2>
    <p>This page demonstrates usage of:</p>
    <ul>
        <li>Hierarchical GridView</li>
        <li>DataSet composed of two DataTables with calculated columns</li>
        <li>LINQ to DataSet</li>
        <li>SQL query notifications</li>
    </ul>
    
    <div>
        <asp:GridView ID="grdCategories" runat="server" AutoGenerateColumns="False"
            OnRowDataBound="grdCategories_RowDataBound" >
            <Columns>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <span><b><%# Eval("CategoryName") %></b> - (<%# Eval("Product Count") %> total)</span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Products">
                    <ItemTemplate>
                        <asp:GridView ID="grdProducts" runat="server" ShowFooter="true" AutoGenerateColumns="false"
                            EnableSortingAndPagingCallbacks="true" AllowSorting="true"
                            OnDataBound="grdProducts_DataBound">
                            <Columns>
                                <asp:BoundField DataField="ProductName" HeaderText="Name" SortExpression="ProductName" />
                                <asp:BoundField DataField="UnitPrice" HeaderText="Price" SortExpression="UnitPrice"
                                     DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>