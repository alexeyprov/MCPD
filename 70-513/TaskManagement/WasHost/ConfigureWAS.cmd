%windir%\system32\inetsrv\appcmd set site "Default Web Site" /+bindings.[protocol='net.tcp',bindingInformation='808:*']
%windir%\system32\inetsrv\appcmd set app "Default Web Site/WasHost" /enabledProtocols:http,net.tcp
