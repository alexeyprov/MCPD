function pageLoad()
{
	Sys.Services.ProfileService.load(null, OnProfileLoadSuccessful, OnProfileLoadError, null);
}

function OnProfileLoadSuccessful(numProperties, userContext, methodName)
{
	$get("txtFirstName").value = Sys.Services.ProfileService.properties.FirstName;
	$get("txtLastName").value = Sys.Services.ProfileService.properties.LastName;
}

function OnProfileLoadError(error, userContext, methodName)
{
	alert(error.get_message());
}

function OnUpdateClicked()
{
	Sys.Services.ProfileService.properties.FirstName = $get("txtFirstName").value;
	Sys.Services.ProfileService.properties.LastName = $get("txtLastName").value;
	Sys.Services.ProfileService.save(null, OnProfileSaveSuccessful, OnProfileSaveError);
	return false;
}

function OnResetClicked()
{
	$get("txtFirstName").value = "";
	$get("txtLastName").value = "";
	return false;
}

function OnProfileSaveSuccessful(arg, userContext, methodName)
{
	alert("Profile saved!");
}

function OnProfileSaveError(error, userContext, methodName)
{
	alert(error.get_message());
}

if (typeof(Sys) !== "undefined")
{
	Sys.Application.notifyScriptLoaded();
}