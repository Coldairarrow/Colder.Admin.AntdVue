cd /d %~dp0
set serviceName="Coldairarrow.Api"
set exePath="Coldairarrow.Api.exe"
nssm install %serviceName% %~dp0\%exePath%
::参数设置
::nssm set emqx AppParameters start
nssm start %serviceName%