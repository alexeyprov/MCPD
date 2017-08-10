<%@ Page Title="Territories by Regions" Language="C#" MasterPageFile="~/Northwind/Master/Northwind.master"
	AutoEventWireup="true" CodeFile="TerritoriesByRegions.aspx.cs" Inherits="Northwind.UI.Entities.TerritoriesByRegionsPage" 
	EnableEventValidation="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="NorthwindHeadContentPlaceHolder" runat="server">

	<script type="text/javascript">
	<!--

		function GetTerritories(regionId)
		{
			var service = new Northwind.Services.TerritoriesWcfService();

			service.GetTerritoriesByRegion(regionId, OnCallbackCompleted, OnCallbackError);
		}

		function OnCallbackError(result)
		{
			var lbl = $get("lblOutput");
			lbl.innerHtml = result.get_message();
		}

		function OnCallbackCompleted(result)
		{
			var combo = $get("cmbTerritories");

			// clear combo before results processing
			combo.innerHTML = "";

			if (result != null)
			{
				for (var i = 0; i < result.length; ++i)
				{
					var option = document.createElement("option");

					option.value = result[i].ID;
					option.innerHTML = result[i].Description;

					combo.appendChild(option);
				}
			}
		}

	//-->
	</script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="NorthwindContentPlaceHolder" Runat="Server">
	<h2>Territories by Regions</h2>
	<p>This page demonstrates usage of client callbacks.
	</p>
	<p>Choose a region, and then a territory</p>
	<asp:EntityDataSource runat="server" ID="dsRegions" ConnectionString="name=NorthwindObjectContext"
		ContextTypeName="Northwind.Data.Entities.NorthwindObjectContext" EntitySetName="Regions"
		 />
	<asp:ScriptManager ID="smAjax" runat="server">
		<Services>
			<asp:ServiceReference Path="~/Northwind/Entities/TerritoriesWcfService.svc" />
		</Services>
	</asp:ScriptManager>
	<table width="90%" cellspacing="10px">
		<tr>
			<td>
				<asp:DropDownList runat="server" ID="cmbRegions" DataSourceID="dsRegions"
					DataTextField="RegionDescription" DataValueField="RegionID" Width="100%" 
					onchange="GetTerritories(this.value);"/>
			</td>
			<td>
				<asp:DropDownList runat="server" ID="cmbTerritories" ClientIDMode="Static"
					Width="100%" />
			</td>
		</tr>
	</table>
	<br />
	<br />
	<asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_OnClick" />
	<br />
	<asp:Label ID="lblOutput" runat="server" ClientIDMode="Static"/>
</asp:Content>

