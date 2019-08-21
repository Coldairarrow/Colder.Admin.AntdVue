#!/bin/bash
#zip -r /web/garbage_classification/bak/pcadmin`date +%Y%m%d`bak.zip ./ #备份
#unzip -o Web.zip #解压
docker stop coldairarrow.web && docker rm coldairarrow.web && docker rmi coldairarrow.web #停止并删除容器并删除原镜像
docker build -t coldairarrow.web . #构建镜像
docker run --name=coldairarrow.web -v /web/dockerdemo/upload:/app/wwwroot/Upload --privileged=true -p 5000:5000 -d --restart=always coldairarrow.web  #
docker ps #
