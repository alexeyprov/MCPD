. ".\Cmds-Database.ps1"
. ".\SampleDatabaseInAzure-SqlScripts.ps1"

### Parse the arguments
if ($args.Count -ne 3)
{
    "In order to setup the sample database in SQL Azure the following information is required."
    $SqlServerName = Read-Host "SQL Azure instance name"
    $UserId =  Read-Host "SQL Azure user id"
    $SecurePassword =  Read-Host "SQL Azure password" -AsSecureString
    
    $Password = Get-Password($SecurePassword)    
}
else
{
    $SqlServerName = $args[0]
    $UserId = $args[1]
    $Password = $args[2]
}

$dbName = "TreyResearch"

### Creates sample database ###
$SqlConnection = New-Object System.Data.SqlClient.SqlConnection
$SqlConnection.ConnectionString = "Server=$SqlServerName;Database=master;UId=$UserId;Pwd=$Password;"
$SqlConnection.Open(); 

$command = Get-DatabaseCreateSql;
Execute-SqlCommand -Sql $command -SqlConnection $SqlConnection -EA stop

$SqlConnection.Close()

"Database Created"

### Creates database objects
$SqlConnection = New-Object System.Data.SqlClient.SqlConnection
$SqlConnection.ConnectionString = "Server=$SqlServerName;Database=$dbName;UId=$UserId;Pwd=$Password;"
$SqlConnection.Open(); 

$command = Get-DatabaseSetupSql;
Execute-SqlCommand -Sql $command -SqlConnection $SqlConnection -EA stop

$SqlConnection.Close()

"Objects Created"