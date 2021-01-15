FROM docker pull mcr.microsoft.com/dotnet/aspnet:5.0
RUN rm -f /etc/localtime && ln -sv /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo "Asia/Shanghai" > /etc/timezone
WORKDIR /app
COPY . .
EXPOSE 5000
ENTRYPOINT ["dotnet","Coldairarrow.Api.dll"]