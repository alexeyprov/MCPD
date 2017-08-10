$SetupPath = resolve-path($args[0])
cd $SetupPath
. '.\ps-scripts\Create-SampleDatabaseInAzure.ps1'

Write-Host 
Write-Host 
Write-Host "Press any key to continue ..."
$host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown") | out-null