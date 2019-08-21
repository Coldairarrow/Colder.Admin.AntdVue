/*
Navicat PGSQL Data Transfer

Source Server         : .PostgreSQL
Source Server Version : 90602
Source Host           : localhost:5432
Source Database       : Colder.Fx.Net.AdminLTE
Source Schema         : public

Target Server Type    : PGSQL
Target Server Version : 90500
File Encoding         : 65001

Date: 2019-06-15 21:01:11
*/


-- ----------------------------
-- Table structure for Base_AppSecret
-- ----------------------------
DROP TABLE IF EXISTS "Base_AppSecret";
CREATE TABLE "Base_AppSecret" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"AppId" varchar(50) COLLATE "default",
"AppSecret" varchar(50) COLLATE "default",
"AppName" varchar(255) COLLATE "default"
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "Base_AppSecret" IS '应用密钥';
COMMENT ON COLUMN "Base_AppSecret"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_AppSecret"."AppId" IS '应用Id';
COMMENT ON COLUMN "Base_AppSecret"."AppSecret" IS '应用密钥';
COMMENT ON COLUMN "Base_AppSecret"."AppName" IS '应用名';

-- ----------------------------
-- Records of Base_AppSecret
-- ----------------------------
BEGIN;
INSERT INTO "Base_AppSecret" VALUES ('039e41170bc72-b89139b1-f3f4-430e-aed7-36b193d256dc', 'AppAdmin', 'VjxNekN2G2z0qrjW', '超级权限');
COMMIT;

-- ----------------------------
-- Table structure for Base_DatabaseLink
-- ----------------------------
DROP TABLE IF EXISTS "Base_DatabaseLink";
CREATE TABLE "Base_DatabaseLink" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"LinkName" varchar(50) COLLATE "default",
"ConnectionStr" varchar(1000) COLLATE "default",
"DbType" varchar(50) COLLATE "default",
"SortNum" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

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
BEGIN;
INSERT INTO "Base_DatabaseLink" VALUES ('039e900bc6bbb-a0070d5c-1fc7-4cf0-a177-e3aebc4633c5', 'SqlServer', 'Data Source=.;Initial Catalog=Colder.Fx.Net.AdminLTE;Integrated Security=True', 'SqlServer', 'aa');
COMMIT;

-- ----------------------------
-- Table structure for Base_Department
-- ----------------------------
DROP TABLE IF EXISTS "Base_Department";
CREATE TABLE "Base_Department" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"Name" varchar(50) COLLATE "default",
"ParentId" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "Base_Department" IS '部门表';
COMMENT ON COLUMN "Base_Department"."Id" IS '自然主键';
COMMENT ON COLUMN "Base_Department"."Name" IS '部门名';
COMMENT ON COLUMN "Base_Department"."ParentId" IS '上级部门Id';

-- ----------------------------
-- Records of Base_Department
-- ----------------------------
BEGIN;
INSERT INTO "Base_Department" VALUES ('1139811378824089600', '宁波分公司', null);
INSERT INTO "Base_Department" VALUES ('1139811435694657536', '鄞州事业部', '1139811378824089600');
INSERT INTO "Base_Department" VALUES ('1139812293048143872', '江北事业部', '1139811378824089600');
COMMIT;

-- ----------------------------
-- Table structure for Base_PermissionAppId
-- ----------------------------
DROP TABLE IF EXISTS "Base_PermissionAppId";
CREATE TABLE "Base_PermissionAppId" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"AppId" varchar(50) COLLATE "default",
"PermissionValue" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "Base_PermissionAppId" IS 'AppId权限表';
COMMENT ON COLUMN "Base_PermissionAppId"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_PermissionAppId"."AppId" IS 'AppId';
COMMENT ON COLUMN "Base_PermissionAppId"."PermissionValue" IS '权限值';

-- ----------------------------
-- Records of Base_PermissionAppId
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_PermissionRole
-- ----------------------------
DROP TABLE IF EXISTS "Base_PermissionRole";
CREATE TABLE "Base_PermissionRole" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"RoleId" varchar(50) COLLATE "default",
"PermissionValue" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "Base_PermissionRole" IS '角色权限表';
COMMENT ON COLUMN "Base_PermissionRole"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_PermissionRole"."RoleId" IS '角色主键Id';
COMMENT ON COLUMN "Base_PermissionRole"."PermissionValue" IS '权限值';

