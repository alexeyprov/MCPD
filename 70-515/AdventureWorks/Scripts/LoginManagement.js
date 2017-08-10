function OnLoginClicked()
{
	var username = $get("UserName");
	var password = $get("Password");
	var persistent = $get("RememberMe");

	Sys.Services.AuthenticationService.login(username.value, password.value, persistent.checked, null, null,
				OnLoginSucceeded, OnLoginFailed, null);
	return false;
}

function OnLoginSucceeded(isValid, userContext, methodName)
{
	var result = (isValid) ? "Logged in successfully" : "Invalid login";

	$get("FailureText").innerText = result;
}

function OnLoginFailed(error, userContext, methodName)
{
	alert(error.get_message());
	alert(error.get_stacktrace());
	if (document.all)
	{
		$get("FailureText").innerText = error.get_stacktrace();
	}
	else
	{
		$get("FailureText").innerHtml = error.get_stacktrace();
	}
}

if (typeof(Sys) !== "undefined")
{
	Sys.Application.notifyScriptLoaded();
}