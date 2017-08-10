<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeBehind="ParentChild.aspx.cs" Inherits="AWDynamicData.ParentChild" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:DynamicDataManager ID="dmDynamicData" runat="server">
		<DataControls>
			<asp:DataControlReference ControlID="gvCategories" />
			<asp:DataControlReference ControlID="gvProducts" />
		</DataControls>
	</asp:DynamicDataManager>
	<asp:LinqDataSource ID="dsCategories" runat="server" ContextTypeName="AdventureWorks.Data.Linq.AdventureWorksDataContext"
		EntityTypeName="" TableName="ProductCategories">
	</asp:LinqDataSource>
	<h2>
		Parent Table: ProductCategories</h2>
	<asp:GridView ID="gvCategories" runat="server" DataSourceID="dsCategories" AllowPaging="True"
		AutoGenerateColumns="true" SelectedIndex="0">
		<Columns>
			<asp:CommandField ShowSelectButton="True" />
		</Columns>
	</asp:GridView>
		<asp:LinqDataSource ID="dsProducts" runat="server" ContextTypeName="AdventureWorks.Data.Linq.AdventureWorksDataContext"
		EntityTypeName="" TableName="Products">
	</asp:LinqDataSource>
	<h2>
		Child Table: Products</h2>
	<asp:GridView ID="gvProducts" runat="server" DataSourceID="dsProducts" AllowPaging="True"
		AutoGenerateColumns="true" SelectedIndex="0">
		<Columns>
			<asp:CommandField ShowSelectButton="True" />
		</Columns>
	</asp:GridView>

	<asp:QueryExtender ID="qexProducts" runat="server" TargetControlID="dsProducts">
		<asp:ControlFilterExpression ControlID="gvCategories" Column="ProductCategory" />
	</asp:QueryExtender>

</asp:Content>
