<%@ Page Language="C#" %>

<%@ Import Namespace="System.Web.Services" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
	<title>ASP.NET AJAX Web Services: Web Service Sample Page</title>
	<link href="templating.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="scripts/jquery-1.5.js">   
	</script>
	<script src="scripts/jquery-jtemplates.min.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function ()
		{
			$.ajax({
				type: "POST",
				url: "CDCatalog.asmx/GetCDCatalog",
				data: "{}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (msg)
				{
					//The old and tired way of building an interminable string
					// from data
					// BuildTable(msg.d);

					//instantiate a template with data
					ApplyTemplate(msg);
				}
			});
		});


		function BuildTable(msg)
		{
			var table = '<table><thead><tr><th>Artist</th><th>Company</th><th>Title</th><th>Price</th></thead><tbody>';

			for (var cd in msg)
			{
				var row = '<tr>';

				row += '<td>' + msg[cd].Artist + '</td>';
				row += '<td>' + msg[cd].Company + '</td>';
				row += '<td>' + msg[cd].Title + '</td>';
				row += '<td>' + msg[cd].Price + '</td>';

				row += '</tr>';

				table += row;
			}

			table += '</tbody></table>';

			$('#Container').html(table);
		}

		function ApplyTemplate(msg)
		{
			$('#Container').setTemplate($("#TemplateResultsTable").html());
			$('#Container').processTemplate(msg);
		}
	</script>
</head>
<body>
	<div id="Container" />
	<script type="text/html" id="TemplateResultsTable">
{#template MAIN}
<table  cellpadding="10" cellspacing="0">
  <tr>
    <th>Artist</th>
    <th>Company</th>
    <th>Title</th>
    <th>Price</th>
  </tr>
  {#foreach $T.d as CD}
    {#include ROW root=$T.CD}
  {#/for}
</table>
{#/template MAIN}

{#template ROW}
<tr class="{#cycle values=['','evenRow']}">
  <td>{$T.Artist}</td>
  <td>{$T.Company}</td>
  <td>{$T.Title}</td>
  <td>{$T.Price}</td>
</tr>
{#/template ROW}
	</script>
</body>
</html>
