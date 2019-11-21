# 设置基础镜像 
FROM docker.io/nginx:1.17.5-alpine
# 将dist文件中的内容复制到 /usr/share/nginx/html/ 这个目录下面
COPY . /usr/share/nginx/html/
COPY default.conf /etc/nginx/conf.d/