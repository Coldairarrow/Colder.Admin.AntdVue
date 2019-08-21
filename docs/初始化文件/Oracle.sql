/*
Navicat Oracle Data Transfer
Oracle Client Version : 10.2.0.5.0

Source Server         : .@SYSTEM
Source Server Version : 110200
Source Host           : 127.0.0.1:1521
Source Schema         : COLDER.FX.CORE.ADMINLTE

Target Server Type    : ORACLE
Target Server Version : 110200
File Encoding         : 65001

Date: 2019-07-26 10:17:32
*/


-- ----------------------------
-- Table structure for Base_AppSecret
-- ----------------------------
CREATE TABLE "Base_AppSecret" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"AppId" VARCHAR2(50 CHAR) NULL ,
"AppSecret" VARCHAR2(50 CHAR) NULL ,
"AppName" VARCHAR2(255 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_AppSecret" IS '应用密钥';
COMMENT ON COLUMN "Base_AppSecret"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_AppSecret"."AppId" IS '应用Id';
COMMENT ON COLUMN "Base_AppSecret"."AppSecret" IS '应用密钥';
COMMENT ON COLUMN "Base_AppSecret"."AppName" IS '应用名';

-- ----------------------------
-- Records of Base_AppSecret
-- ----------------------------
INSERT INTO "Base_AppSecret" VALUES ('039e41170bc72-b89139b1-f3f4-430e-aed7-36b193d256dc', 'AppAdmin', 'VjxNekN2G2z0qrjW', '超级权限');

-- ----------------------------
-- Table structure for Base_DatabaseLink
-- ----------------------------
CREATE TABLE "Base_DatabaseLink" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"LinkName" VARCHAR2(50 CHAR) NULL ,
"ConnectionStr" VARCHAR2(1000 CHAR) NULL ,
"DbType" VARCHAR2(50 CHAR) NULL ,
"SortNum" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_DatabaseLink" IS '数据库连接';
COMMENT ON COLUMN "Base_DatabaseLink"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_DatabaseLink"."LinkName" IS '连接名';
COMMENT ON COLUMN "Base_DatabaseLink"."ConnectionStr" IS '连接字符串';
COMMENT ON COLUMN "Base_DatabaseLink"."DbType" IS '数据库类型';
COMMENT ON COLUMN "Base_DatabaseLink"."SortNum" IS '排序编号';

-- ----------------------------
-- Records of Base_DatabaseLink
-- ----------------------------
INSERT INTO "Base_DatabaseLink" VALUES ('1154570454057488384', 'Oracle', 'Data Source=127.0.0.1/XE;User ID=COLDER.FX.CORE.ADMINLTE;Password=123456;Connect Timeout=3', 'Oracle', 'aa');
INSERT INTO "Base_DatabaseLink" VALUES ('039e900bc6bbb-a0070d5c-1fc7-4cf0-a177-e3aebc4633c5', 'SqlServer', 'Data Source=.;Initial Catalog=Colder.Fx.Net.AdminLTE;Integrated Security=True', 'SqlServer', 'aa');

-- ----------------------------
-- Table structure for Base_Department
-- ----------------------------
CREATE TABLE "Base_Department" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"Name" VARCHAR2(50 CHAR) NULL ,
"ParentId" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_Department" IS '部门表';
COMMENT ON COLUMN "Base_Department"."Id" IS '自然主键';
COMMENT ON COLUMN "Base_Department"."Name" IS '部门名';
COMMENT ON COLUMN "Base_Department"."ParentId" IS '上级部门Id';

-- ----------------------------
-- Records of Base_Department
-- ----------------------------
INSERT INTO "Base_Department" VALUES ('1139811378824089600', '宁波分公司', null);
INSERT INTO "Base_Department" VALUES ('1139811435694657536', '鄞州事业部', '1139811378824089600');
INSERT INTO "Base_Department" VALUES ('1139812293048143872', '江北事业部', '1139811378824089600');

-- ----------------------------
-- Table structure for Base_PermissionAppId
-- ----------------------------
CREATE TABLE "Base_PermissionAppId" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"AppId" VARCHAR2(50 CHAR) NULL ,
"PermissionValue" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_PermissionAppId" IS 'AppId权限表';
COMMENT ON COLUMN "Base_PermissionAppId"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_PermissionAppId"."AppId" IS 'AppId';
COMMENT ON COLUMN "Base_PermissionAppId"."PermissionValue" IS '权限值';

-- ----------------------------
-- Records of Base_PermissionAppId
-- ----------------------------

-- ----------------------------
-- Table structure for Base_PermissionRole
-- ----------------------------
CREATE TABLE "Base_PermissionRole" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"RoleId" VARCHAR2(50 CHAR) NULL ,
"PermissionValue" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_PermissionRole" IS '角色权限表';
COMMENT ON COLUMN "Base_PermissionRole"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_PermissionRole"."RoleId" IS '角色主键Id';
COMMENT ON COLUMN "Base_PermissionRole"."PermissionValue" IS '权限值';

-- ----------------------------
-- Records of Base_PermissionRole
-- ----------------------------
INSERT INTO "Base_PermissionRole" VALUES ('1150651336300302336', '1133011663516209152', 'sysuser.search');
INSERT INTO "Base_PermissionRole" VALUES ('1150651336300302337', '1133011663516209152', 'sysuser.manage');
INSERT INTO "Base_PermissionRole" VALUES ('1150651336300302338', '1133011663516209152', 'sysuser.manageSysPermission');
INSERT INTO "Base_PermissionRole" VALUES ('1150651336300302339', '1133011663516209152', 'sysrole.search');
INSERT INTO "Base_PermissionRole" VALUES ('1150651336300302340', '1133011663516209152', 'department.search');
INSERT INTO "Base_PermissionRole" VALUES ('1150651336300302341', '1133011663516209152', 'appsecret.search');
INSERT INTO "Base_PermissionRole" VALUES ('1150651336300302342', '1133011663516209152', 'sysLog.search');
INSERT INTO "Base_PermissionRole" VALUES ('1150666398465396736', '1150666215103008768', 'sysuser.search');
INSERT INTO "Base_PermissionRole" VALUES ('1150666398465396737', '1150666215103008768', 'sysuser.manage');
INSERT INTO "Base_PermissionRole" VALUES ('1150666398465396738', '1150666215103008768', 'sysuser.manageSysPermission');

-- ----------------------------
-- Table structure for Base_PermissionUser
-- ----------------------------
CREATE TABLE "Base_PermissionUser" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"UserId" VARCHAR2(50 CHAR) NULL ,
"PermissionValue" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_PermissionUser" IS '用户权限表';
COMMENT ON COLUMN "Base_PermissionUser"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_PermissionUser"."UserId" IS '用户主键Id';
COMMENT ON COLUMN "Base_PermissionUser"."PermissionValue" IS '权限';

-- ----------------------------
-- Records of Base_PermissionUser
-- ----------------------------
INSERT INTO "Base_PermissionUser" VALUES ('1133345814723301376', '1133345545746780160', 'sysLog.search');

-- ----------------------------
-- Table structure for Base_SysLog
-- ----------------------------
CREATE TABLE "Base_SysLog" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"Level" VARCHAR2(200 CHAR) NULL ,
"LogType" VARCHAR2(50 CHAR) NULL ,
"LogContent" CLOB NULL ,
"OpUserName" VARCHAR2(50 CHAR) NULL ,
"OpTime" DATE NULL ,
"Data" NCLOB NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_SysLog" IS '系统日志表';
COMMENT ON COLUMN "Base_SysLog"."Id" IS '自然主键';
COMMENT ON COLUMN "Base_SysLog"."Level" IS '日志级别';
COMMENT ON COLUMN "Base_SysLog"."LogType" IS '日志类型';
COMMENT ON COLUMN "Base_SysLog"."LogContent" IS '日志内容';
COMMENT ON COLUMN "Base_SysLog"."OpUserName" IS '操作员用户名';
COMMENT ON COLUMN "Base_SysLog"."OpTime" IS '日志记录时间';
COMMENT ON COLUMN "Base_SysLog"."Data" IS '数据备份（转为JSON字符串）';

-- ----------------------------
-- Records of Base_SysLog
-- ----------------------------

-- ----------------------------
-- Table structure for Base_SysRole
-- ----------------------------
CREATE TABLE "Base_SysRole" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"RoleName" NVARCHAR2(50) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_SysRole" IS '系统角色';
COMMENT ON COLUMN "Base_SysRole"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_SysRole"."RoleName" IS '角色名';

-- ----------------------------
-- Records of Base_SysRole
-- ----------------------------
INSERT INTO "Base_SysRole" VALUES ('1133011623854870528', '超级管理员');
INSERT INTO "Base_SysRole" VALUES ('1133011663516209152', '部门管理员');
INSERT INTO "Base_SysRole" VALUES ('1150666215103008768', '编辑部');

-- ----------------------------
-- Table structure for Base_UnitTest
-- ----------------------------
CREATE TABLE "Base_UnitTest" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"UserId" VARCHAR2(50 CHAR) NULL ,
"UserName" VARCHAR2(50 CHAR) NULL ,
"Age" NUMBER(11) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_UnitTest" IS '单元测试表';
COMMENT ON COLUMN "Base_UnitTest"."Id" IS '代理主键';

-- ----------------------------
-- Records of Base_UnitTest
-- ----------------------------

-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
CREATE TABLE "Base_User" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"UserName" VARCHAR2(255 CHAR) NULL ,
"Password" VARCHAR2(255 CHAR) NULL ,
"RealName" VARCHAR2(50 CHAR) NULL ,
"Sex" NUMBER(11) NULL ,
"Birthday" DATE NULL ,
"DepartmentId" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_User" IS '系统，用户表';
COMMENT ON COLUMN "Base_User"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_User"."UserName" IS '用户名';
COMMENT ON COLUMN "Base_User"."Password" IS '密码';
COMMENT ON COLUMN "Base_User"."RealName" IS '真实姓名';
COMMENT ON COLUMN "Base_User"."Sex" IS '性别(1为男，0为女)';
COMMENT ON COLUMN "Base_User"."Birthday" IS '出生日期';
COMMENT ON COLUMN "Base_User"."DepartmentId" IS '所属部门Id';

-- ----------------------------
-- Records of Base_User
-- ----------------------------
INSERT INTO "Base_User" VALUES ('1133345545746780160', '小王', 'e10adc3949ba59abbe56e057f20f883e', 'xiaoming', '1', TO_DATE('2019-07-04 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1139811435694657536');
INSERT INTO "Base_User" VALUES ('Admin', 'Admin', 'e10adc3949ba59abbe56e057f20f883e', '超级管理员', '100', TO_DATE('2017-12-15 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1139811378824089600');

-- ----------------------------
-- Table structure for Base_UserRoleMap
-- ----------------------------
CREATE TABLE "Base_UserRoleMap" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"UserId" VARCHAR2(50 CHAR) NULL ,
"RoleId" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of Base_UserRoleMap
-- ----------------------------
INSERT INTO "Base_UserRoleMap" VALUES ('1139822682855051264', '1133345545746780160', '1133011663516209152');

-- ----------------------------
-- Table structure for Dev_Project
-- ----------------------------
CREATE TABLE "Dev_Project" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"ProjectId" VARCHAR2(50 CHAR) NULL ,
"ProjectName" VARCHAR2(255 CHAR) NULL ,
"ProjectTypeId" VARCHAR2(50 CHAR) NULL ,
"ProjectManagerId" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Dev_Project" IS '项目表';
COMMENT ON COLUMN "Dev_Project"."Id" IS '自然主键';
COMMENT ON COLUMN "Dev_Project"."ProjectId" IS '项目Id';
COMMENT ON COLUMN "Dev_Project"."ProjectName" IS '项目名';
COMMENT ON COLUMN "Dev_Project"."ProjectTypeId" IS '项目类型Id';
COMMENT ON COLUMN "Dev_Project"."ProjectManagerId" IS '项目经理Id';

-- ----------------------------
-- Records of Dev_Project
-- ----------------------------

-- ----------------------------
-- Table structure for Dev_ProjectType
-- ----------------------------
CREATE TABLE "Dev_ProjectType" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"ProjectTypeId" VARCHAR2(50 CHAR) NULL ,
"ProjectTypeName" VARCHAR2(255 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Dev_ProjectType" IS '项目类型表';
COMMENT ON COLUMN "Dev_ProjectType"."Id" IS '自然主键';
COMMENT ON COLUMN "Dev_ProjectType"."ProjectTypeId" IS '项目类型Id';
COMMENT ON COLUMN "Dev_ProjectType"."ProjectTypeName" IS '项目类型名';

-- ----------------------------
-- Records of Dev_ProjectType
-- ----------------------------
INSERT INTO "Dev_ProjectType" VALUES ('1133722179070988288', 'sadsa', 'sdsadasdsa');

-- ----------------------------
-- Indexes structure for table Base_AppSecret
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE "Base_AppSecret" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE "Base_AppSecret" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_DatabaseLink
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_DatabaseLink
-- ----------------------------
ALTER TABLE "Base_DatabaseLink" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_DatabaseLink
-- ----------------------------
ALTER TABLE "Base_DatabaseLink" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_Department
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_Department
-- ----------------------------
ALTER TABLE "Base_Department" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_Department
-- ----------------------------
ALTER TABLE "Base_Department" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_PermissionAppId
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_PermissionAppId
-- ----------------------------
ALTER TABLE "Base_PermissionAppId" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_PermissionAppId
-- ----------------------------
ALTER TABLE "Base_PermissionAppId" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_PermissionRole
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_PermissionRole
-- ----------------------------
ALTER TABLE "Base_PermissionRole" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_PermissionRole
-- ----------------------------
ALTER TABLE "Base_PermissionRole" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_PermissionUser
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_PermissionUser
-- ----------------------------
ALTER TABLE "Base_PermissionUser" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_PermissionUser
-- ----------------------------
ALTER TABLE "Base_PermissionUser" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_SysLog
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_SysLog
-- ----------------------------
ALTER TABLE "Base_SysLog" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_SysLog
-- ----------------------------
ALTER TABLE "Base_SysLog" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_SysRole
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_SysRole
-- ----------------------------
ALTER TABLE "Base_SysRole" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_SysRole
-- ----------------------------
ALTER TABLE "Base_SysRole" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_UnitTest
-- ----------------------------

-- ----------------------------
-- Uniques structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE "Base_UnitTest" ADD UNIQUE ("UserId");

-- ----------------------------
-- Checks structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE "Base_UnitTest" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE "Base_UnitTest" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_User
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_User
-- ----------------------------
ALTER TABLE "Base_User" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_User
-- ----------------------------
ALTER TABLE "Base_User" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_UserRoleMap
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_UserRoleMap
-- ----------------------------
ALTER TABLE "Base_UserRoleMap" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_UserRoleMap
-- ----------------------------
ALTER TABLE "Base_UserRoleMap" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Dev_Project
-- ----------------------------

-- ----------------------------
-- Checks structure for table Dev_Project
-- ----------------------------
ALTER TABLE "Dev_Project" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Dev_Project
-- ----------------------------
ALTER TABLE "Dev_Project" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Dev_ProjectType
-- ----------------------------

-- ----------------------------
-- Checks structure for table Dev_ProjectType
-- ----------------------------
ALTER TABLE "Dev_ProjectType" ADD CHECK ("Id" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Dev_ProjectType
-- ----------------------------
ALTER TABLE "Dev_ProjectType" ADD PRIMARY KEY ("Id");
