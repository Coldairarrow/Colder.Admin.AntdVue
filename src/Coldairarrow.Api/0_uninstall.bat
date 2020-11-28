cd /d %~dp0
set serviceName="Coldairarrow.Api"
nssm stop %serviceName%
nssm remove %serviceName% confirm