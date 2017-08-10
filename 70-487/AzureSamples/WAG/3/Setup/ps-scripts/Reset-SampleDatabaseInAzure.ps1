. ".\Cmds-Database.ps1"
. ".\SampleDatabaseInAzure-SqlScripts.ps1"

### Parse the arguments
if ($args.Count -ne 3)
{
    "In order to setup the sample database in SQL Azure the following information is required."
    $SqlServerName = Read-Host "SQL Azure fully qualified DNS instance name"
}
else
{
    $SqlServerName = $args[0]
}

### Sets administrator credentials and region
$UserId =  Read-Host "Enter your SQL Azure user id"
$Password =  Read-Host "Enter your SQL Azure password" -AsSecureString

$SqlConnection = New-Object System.Data.SqlClient.SqlConnection
$SqlConnection.ConnectionString = "Server=$SqlServerName;Database=master;UId=$UserId;Pwd=$Password;"
$SqlConnection.Open()

# delete the database if exists (this is used to reset the sample)
"Deleting sample database"
Execute-SqlCommand -Sql (Get-DatabaseDeleteSql) -SqlConnection $SqlConnection

$SqlConnection.Close()

### Calls the create script
.\Create-SampleDatabaseInAzure.ps1 $SqlServerName $UserId $Password
