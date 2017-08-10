<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true"
	CodeFile="Default.aspx.cs" Inherits="WebParts.UI.DefaultPage" %>

<%@ Register src="Customers.ascx" tagname="Customers" tagprefix="uc1" %>
<%@ Register Namespace="WebParts" TagPrefix="uc2" %>
<%@ Register Namespace="ControlExtensions" Assembly="ControlExtensions" TagPrefix="ce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
	<div>
		<asp:WebPartManager ID="wpManager" runat="server" 
			onauthorizewebpart="wpManager_AuthorizeWebPart">
			<StaticConnections>
				<asp:WebPartConnection ConsumerID="partNoteEditor" ConsumerConnectionPointID="NotesConsumerID"
					ProviderID="partNotes" ProviderConnectionPointID="NotesProviderID" ID="notesConnection"/>
			</StaticConnections>
		</asp:WebPartManager>
		<table style="width: 100%">
			<tr valign="middle" style="background: #00ccff">
				<td colspan="2">
					<span style="font-size: 16pt; font-family: Verdana">
						<strong>Welcome to Web Part pages!</strong>
					</span>
				</td>
				<td style="height: 22px">
					<asp:Menu ID="mnuDisplayMode" runat="server" 
						onmenuitemclick="mnuDisplayMode_MenuItemClick">
						<Items>
							<asp:MenuItem Text="Select Mode" Value="Select Mode"></asp:MenuItem>
						</Items>
					</asp:Menu>
				</td>
			</tr>
			<tr valign="top">
				<td style="width: 20%">
					<asp:CatalogZone ID="zoneCatalog" runat="server" BackColor="#F7F6F3" 
						BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" Padding="6">
						<ZoneTemplate>
							<asp:ImportCatalogPart ID="partImportCatalog" runat="server" />
							<asp:PageCatalogPart ID="partPageCatalog" runat="server" />
						</ZoneTemplate>
						<PartLinkStyle Font-Size="0.8em" />
						<SelectedPartLinkStyle Font-Size="0.8em" />
						<EditUIStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
						<HeaderVerbStyle Font-Bold="False" Font-Size="0.8em" Font-Underline="False" 
							ForeColor="#333333" />
						<InstructionTextStyle Font-Size="0.8em" ForeColor="#333333" />
						<LabelStyle Font-Size="0.8em" ForeColor="#333333" />
						<EmptyZoneTextStyle Font-Size="0.8em" ForeColor="#333333" />
						<FooterStyle BackColor="#E2DED6" HorizontalAlign="Right" />
						<HeaderStyle BackColor="#E2DED6" Font-Bold="True" Font-Size="0.8em" 
							ForeColor="#333333" />
						<PartChromeStyle BorderColor="#E2DED6" BorderStyle="Solid" BorderWidth="1px" />
						<PartStyle BorderColor="#F7F6F3" BorderWidth="5px" />
						<PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" 
							ForeColor="White" />
						<VerbStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
					</asp:CatalogZone>
					<asp:EditorZone ID="zoneEditor" runat="server">
						<ZoneTemplate>
							<asp:PropertyGridEditorPart ID="PropertyGridEditorPart1" runat="server" />
							<asp:AppearanceEditorPart runat="server" ID="partAppearanceEditor">
							</asp:AppearanceEditorPart>
						</ZoneTemplate>
					</asp:EditorZone>
					<asp:ConnectionsZone ID="zoneConnector" runat="server">
					</asp:ConnectionsZone>
				</td>
				<td style="width: 60%">
					<asp:WebPartZone ID="zoneMain" runat="server" BorderColor="#CCCCCC" 
						Font-Names="Verdana" Padding="6">
						<ZoneTemplate>
							<uc1:Customers ID="ucCustomers" runat="server" OnLoad="ucCustomers_Load"/>
							<uc2:CustomerNotesPart ID="partNotes" runat="server" />
						</ZoneTemplate>
						<MenuLabelHoverStyle ForeColor="#E2DED6" />
						<MenuLabelStyle ForeColor="White" />
						<MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" 
							Font-Names="Verdana" Font-Size="0.6em" />
						<MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" 
							BorderStyle="Solid" BorderWidth="1px" ForeColor="#333333" />
						<MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" 
							ForeColor="White" />
						<TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
						<EmptyZoneTextStyle Font-Size="0.8em" />
						<HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
						<PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" 
							ForeColor="White" />
						<PartStyle Font-Size="0.8em" ForeColor="#333333" />
						<PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" 
							ForeColor="White" />
					</asp:WebPartZone>
				</td>
				<td style="width: 20%">
					<asp:WebPartZone ID="zoneHelp" runat="server" BorderColor="#CCCCCC" 
						Font-Names="Verdana" Padding="6">
						<ZoneTemplate>
							<uc2:CustomerNotesEditorPart ID="partNoteEditor" runat="server" />
							<asp:Calendar ID="calDateHelper" runat="server"></asp:Calendar>
							<asp:FileUpload ID="uplDataFile" runat="server" />
						</ZoneTemplate>
						<MenuLabelHoverStyle ForeColor="#E2DED6" />
						<MenuLabelStyle ForeColor="White" />
						<MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" 
							Font-Names="Verdana" Font-Size="0.6em" />
						<MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" 
							BorderStyle="Solid" BorderWidth="1px" ForeColor="#333333" />
						<MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" 
							ForeColor="White" />
						<TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
						<EmptyZoneTextStyle Font-Size="0.8em" />
						<HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
						<PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" 
							ForeColor="White" />
						<PartStyle Font-Size="0.8em" ForeColor="#333333" />
						<PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" 
							ForeColor="White" />
					</asp:WebPartZone>
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
