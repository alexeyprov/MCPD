<%@ Control Language="C#" CodeBehind="Default.ascx.cs" Inherits="NorthwindDynamicData.DefaultEntityTemplate" %>

<asp:EntityTemplate runat="server" ID="EntityTemplate1">
    <ItemTemplate>
        <tr class="td">
			<td class="DDLightHeader">
				Field name:
			</td>
            <td>
                <asp:Label runat="server" OnInit="Label_Init" />
            </td>
			<td class="DDLightHeader">
				Field value:
			</td>
            <td>
                <asp:DynamicControl runat="server" OnInit="DynamicControl_Init" />
            </td>
        </tr>
    </ItemTemplate>
</asp:EntityTemplate>

