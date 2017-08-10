certmgr -del -r LocalMachine -s My -c -n MyServiceCert

makecert.exe  -sr LocalMachine -ss MY  -pe -sky exchange -n "CN=MyServiceCert" MyServiceCert.cer

