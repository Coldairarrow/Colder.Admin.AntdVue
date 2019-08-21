cd /d %~dp0
set serviceName="AService"
set exePath="SocketPassthrough.exe"
nssm install %serviceName% %~dp0\%exePath%
nssm start %serviceName%