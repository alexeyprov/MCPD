$servicename = $args[0]
$certToDeploy = $args[1]
$certPassword = $args[2]
$mgmtcertthumbprint = $args[3]
$cert = Get-Item cert:\CurrentUser\My\$mgmtcertthumbprint
$sub = $args[4]
$algo = $args[5]
$certThumb = $args[6]
$env = Get-Item Env:\ProgramFiles*x86*

if ($env -ne $null)
{
	$PFilesPath = $env.value
}
else
{
	$env = Get-Item Env:\ProgramFiles
	$PFilesPath = $env.value
}

$ImportModulePath = Join-Path $PFilesPath "Microsoft SDKs\Windows Azure\PowerShell\Microsoft.WindowsAzure.Management.psd1"
Import-Module $ImportModulePath

Set-AzureSubscription -SubscriptionName Adatum -Certificate $cert -SubscriptionId $sub

try
{
  Remove-AzureCertificate -ServiceName $servicename -ThumbprintAlgorithm $algo -Thumbprint $certThumb
}
catch {}

Add-AzureCertificate -ServiceName $servicename -CertToDeploy $certToDeploy -Password $certPassword