-- ----------------------------
-- Records of Base_PermissionRole
-- ----------------------------
BEGIN;
INSERT INTO "Base_PermissionRole" VALUES ('1139819691020259328', '1133011663516209152', 'sysuser.search');
INSERT INTO "Base_PermissionRole" VALUES ('1139819691020259329', '1133011663516209152', 'sysrole.search');
INSERT INTO "Base_PermissionRole" VALUES ('1139819691020259330', '1133011663516209152', 'department.search');
INSERT INTO "Base_PermissionRole" VALUES ('1139819691020259331', '1133011663516209152', 'appsecret.search');
INSERT INTO "Base_PermissionRole" VALUES ('1139819691020259332', '1133011663516209152', 'sysLog.search');
COMMIT;

-- ----------------------------
-- Table structure for Base_PermissionUser
-- ----------------------------
DROP TABLE IF EXISTS "Base_PermissionUser";
CREATE TABLE "Base_PermissionUser" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"UserId" varchar(50) COLLATE "default",
"PermissionValue" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "Base_PermissionUser" IS '用户权限表';
COMMENT ON COLUMN "Base_PermissionUser"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_PermissionUser"."UserId" IS '用户主键Id';
COMMENT ON COLUMN "Base_PermissionUser"."PermissionValue" IS '权限';

-- ----------------------------
-- Records of Base_PermissionUser
-- ----------------------------
BEGIN;
INSERT INTO "Base_PermissionUser" VALUES ('1133345814723301376', '1133345545746780160', 'sysLog.search');
COMMIT;

-- ----------------------------
-- Table structure for Base_SysLog
-- ----------------------------
DROP TABLE IF EXISTS "Base_SysLog";
CREATE TABLE "Base_SysLog" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"LogType" varchar(255) COLLATE "default",
"LogContent" text COLLATE "default",
"OpUserName" varchar(255) COLLATE "default",
"OpTime" timestamp(0),
"Data" text COLLATE "default",
"Level" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "Base_SysLog" IS '系统日志表';
COMMENT ON COLUMN "Base_SysLog"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_SysLog"."LogType" IS '日志类型';
COMMENT ON COLUMN "Base_SysLog"."LogContent" IS '日志内容';
COMMENT ON COLUMN "Base_SysLog"."OpUserName" IS '操作员用户名';
COMMENT ON COLUMN "Base_SysLog"."OpTime" IS '日志记录时间';
COMMENT ON COLUMN "Base_SysLog"."Data" IS '数据备份';
COMMENT ON COLUMN "Base_SysLog"."Level" IS '日志级别';

-- ----------------------------
-- Records of Base_SysLog
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_SysRole
-- ----------------------------
DROP TABLE IF EXISTS "Base_SysRole";
CREATE TABLE "Base_SysRole" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"RoleName" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "Base_SysRole" IS '系统角色';
COMMENT ON COLUMN "Base_SysRole"."Id" IS '代理主键';
COMMENT ON COLUMN "Base_SysRole"."RoleName" IS '角色名';

-- ----------------------------
-- Records of Base_SysRole
-- ----------------------------
BEGIN;
INSERT INTO "Base_SysRole" VALUES ('1133011623854870528', '超级管理员');
INSERT INTO "Base_SysRole" VALUES ('1133011663516209152', '部门管理员');
COMMIT;

-- ----------------------------
-- Table structure for Base_UnitTest
-- ----------------------------
DROP TABLE IF EXISTS "Base_UnitTest";
CREATE TABLE "Base_UnitTest" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"UserId" varchar(50) COLLATE "default",
"UserName" varchar(50) COLLATE "default",
"Age" int4
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "Base_UnitTest" IS '单元测试表';
COMMENT ON COLUMN "Base_UnitTest"."Id" IS '代理主键';

-- ----------------------------
-- Records of Base_UnitTest
-- ----------------------------
BEGIN;
INSERT INTO "Base_UnitTest" VALUES ('10', null, null, null);
INSERT INTO "Base_UnitTest" VALUES ('1139855817357529088', '1139855817357529089', '超级管理员', '22');
INSERT INTO "Base_UnitTest" VALUES ('6a1230b5-43fa-4d4c-8c3e-59f8e10d89a1', 'Admin', '超级管理员', '22');
COMMIT;

