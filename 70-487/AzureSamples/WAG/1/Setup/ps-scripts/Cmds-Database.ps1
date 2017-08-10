function Get-SqlServerNameInComputer()
{
    $SqlServerName = ""
    $SqlServerNamesToCheck = @('.\SQLEXPRESS', '.')
    foreach ($SqlServerName in $SqlServerNamesToCheck)
    {
        if (Check-Database -SqlServerName "$SqlServerName" -DatabaseName 'master')
        {
            return $SqlServerName;
        }
    }
	
	return $null
}

function Check-Database($SqlServerName, $DatabaseName)
{
    $error.Clear()
    trap {continue;}

    $SqlConnection = New-Object System.Data.SqlClient.SqlConnection
	$SqlConnection.ConnectionString = "Server=$SqlServerName;Database=$DatabaseName;Integrated Security=True;Connection Timeout=1;"

    $SqlConnection.Open()
    $SqlConnection.Close()

    if ($error.Count -gt 0)
    {
        return $false
    }

    return $true
}


function Create-SqlAlias($SqlServerName, $AliasName)
{
    # if SqlServerName then use the sql server
    if ($SqlServerName -eq '.')
    {
        $AliasValue = 'DBNMPNTW,\.\PIPE\sql\query'
    }
    # else use the sql express
    else
    {
        $AliasValue = 'DBNMPNTW,\\.\PIPE\MSSQL$SQLEXPRESS\sql\query'
    }
    
    # test if it's running x64 add it to both paths, if not use the x86 path only
    if ((Test-Path -path "HKLM:\SOFTWARE\Wow6432Node\Microsoft\MSSQLServer") -eq $True) 
	{
        $PropertyPath = "SOFTWARE\Wow6432Node\Microsoft\MSSQLServer\Client\ConnectTo"
        Add-NewKeyValueToRegistry -PropertyPath $PropertyPath -PropertyName $AliasName -PropertyValue $AliasValue
    }
    
    $PropertyPath = "SOFTWARE\Microsoft\MSSQLServer\Client\ConnectTo"
    Add-NewKeyValueToRegistry -PropertyPath $PropertyPath -PropertyName $AliasName -PropertyValue $AliasValue
}

function Execute-SqlCommand($Sql, $SqlConnection)
{
    $cmd = new-object "System.Data.SqlClient.SqlCommand" ($Sql, $SqlConnection)
    $cmd.ExecuteNonQuery() | out-null
    $cmd = $null
}

function Add-NewKeyValueToRegistry($PropertyPath, $PropertyName, $PropertyValue)
{
    $reg = [Microsoft.Win32.RegistryKey]::OpenRemoteBaseKey('LocalMachine', $null)         
    $regKey= $reg.CreateSubKey($PropertyPath)
    $regkey.SetValue($AliasName, $PropertyValue)
}