### you need to set execution policy to Unrestricted before running this script
### Set-ExecutionPolicy Unrestricted

###########################################################################################################
###########################################################################################################
### You need to replace 
### My_Namespace, My_ManagementAccountName, My_Password, My_Defaultkey,My_CacheAuthticationToken and My_x509certthumbprint 
### with the value you get in your windows azure portal 

$namespace                  ="My_Namespace"
$managementaccountname      ="My_ManagementAccountName"
$password                   ="My_Password"
$defaultkey                 ="My_Defaultkey"
$cacheauthenticationtoken   ="My_CacheAuthticationToken"
$x509certthumbprint         ="My_x509certthumbprint"

###########################################################################################################
###########################################################################################################
Function Replace-Text($SubFolder, $FileFilter, $Oldtext, $Newtext)
{
	Get-ChildItem $SubFolder $FileFilter -recurse |
	Foreach-Object{
		$file = $_
		$oldecontent=$file | Get-Content
		$newcontent = $oldecontent -replace $OldText, $NewText 
		[IO.File]::WriteAllText($file.FullName, ($newcontent -join "`r`n"))
	}
}

Replace-Text Sourcecode *.config '{your_service_bus_namespace}' $namespace
Replace-Text Sourcecode *.config '{your_acs_namespace}'         $namespace
Replace-Text Sourcecode *.config '{your_acs_username}'          $managementaccountname
Replace-Text Sourcecode *.config '{your_acs_password}'          $password
Replace-Text Sourcecode *.config '{your_service_bus_key}'       $defaultkey
Replace-Text Sourcecode *.config '{your_access_control_x509_thumbprint}'           $x509certthumbprint

Replace-Text Sourcecode *.cscfg  '{your_service_bus_namespace}' $namespace
Replace-Text Sourcecode *.cscfg  '{your_acs_username}'          $managementaccountname
Replace-Text Sourcecode *.cscfg  '{your_acs_namespace}'         $namespace
Replace-Text Sourcecode *.cscfg  '{your_cache_namespace}'		$namespace
Replace-Text Sourcecode *.cscfg  '{your_acs_password}'          $password
Replace-Text Sourcecode *.cscfg  '{your_cache_auth_info}'		$cacheauthenticationtoken

###########################################################################################################
###########################################################################################################
### Replaced Predefined keys ###

Function Generate-Key()
{
	$mySymetricKeyProvider = new-Object System.Security.Cryptography.RijndaelManaged
	return [Convert]::ToBase64String($mySymetricKeyProvider.Key)
}

###########################################################################################################
Function Generate-RandomPassword()
{
	$SmallLetters = [Char[]]'abcdefghijklmnopqrstuvwxyz'
	$CapLetters = [Char[]]'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'
	$Numbers = [Char[]]'0123456789'
	$Specials = [Char[]]'$@!'
	$Small=-join(1..3 | Foreach-Object {Get-Random $SmallLetters -count 1})
	$Cap =-join(1..3 | Foreach-Object {Get-Random $CapLetters -count 1})
	$Num =-join(1..3 | Foreach-Object {Get-Random $Numbers -count 1})
	$Spe =-join(1..2 | Foreach-Object {Get-Random $Specials -count 1})
	$password = $Small + $Cap + $Num + $Spe
	return $password
}

$symmetrickeyforcontoso		  = Generate-Key
$symmetrickeyforfabrikam	  = Generate-Key
$neworderjobkeyvalue		  = Generate-Key
$statusupdatejobkeyvalue	  = Generate-Key
$contosokeyvalue			  = Generate-Key
$auditloglistenerkeyvalue	  = Generate-Key
$fabrikamkeyvalue			  = Generate-Key
$externaldataanalyzerkeyvalue = Generate-Key
$headofficekeyvalue			  = Generate-Key
$yourmessagepassword		  = Generate-RandomPassword

Replace-Text Sourcecode *.config '{symmetrickeyforcontoso_value}'	$symmetrickeyforcontoso
Replace-Text Sourcecode *.config '{symmetrickeyforfabrikam_value}'	$symmetrickeyforfabrikam
Replace-Text Sourcecode *.config '{neworderjob_key_value}'			$neworderjobkeyvalue
Replace-Text Sourcecode *.config '{statusupdatejob_key_value}'		$statusupdatejobkeyvalue
Replace-Text Sourcecode *.config '{contoso_key_value}'				$contosokeyvalue
Replace-Text Sourcecode *.config '{auditloglistener_key_value}'		$auditloglistenerkeyvalue
Replace-Text Sourcecode *.config '{fabrikam_key_value}'				$fabrikamkeyvalue
Replace-Text Sourcecode *.config '{externaldataanalyzer_key_value}' $externaldataanalyzerkeyvalue
Replace-Text Sourcecode *.config '{headoffice_key_value}'			$headofficekeyvalue
Replace-Text Sourcecode *.config '{your_message_password}'			$yourmessagepassword

Replace-Text Sourcecode *.cscfg '{symmetrickeyforcontoso_value}'	$symmetrickeyforcontoso
Replace-Text Sourcecode *.cscfg '{symmetrickeyforfabrikam_value}'	$symmetrickeyforfabrikam
Replace-Text Sourcecode *.cscfg '{neworderjob_key_value}'			$neworderjobkeyvalue
Replace-Text Sourcecode *.cscfg '{statusupdatejob_key_value}'		$statusupdatejobkeyvalue
Replace-Text Sourcecode *.cscfg '{contoso_key_value}'				$contosokeyvalue
Replace-Text Sourcecode *.cscfg '{auditloglistener_key_value}'		$auditloglistenerkeyvalue
Replace-Text Sourcecode *.cscfg '{fabrikam_key_value}'				$fabrikamkeyvalue
Replace-Text Sourcecode *.cscfg '{externaldataanalyzer_key_value}'	$externaldataanalyzerkeyvalue
Replace-Text Sourcecode *.cscfg '{headoffice_key_value}'			$headofficekeyvalue
Replace-Text Sourcecode *.cscfg '{your_message_password}'			$yourmessagepassword

###########################################################################################################
###########################################################################################################
"Configuration finished." 