$buildPath = $args[0]
$packagename = $args[1]
$serviceconfig = $args[2]
$servicename = $args[3]
$mgmtcertthumbprint = $args[4]
$cert = Get-Item cert:\CurrentUser\My\$mgmtcertthumbprint
$sub = $args[5]
$slot = $args[6]
$storage = $args[7]
$package = join-path $buildPath $packageName
$config = join-path $buildPath $serviceconfig
$a = Get-Date
$buildLabel = $a.ToShortDateString() + "-" + $a.ToShortTimeString()


#Important!  When using file based packages (non-http paths), the cmdlets will attempt to upload
#the package to blob storage for you automatically.  If you do not specifiy a -StorageServiceName
#option, it will attempt to upload a storage account with the same name as $servicename.  If that
#account does not exist, it will fail.  This only applies to file-based package paths.
$env = Get-Item Env:\ProgramFiles*x86*  

if ($env -ne $null) {
	$PFilesPath = $env.value
} else {
	$env = Get-Item Env:\ProgramFiles
	$PFilesPath = $env.value
}

$ImportModulePath = Join-Path $PFilesPath "Microsoft SDKs\Windows Azure\PowerShell\Microsoft.WindowsAzure.Management.psd1"
Import-Module $ImportModulePath

Set-AzureSubscription -SubscriptionName Adatum -Certificate $cert -SubscriptionId $sub -CurrentStorageAccount $storage

$hostedService = Get-AzureService $servicename | Get-AzureDeployment -Slot $slot

if ($hostedService.Status -ne $null)
{
    $hostedService |
	  Set-AzureDeployment –Status –NewStatus "Suspended"
    $hostedService | 
      Remove-AzureDeployment -Force
}
Get-AzureService -ServiceName $servicename |
    New-AzureDeployment -Slot $slot -Package $package -Configuration $config -Label $buildLabel 

Get-AzureService -ServiceName $servicename | 
    Get-AzureDeployment -Slot $slot | 
    Set-AzureDeployment –Status –NewStatus "Running" 