<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DynamicStyle.aspx.cs" Inherits="HtmlControlsDemo_DynamicStyle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dynamic Style Sample</title>
    <link href="../App_Themes/MasterStyleSheet.css" rel="Stylesheet" />
</head>
<body>
    <h1>Dynamic Style Sample</h1>
    <form id="frm" runat="server">
    <div>
		
    	<table class="max_width">
			<tr>
				<td class="labelCell">
					Fore color:
				</td>
				<td>
					<asp:DropDownList ID="cbForeColor" runat="server" DataSourceID="dsColors" 
						DataTextField="name" DataValueField="value">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td class="labelCell">
					Back color:
				</td>
				<td>
					<asp:DropDownList ID="cbBackColor" runat="server" DataSourceID="dsColors" 
						DataTextField="name" DataValueField="value">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td class="labelCell">
					Font weight:
				</td>
				<td>
					<asp:TextBox ID="txtFontWeight" runat="server" MaxLength="3"></asp:TextBox>
					<asp:RequiredFieldValidator ID="valFontWeightPresent" runat="server" 
						ControlToValidate="txtFontWeight" Display="Dynamic">Required field!</asp:RequiredFieldValidator>
					<asp:RangeValidator ID="valFontWeightRange" runat="server" 
						ControlToValidate="txtFontWeight" Display="Dynamic" MaximumValue="20" 
						MinimumValue="5" Type="Integer">Font weight should be a number between 5 and 20</asp:RangeValidator>
				</td>
			</tr>
		</table>
		<asp:Button ID="btnUpdate" runat="server" Text="Update" 
			onclick="btnUpdate_Click" />
    </div>
    <div>
        <span>Enter some data here:</span>
        <input type="text" id="txtSample" runat="server" />
    </div>
    <asp:XmlDataSource ID="dsColors" runat="server" 
		DataFile="~/App_Data/ColorList.xml"></asp:XmlDataSource>
    </form>
</body>
</html>
