. ".\Cmds-Database.ps1"

if ( (Get-PSSnapin -Name WAPPSCmdlets -ErrorAction SilentlyContinue) -eq $null)
{
	Add-PSSnapin WAPPSCmdlets
}

### Sets administrator credentials and region
$SqlServerName = Read-Host "Please enter the sql server name"
$SqlUserId =  Read-Host "Enter the SQL Azure user id"
$SecurePassword =  Read-Host "Enter the SQL Azure password" -AsSecureString

$SqlPassword = Get-Password($SecurePassword)

.\Create-SampleDatabaseInAzure.ps1 "$SqlServerName.database.windows.net" $SqlUserId $SqlPassword

### Creates Rules:
$startipaddress = Read-Host "Please enter the Start Client IP address for configuring the firewall rules"
$endipaddress = Read-Host "Please enter the End Client IP address for configuring the firewall rules"
$rule = New-SqlAzureFirewallRule -ServerName $SqlServerName -RuleName "MyIpRule" -StartIpAddress $startipaddress -EndIpAddress $endipaddress -SubscriptionId $subId -Certificate $cert -EA stop 

"Creating Firewall rule for $startipaddress and $endipaddress, this will take ~60 seconds..."

Start-Sleep -s 60

"Created Firewall rule for $startipaddress to $endipaddres"

.\Create-SampleDatabaseInAzure.ps1 "$SqlServerName.database.windows.net" $SqlUserId $SqlPassword