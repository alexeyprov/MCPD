<%@ Page Title="Territories by Regions" Language="C#" MasterPageFile="~/Northwind/Master/Northwind.master"
	AutoEventWireup="true" CodeFile="TerritoriesByRegions.aspx.cs" Inherits="Northwind.UI.Ado.TerritoriesByRegionsPage" 
	EnableEventValidation="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="NorthwindHeadContentPlaceHolder" runat="server">

	<script type="text/javascript">
	<!--
		function OnCallbackCompleted(result, context)
		{
			var combo = document.getElementById("cmbTerritories");

			// clear combo before results processing
			combo.innerHTML = "";

			if (result != null)
			{
				var jsDataset = JSON.parse(result);

				if (jsDataset != null)
				{
					for (var i = 0; i < jsDataset.length; ++i)
					{
						var option = document.createElement("option");

						option.value = jsDataset[i].ID;
						option.innerHTML = jsDataset[i].Description;

						combo.appendChild(option);
					}
				}
			}

			combo.disabled = false;
		}

	//-->
	</script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="NorthwindContentPlaceHolder" Runat="Server">
	<h2>Territories by Regions</h2>
	<p>This page demonstrates usage of client callbacks.
	</p>
	<p>Choose a region, and then a territory</p>
	<asp:SqlDataSource runat="server" ID="dsRegions" ConnectionString="<%$ ConnectionStrings: Northwind %>"
		SelectCommand="SP_REGIONS_GET" SelectCommandType="StoredProcedure" />
	<table cellspacing="10px">
		<tr>
			<td>
				<asp:DropDownList runat="server" ID="cmbRegions" DataSourceID="dsRegions"
					DataTextField="RegionDescription" DataValueField="RegionID" Width="150px" />
			</td>
			<td>
				<asp:DropDownList runat="server" ID="cmbTerritories" ClientIDMode="Static"
					Width="200px" Enabled="false" />
			</td>
		</tr>
	</table>
	<br />
	<br />
	<asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_OnClick" />
	<br />
	<asp:Label ID="lblOutput" runat="server" />
</asp:Content>

