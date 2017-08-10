function pageLoad()
{
	var pageManager = Sys.WebForms.PageRequestManager.getInstance();
	pageManager.add_beginRequest(OnBeginAsyncRequest);
	pageManager.add_endRequest(OnEndAsyncRequest);
}

function pageUnload()
{
	var pageManager = Sys.WebForms.PageRequestManager.getInstance();
	pageManager.remove_beginRequest(OnBeginAsyncRequest);
	pageManager.remove_endRequest(OnEndAsyncRequest);
}

function OnBeginAsyncRequest(s, e)
{
	UpdateStatusDiv("StatusDiv", "visible", "update in progress...");
}

function OnEndAsyncRequest(s, e)
{
	UpdateStatusDiv("StatusDiv", "hidden", "");

	if (e.get_error() != null)
	{
		alert(e.get_error().message);
		e.set_errorHandled(true);
	}
}

function UpdateStatusDiv(elementName, visible, text)
{
	var element = $get(elementName);
	if (element != null)
	{
		element.style.visibility = visible;
		element.innerHTML = text;
	}
}

if (typeof (Sys) !== "undefined")
{
	Sys.Application.notifyScriptLoaded();
}