<%@ Page Title="Amazon Books" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" 
	CodeFile="IncrementalDownloadGrid.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.IncrementalDownloadGridPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" Runat="Server">
	<script type="text/javascript">
		<!--
		function GetBookImage(img, isbn)
		{
			img.onload = null;
			img.src = "GetImagePage.aspx?isbn=" + isbn;
		}
		//-->
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<asp:XmlDataSource runat="server" ID="dsBooks" DataFile="~/App_Data/BookList.xml" XPath="/Books/Book" />
	<asp:GridView runat="server" ID="grdBooks" DataSourceID="dsBooks" AutoGenerateColumns="false">
		<Columns>
			<asp:BoundField DataField="Title" HeaderText="Title" />
			<asp:BoundField DataField="isbn" HeaderText="ISBN" />
			<asp:BoundField DataField="Publisher" HeaderText="Publisher" />
			<asp:TemplateField HeaderText="Cover">
				<ItemTemplate>
					<img src="../Images/DefaultBookCover.jpg" 
						onerror="this.src='../Images/DefaultBookCover.jpg';"
						onload="GetBookImage(this, '<%# DataBinder.Eval(Container.DataItem, "isbn") %>');" />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
</asp:Content>

