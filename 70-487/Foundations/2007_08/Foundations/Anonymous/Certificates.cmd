certmgr -del -r localmachine -s TrustedPeople -c -n MyServiceCert
certmgr -del -r localmachine -s My -c -n MyServiceCert

makecert.exe  -sr LocalMachine -ss MY  -pe -sky exchange -n "CN=MyServiceCert" MyServiceCert.cer
certmgr.exe -add -r localmachine -s My -c -n MyServiceCert -r localmachine -s TrustedPeople



