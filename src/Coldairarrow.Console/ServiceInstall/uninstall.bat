cd /d %~dp0
set serviceName="AService"
nssm stop %serviceName%
nssm remove %serviceName% confirm