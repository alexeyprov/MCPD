<%@ Page Language="C#" AutoEventWireup="false" CodeFile="XmlDataSourceXSLT.aspx.cs" Inherits="XmlDataSourceXSLT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:XmlDataSource ID="sourceDVD" runat="server" DataFile="~/DvdList.xml"
      TransformFile="DVDTreeList.xsl"
     ></asp:XmlDataSource>
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="sourceDVD"
         AutoGenerateDataBindings="False">
            <DataBindings>
                <asp:TreeNodeBinding DataMember="Movies" Text="Movies" />
                <asp:TreeNodeBinding DataMember="DVD" TextField="Title" />
                <asp:TreeNodeBinding DataMember="Star" TextField="Name" />
            </DataBindings>
      

        </asp:TreeView>
    </div>
    </form>
</body>
</html>
