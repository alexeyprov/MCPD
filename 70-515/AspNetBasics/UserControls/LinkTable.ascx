<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LinkTable.ascx.cs" Inherits="LinkTableControl" %>
<table border="1px" cellpadding="2px">
	<tr>
		<td>
			<asp:Label ID="lblCaption" runat="server" Text=" "></asp:Label>
		</td>
	</tr>
	<tr>
		<td>
			<asp:GridView runat="server" ID="grdLinks" AutoGenerateColumns="False" GridLines="None"
				 EnableTheming="false" ShowHeader="False" onrowcommand="grdLinks_RowCommand">
				<Columns>
					<asp:TemplateField>
						<ItemTemplate>
							<img height="16" src="Images/Exclamation.gif" alt="Menu Item" style="vertical-align: middle" />
							<asp:LinkButton ID="lnk" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#0000cd"
								runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Text") %>' CommandName="LinkClick"
								CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Url") %>'>
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</td>
	</tr>
</table>
