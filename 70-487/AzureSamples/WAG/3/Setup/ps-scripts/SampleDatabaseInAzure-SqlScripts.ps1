function Get-DatabaseDeleteSql($DatabaseName)
{
	[Environment]::CurrentDirectory=(Get-Location -PSProvider FileSystem).ProviderPath
	
	$sqlScriptPath = "..\DependencyChecker\SqlScripts\TreyResearch-DropDatabase.sql";
	$reader = New-Object System.IO.StreamReader($sqlScriptPath);
	$command = $reader.ReadToEnd();
	$reader.Close();
	
	return $command;
}

function Get-DatabaseCreateSql($DatabaseName)
{
	[Environment]::CurrentDirectory=(Get-Location -PSProvider FileSystem).ProviderPath
	
	$sqlScriptPath = "..\DependencyChecker\SqlScripts\create_TreyResearch.sql";
	$reader = New-Object System.IO.StreamReader($sqlScriptPath);
	$command = $reader.ReadToEnd();
	$reader.Close();
	
	return $command;
}

function Get-DatabaseSetupSql($DatabaseName)
{
	[Environment]::CurrentDirectory=(Get-Location -PSProvider FileSystem).ProviderPath
	
	$sqlScriptPath = "..\DependencyChecker\SqlScripts\create_objects_TreyResearch.sql";
	
	$reader = New-Object System.IO.StreamReader($sqlScriptPath);
	$command = $reader.ReadToEnd();
	$reader.Close();
	
	return $command;
}


