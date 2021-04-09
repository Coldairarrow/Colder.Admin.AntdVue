#使用阿里云镜像，微软镜像太慢
#FROM docker pull mcr.microsoft.com/dotnet/aspnet:5.0.2
FROM registry.cn-hangzhou.aliyuncs.com/colder-public/aspnet:5.0.2
RUN rm -f /etc/localtime && ln -sv /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo "Asia/Shanghai" > /etc/timezone
WORKDIR /app
COPY . .
EXPOSE 5000
ENTRYPOINT ["dotnet","Coldairarrow.Api.dll"]