<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<string>>" %>

<script type="text/javascript">
	<!--
	$(document).ready(
		function ()
		{
			$("#dlgEditCustomer").dialog(
				{
					autoOpen : false,
					modal : true,
					title : "Edit Customer",
					width: 550,
					height: 400,
					position: [600, 250]
				}
			);
		}
	);

	function OpenCustomerDialog(customerID,
								customerName,
								customerContact,
								customerAddress,
								customerCity,
								customerCountry)
	{
		var dlgCustomer = $("#dlgEditCustomer");
		dlgCustomer.dialog("option", "title", "Customer: [" + customerName + "]");

		$("#dlgEditCustomer input[id=txtCustomerID]").val(customerID);
		$("#dlgEditCustomer input[id=txtCustomerCompany]").val(customerName);
		$("#dlgEditCustomer input[id=txtCustomerContact]").val(customerContact);
		$("#dlgEditCustomer input[id=txtCustomerAddress]").val(customerAddress);
		$("#dlgEditCustomer input[id=txtCustomerCity]").val(customerCity);
		$("#dlgEditCustomer select > option:contains(\"" + customerCountry + "\")").attr("selected", "selected");

		$("#dlgEditCustomer form").attr("action", "/Customer/Edit/" + customerID);

		dlgCustomer.dialog("open");
	}
	//-->
</script>

<div id="dlgEditCustomer">
	<%  using (Html.BeginForm("Edit", "Customer"))
		{
	%>
	<table style="border-style: none">
		<tr>
			<td align="right">
				<b>ID</b>
			</td>
			<td>
				<input type="text" id="txtCustomerID" disabled="disabled" />
			</td>
		</tr>
		<tr>
			<td align="right">
				<b>Company</b>
			</td>
			<td>
				<input type="text" id="txtCustomerCompany" name="CompanyName"/>
			</td>
		</tr>
		<tr>
			<td align="right">
				<b>Contact</b>
			</td>
			<td>
				<input type="text" id="txtCustomerContact" name="ContactName" />
			</td>
		</tr>
		<tr>
			<td align="right">
				<b>Address</b>
			</td>
			<td>
				<input type="text" id="txtCustomerAddress" name="Address" />
			</td>
		</tr>
		<tr>
			<td align="right">
				<b>City</b>
			</td>
			<td>
				<input type="text" id="txtCustomerCity" name="City" />
			</td>
		</tr>
		<tr>
			<td align="right">
				<b>Country</b>
			</td>
			<td>
				<%: Html.DropDownList("Country", new SelectList(Model)) %>
			</td>
		</tr>
	</table>

	<input type="submit" value="OK" />

	<%
		}
	%>
</div>