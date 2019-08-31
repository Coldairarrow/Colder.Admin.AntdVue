FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.5-alpine3.9
RUN echo "http://mirrors.aliyun.com/alpine/v3.8/main/" > /etc/apk/repositories \
	&& apk add --no-cache  icu-libs \
	&& apk add --no-cache --repository http://mirrors.aliyun.com/alpine/edge/testing/ libgdiplus \
	&& apk add --no-cache tzdata \
	&& cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime \
    && echo "Asia/Shanghai" > /etc/timezone \
    && apk del tzdata
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY . .
EXPOSE 5000
ENTRYPOINT ["dotnet","Coldairarrow.Web.dll"]