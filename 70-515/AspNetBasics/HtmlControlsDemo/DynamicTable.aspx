<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DynamicTable.aspx.cs" Inherits="HtmlControlsDemo_DynamicTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dynamic Table Sample</title>
    <link href="../App_Themes/MasterStyleSheet.css" rel="Stylesheet" />
</head>
<body>
    <h1>Dynamic Table Sample</h1>
    <form id="frm" runat="server">
    <div>
    	<table class="max_width">
			<tr>
				<td class="labelCell">
					Row count:
			    </td>
				<td>
					<asp:TextBox ID="txtRowCount" runat="server" MaxLength="1"></asp:TextBox>
					<asp:RequiredFieldValidator ID="valRowCountPresent" runat="server" 
						ControlToValidate="txtRowCount" Display="Dynamic">Required field!</asp:RequiredFieldValidator>
					<asp:RangeValidator ID="valRowCountRange" runat="server" 
						ControlToValidate="txtRowCount" Display="Dynamic" MaximumValue="5" 
						MinimumValue="1" Type="Integer">Row count should be a number between 1 and 5</asp:RangeValidator>
				</td>
			</tr>
			<tr>
				<td class="labelCell">
					Column count:
			    </td>
				<td>
					<asp:TextBox ID="txtColCount" runat="server" MaxLength="1"></asp:TextBox>
					<asp:RequiredFieldValidator ID="valColCountPresent" runat="server" 
						ControlToValidate="txtColCount" Display="Dynamic">Required field!</asp:RequiredFieldValidator>
					<asp:RangeValidator ID="valColCountRange" runat="server" 
						ControlToValidate="txtColCount" Display="Dynamic" MaximumValue="5" 
						MinimumValue="1" Type="Integer">Column count should be a number between 1 and 5</asp:RangeValidator>
				</td>
			</tr>			
		</table>
		<asp:Button ID="btnGenerate" runat="server" Text="Generate" 
			onclick="btnGenerate_Click" />
    </div>
    <div>
		<asp:PlaceHolder ID="phTable" runat="server"></asp:PlaceHolder>
	</div>
    </form>
</body>
</html>
