. ".\PS-Scripts\Cmds-Database.ps1"
. ".\PS-Scripts\SampleDatabaseInAzure-SqlScripts.ps1"
### Parse the arguments
if ($args.Count -ne 3)
{
    "In order to setup the sample database in Windows Azure SQL Database the following information is required."
    $SqlServerName = Read-Host "Windows Azure SQL Database instance name"
    $UserId =  Read-Host "Windows Azure SQL Database user id"
    $SecurePassword =  Read-Host "Windows Azure SQL Database password" -AsSecureString
    $Ptr = [System.Runtime.InteropServices.Marshal]::SecureStringToCoTaskMemUnicode($SecurePassword)
    $Password = [System.Runtime.InteropServices.Marshal]::PtrToStringUni($Ptr)
    [System.Runtime.InteropServices.Marshal]::ZeroFreeCoTaskMemUnicode($Ptr)
}
else
{
    $SqlServerName = $args[0]
    $UserId = $args[1]
    $Password = $args[2]
}

### The name of the sample database
$NewDatabaseName = "aExpense"

### Create, setup and populate sample database ###
$SqlConnection = New-Object System.Data.SqlClient.SqlConnection
$SqlConnection.ConnectionString = "Server=$SqlServerName;Database=master;UId=$UserId;Pwd=$Password;"
$SqlConnection.Open()

# delete the database if exists (this is used to reset the sample)
"Deleting $NewDatabaseName sample database"
Execute-SqlCommand -Sql (Get-DatabaseDeleteSql $NewDatabaseName) -SqlConnection $SqlConnection

# create the database
"Creating $NewDatabaseName sample database"
Execute-SqlCommand -Sql (Get-DatabaseCreateSql $NewDatabaseName) -SqlConnection $SqlConnection

$SqlConnection.Close()

#Create provider tables
& ".\Assets\aspnet_regsqlazure.exe" -S "$SqlServerName" -U "$UserId" -P "$Password" -A p -d "$NewDatabaseName"

# Create tables and populate sample data
$SqlConnection = New-Object System.Data.SqlClient.SqlConnection
$SqlConnection.ConnectionString = "Server=$SqlServerName;Database=$NewDatabaseName;UId=$UserId;Pwd=$Password;"
$SqlConnection.Open()

# setup database and populate with sample data
"Populating $NewDatabaseName sample database"
Execute-SqlCommand -Sql (Get-DatabaseSetupSql $NewDatabaseName) -SqlConnection $SqlConnection

# populate ASPNET Providers tables
Execute-SqlCommand -Sql (Get-DatabaseSetupAspNetSql $NewDatabaseName) -SqlConnection $SqlConnection

$SqlConnection.Close()
