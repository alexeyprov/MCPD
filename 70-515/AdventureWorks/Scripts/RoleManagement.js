function pageLoad()
{
	Sys.Services.RoleService.load(OnRolesLoadComplete, OnRolesLoadFailed, null);
}

function OnRolesLoadComplete(methodName, userContext)
{
	$get("adminDiv").style.display = (Sys.Services.RoleService.isUserInRole("Admin")) ?
				"block" : "none";
}

function OnRolesLoadFailed(error, methodName, userContext)
{
	alert(error.get_message());
}

if (typeof(Sys) !== "undefined")
{
	Sys.Application.notifyScriptLoaded();
}