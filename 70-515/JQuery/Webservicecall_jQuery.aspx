<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
	<title>ASP.NET AJAX Web Services: Web Service Sample Page</title>
	<script type="text/javascript" src="scripts/jquery-1.5.js">
	</script>
	<script type="text/javascript">

		$(document).ready(function ()
		{
			$("#sayHelloButton").click(function (event)
			{
				$.ajaxSetup(
					{
						contentType : "application/json"
					});

				$.post(
					"DummyWebservice.asmx/HelloToYou",
					"{ name : \"" + $("#name").val() + "\"}",
					//$("#name").serializeArray(),
					function (msg)
					{
						AjaxSucceeded(msg);
					},
					"json");
			});
		});

		function AjaxSucceeded(result)
		{
			alert(result.d);
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<h1>
		Calling ASP.NET AJAX Web Services with jQuery
	</h1>
	Enter your name:
	<input id="name" name="name" />
	<br />
	<input id="sayHelloButton" value="Say Hello" type="button" />
	</form>
</body>
</html>
