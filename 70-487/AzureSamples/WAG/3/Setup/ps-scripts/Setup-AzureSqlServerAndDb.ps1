. ".\Cmds-Database.ps1"

if ( (Get-PSSnapin -Name WAPPSCmdlets -ErrorAction SilentlyContinue) -eq $null)
{
	Add-PSSnapin WAPPSCmdlets
}

$subid = Read-Host "Please enter your subscription Id"
$thump = Read-Host "Please enter the thumbprint of your management certificate"

### Gets the management certificate
$cert = Get-Item cert:\\LocalMachine\My\$thump -EA stop

### Sets administrator credentials and region
$UserId =  Read-Host "Enter your SQL Azure user id"
$SecurePassword =  Read-Host "Enter your SQL Azure password" -AsSecureString
$location = Read-Host "Enter the location you want to use for the SQL Azure database (enter one of the following datacenters North Central US; South Central US; North Europe; West Europe; East Asia; Southeast Asia)"
$Password = Get-Password($SecurePassword)

### Creates Server
$result = New-SqlAzureServer -AdministratorLogin $UserId -AdministratorLoginPassword $Password -Location $location -SubscriptionId $subid -Certificate $cert -EA stop

$SqlServerName = $result.ServerName

"Created Server $SqlServerName"

$rule = New-SqlAzureFirewallRule -ServerName $SqlServerName -RuleName "MicrosoftServices" -StartIpAddress "0.0.0.0" -EndIpAddress "0.0.0.0" -SubscriptionId $subId -Certificate $cert -EA stop
"Created Firewall rule for Microsoft Services"

$startipaddress = Read-Host "Please enter the Start Client IP address for configuring the firewall rules"
$endipaddress = Read-Host "Please enter the End Client IP address for configuring the firewall rules"

### Creates Rules:
$rule = New-SqlAzureFirewallRule -ServerName $SqlServerName -RuleName "MyIpRule" -StartIpAddress $startipaddress -EndIpAddress $endipaddress -SubscriptionId $subId -Certificate $cert -EA stop 

"Creating Firewall rule for $startipaddress and $endipaddress, this will take ~60 seconds..."

Start-Sleep -s 60

.\Create-SampleDatabaseInAzure.ps1 "$SqlServerName.database.windows.net" $UserId $Password