-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
DROP TABLE IF EXISTS "Base_User";
CREATE TABLE "Base_User" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"UserName" varchar(255) COLLATE "default",
"Password" varchar(255) COLLATE "default",
"RealName" varchar(50) COLLATE "default",
"Sex" int4,
"Birthday" date,
"DepartmentId" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

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
BEGIN;
INSERT INTO "Base_User" VALUES ('1133345545746780160', 'xiaoming', 'e10adc3949ba59abbe56e057f20f883e', 'xiaoming', '1', null, '1139811435694657536');
INSERT INTO "Base_User" VALUES ('Admin', 'Admin', 'e10adc3949ba59abbe56e057f20f883e', '超级管理员', '1', '2017-12-15', '1139811378824089600');
COMMIT;

-- ----------------------------
-- Table structure for Base_UserRoleMap
-- ----------------------------
DROP TABLE IF EXISTS "Base_UserRoleMap";
CREATE TABLE "Base_UserRoleMap" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"UserId" varchar(50) COLLATE "default",
"RoleId" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

;

-- ----------------------------
-- Records of Base_UserRoleMap
-- ----------------------------
BEGIN;
INSERT INTO "Base_UserRoleMap" VALUES ('1139822682855051264', '1133345545746780160', '1133011663516209152');
COMMIT;

-- ----------------------------
-- Table structure for Dev_Project
-- ----------------------------
DROP TABLE IF EXISTS "Dev_Project";
CREATE TABLE "Dev_Project" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"ProjectId" varchar(50) COLLATE "default" NOT NULL,
"ProjectName" varchar(255) COLLATE "default" NOT NULL,
"ProjectTypeId" varchar(50) COLLATE "default",
"ProjectManagerId" varchar(50) COLLATE "default"
)
WITH (OIDS=FALSE)

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
BEGIN;
INSERT INTO "Dev_Project" VALUES ('039e943dea9f4-30e0e19b-828e-4938-98b6-da3941987925', 'asdsa', '厉害了', '5645646', 'zxzx');
COMMIT;

-- ----------------------------
-- Table structure for Dev_ProjectType
-- ----------------------------
DROP TABLE IF EXISTS "Dev_ProjectType";
CREATE TABLE "Dev_ProjectType" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"ProjectTypeId" varchar(50) COLLATE "default",
"ProjectTypeName" varchar(255) COLLATE "default"
)
WITH (OIDS=FALSE)

;
COMMENT ON TABLE "Dev_ProjectType" IS '项目类型表';
COMMENT ON COLUMN "Dev_ProjectType"."Id" IS '自然主键';
COMMENT ON COLUMN "Dev_ProjectType"."ProjectTypeId" IS '项目类型Id';
COMMENT ON COLUMN "Dev_ProjectType"."ProjectTypeName" IS '项目类型名';

-- ----------------------------
-- Records of Dev_ProjectType
-- ----------------------------
BEGIN;
INSERT INTO "Dev_ProjectType" VALUES ('1133722179070988288', 'sadsa', 'sdsadasdsa');
COMMIT;

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE "Base_AppSecret" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_DatabaseLink
-- ----------------------------
ALTER TABLE "Base_DatabaseLink" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_Department
-- ----------------------------
ALTER TABLE "Base_Department" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_PermissionAppId
-- ----------------------------
ALTER TABLE "Base_PermissionAppId" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_PermissionRole
-- ----------------------------
ALTER TABLE "Base_PermissionRole" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_PermissionUser
-- ----------------------------
ALTER TABLE "Base_PermissionUser" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_SysLog
-- ----------------------------
ALTER TABLE "Base_SysLog" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_SysRole
-- ----------------------------
ALTER TABLE "Base_SysRole" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Uniques structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE "Base_UnitTest" ADD UNIQUE ("UserId");

-- ----------------------------
-- Primary Key structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE "Base_UnitTest" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_User
-- ----------------------------
ALTER TABLE "Base_User" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_UserRoleMap
-- ----------------------------
ALTER TABLE "Base_UserRoleMap" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Dev_Project
-- ----------------------------
ALTER TABLE "Dev_Project" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Dev_ProjectType
-- ----------------------------
ALTER TABLE "Dev_ProjectType" ADD PRIMARY KEY ("Id");
