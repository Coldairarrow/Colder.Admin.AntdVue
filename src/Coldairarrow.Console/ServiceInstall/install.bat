cd /d %~dp0
set serviceName="AService"
set exePath="SocketPassthrough.exe"
nssm install %serviceName% %~dp0\%exePath%
::参数设置
::nssm set emqx AppParameters start
nssm start %serviceName%