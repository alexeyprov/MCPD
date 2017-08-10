<%@ Page Title="Simple Ajax Demo" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="AjaxSimple.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.AjaxSimplePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" Runat="Server">
	<script type="text/javascript">

		var HANDLER_URL_PREFIX = "CalculatorHandler.ashx?";
		var METHOD = "GET";

		var ReadyStates = 
		{
			NotInitialized: 0,
			ConnectionEstablished: 1,
			RequestReceived: 2,
			ProcessingRequest: 3,
			RequestFinished: 4
		};

		var HttpStatuses = 
		{
			OK: 200,
			NotFound: 404
		};

		var _ajaxRequest;

		function CreateAjaxRequest()
		{
			try
			{
				_ajaxRequest = new XMLHttpRequest();
			}
			catch (ex)
			{
				_ajaxRequest = new ActiveXObject("Microsoft.XMLHTTP");
			}
		}

		function SendAjaxRequest()
		{
			_ajaxRequest.open(METHOD, HANDLER_URL_PREFIX + 
								FormatAjaxRequestParameter("op1") + "&" +
								FormatAjaxRequestParameter("op2"));
			_ajaxRequest.onreadystatechange = OnRequestStatusUpdated;
			_ajaxRequest.send();
		}

		function OnRequestStatusUpdated()
		{
			if (ReadyStates.RequestFinished == _ajaxRequest.readyState &&
				HttpStatuses.OK == _ajaxRequest.status)
			{
				// unsubscribe first
				if (_ajaxRequest != null)
				{
					_ajaxRequest.onreadystatechange = null;
				}

				var lbl = document.getElementById("lbl");
				var result = _ajaxRequest.responseText;

				// parse result
				var separatorIndex = result.indexOf(",");

				if (-1 == separatorIndex)
				{
					lbl.innerHTML = "Error during ajax request";
				}
				else
				{
					lbl.innerHTML = "Sum: " + result.substring(0, separatorIndex) +
					"&nbsp;Last updated: " + result.substring(separatorIndex + 1);
				}
			}
		}

		function FormatAjaxRequestParameter(controlId)
		{
			var control = document.getElementById(controlId);
			if (control != null)
			{
				return controlId + "=" + control.value;
			}
			return null;
		}

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<table width="300px">
		<tr>
			<td>
				<img src="../Images/Hourglass.gif" alt="Hourglass"/>
			</td>
			<td>
				<span>
					Since call is done asynchronously, the animated image never stops
				</span>
			</td>
		</tr>
	</table>
	<br />
	<span>
		First value:&nbsp;
	</span>
	<input type="text" id="op1" maxlength="10" onkeyup="SendAjaxRequest();" value="0" />
	<br />
	<span>
		Second value:&nbsp;
	</span>
	<input type="text" id="op2" maxlength="10" onkeyup="SendAjaxRequest();" value="0" />
	<br />
	<br />
	<div id="lbl" />
</asp:Content>

