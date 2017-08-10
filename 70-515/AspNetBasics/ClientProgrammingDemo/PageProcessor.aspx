<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageProcessor.aspx.cs" 
	Inherits="AspNetBasics.ClientProgrammingDemo.UI.PageProcessorPage" Theme="" StylesheetTheme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Load Page</title>
	<script type="text/javascript">
		var TIMER_INTERVAL = 500;
		var ITERATIONS = 6;

		var _timer;
		var _loopCounter = 1;

		function OnBodyLoaded()
		{
			var newLocation = "<%=PageToLoad %>";
			if (newLocation != null && newLocation != "")
			{
				location.href = newLocation;
				_timer = window.setInterval("_loopCounter = UpdateProgress(_loopCounter, ITERATIONS);", TIMER_INTERVAL);
			}
		}

		function OnBodyUnloaded()
		{
			window.clearInterval(_timer);
			ProgressMeter.innerHTML = "Page Loaded - Now Transferring";
		}

		function UpdateProgress(counter, numIterations)
		{
			if (counter < numIterations)
			{
				ProgressMeter.innerHTML += ".";
			}
			else
			{
				counter = 1;
				ProgressMeter.innerHTML = "";
			}
			return counter;
		}

	</script>
</head>
<body onload="OnBodyLoaded();" onunload="OnBodyUnloaded();">
	<form id="frmPageProcessor" runat="server" method="post">
	<table width="99%" border="0">
		<tr>
			<td align="center" valign="middle">
				<span id="MessageText">Loading - Please Wait</span>
				<span id="ProgressMeter"></span>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
