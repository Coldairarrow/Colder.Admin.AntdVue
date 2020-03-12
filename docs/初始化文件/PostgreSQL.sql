/*
Navicat PGSQL Data Transfer

Source Server         : .PostgreSQL
Source Server Version : 90602
Source Host           : localhost:5432
Source Database       : Colder.Admin.AntdVue
Source Schema         : public

Target Server Type    : PGSQL
Target Server Version : 90602
File Encoding         : 65001

Date: 2020-01-15 20:38:28
*/


-- ----------------------------
-- Table structure for Base_Action
-- ----------------------------
CREATE TABLE "Base_Action" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"ParentId" varchar(50) COLLATE "default",
"Type" int4 DEFAULT 0 NOT NULL,
"Name" varchar(50) COLLATE "default",
"Url" varchar(500) COLLATE "default",
"Value" varchar(50) COLLATE "default",
"NeedAction" bool DEFAULT false NOT NULL,
"Icon" varchar(50) COLLATE "default",
"Sort" int4 DEFAULT 0 NOT NULL
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_Action
-- ----------------------------
BEGIN;
INSERT INTO "Base_Action" VALUES ('1178957405992521728', '2019-10-01 16:58:44', null, 'f', null, '0', '系统管理', '', null, 't', 'setting', '1');
INSERT INTO "Base_Action" VALUES ('1178957553778823168', '2019-10-01 16:59:19', null, 'f', '1178957405992521728', '1', '权限管理', '/Base_Manage/Base_Action/List', null, 't', null, '20');
INSERT INTO "Base_Action" VALUES ('1179018395304071168', '2019-10-01 21:01:05', null, 'f', '1178957405992521728', '1', '密钥管理', '/Base_Manage/Base_AppSecret/List', null, 't', null, '15');
INSERT INTO "Base_Action" VALUES ('1182652266117599232', '2019-10-11 21:40:47', null, 'f', '1178957405992521728', '1', '用户管理', '/Base_Manage/Base_User/List', null, 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1182652367447789568', '2019-10-11 21:41:11', null, 'f', '1178957405992521728', '1', '角色管理', '/Base_Manage/Base_Role/List', null, 't', null, '5');
INSERT INTO "Base_Action" VALUES ('1182652433302556672', '2019-10-11 21:41:27', null, 'f', '1178957405992521728', '1', '部门管理', '/Base_Manage/Base_Department/List', null, 't', null, '10');
INSERT INTO "Base_Action" VALUES ('1182652599069839360', '2019-10-11 21:42:06', null, 'f', '1178957405992521728', '1', '系统日志', '/Base_Manage/Base_Log/List', null, 't', null, '25');
INSERT INTO "Base_Action" VALUES ('1188800845714558976', '2019-10-28 20:53:03.127', null, 't', '1182652266117599232', '2', '增', null, 'Base_User.Add', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188800845714558977', '2019-10-28 20:53:03.127', null, 't', '1182652266117599232', '2', '改', null, 'Base_User.Edit', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188800845714558978', '2019-10-28 20:53:03.127', null, 't', '1182652266117599232', '2', '删', null, 'Base_User.Delete', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801057778569216', '2019-10-28 20:53:53.687', null, 'f', '1182652367447789568', '2', '增', null, 'Base_Role.Add', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801057778569217', '2019-10-28 20:53:53.687', null, 'f', '1182652367447789568', '2', '改', null, 'Base_Role.Edit', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801057778569218', '2019-10-28 20:53:53.687', null, 'f', '1182652367447789568', '2', '删', null, 'Base_Role.Delete', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801109783744512', '2019-10-28 20:54:06.087', null, 'f', '1182652433302556672', '2', '增', null, 'Base_Department.Add', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801109783744513', '2019-10-28 20:54:06.087', null, 'f', '1182652433302556672', '2', '改', null, 'Base_Department.Edit', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801109783744514', '2019-10-28 20:54:06.087', null, 'f', '1182652433302556672', '2', '删', null, 'Base_Department.Delete', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801273885888512', '2019-10-28 20:54:45.213', null, 'f', '1179018395304071168', '2', '增', null, 'Base_AppSecret.Add', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801273885888513', '2019-10-28 20:54:45.213', null, 'f', '1179018395304071168', '2', '改', null, 'Base_AppSecret.Edit', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801273885888514', '2019-10-28 20:54:45.213', null, 'f', '1179018395304071168', '2', '删', null, 'Base_AppSecret.Delete', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801341661646848', '2019-10-28 20:55:01.37', null, 'f', '1178957553778823168', '2', '增', null, 'Base_Action.Add', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801341661646849', '2019-10-28 20:55:01.37', null, 'f', '1178957553778823168', '2', '改', null, 'Base_Action.Edit', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801341661646850', '2019-10-28 20:55:01.37', null, 'f', '1178957553778823168', '2', '删', null, 'Base_Action.Delete', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1193158266167758848', '2019-11-09 21:27:53', 'Admin', 'f', null, '0', '首页', null, null, 't', 'home', '0');
INSERT INTO "Base_Action" VALUES ('1193158630615027712', '2019-11-09 21:29:20.013', 'Admin', 'f', '1193158266167758848', '1', '框架介绍', '/Home/Introduce', null, 'f', null, '0');
INSERT INTO "Base_Action" VALUES ('1193158780011941888', '2019-11-09 21:29:55.63', 'Admin', 'f', '1193158266167758848', '1', '运营统计', '/Home/Statis', null, 'f', null, '0');
INSERT INTO "Base_Action" VALUES ('1193527101521661952', '2019-11-10 21:53:30.32', null, 'f', '1182652266117599232', '2', '增', null, 'Base_User.Add', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1193527101521661953', '2019-11-10 21:53:30.32', null, 'f', '1182652266117599232', '2', '改', null, 'Base_User.Edit', 't', null, '0');
INSERT INTO "Base_Action" VALUES ('1193527101521661954', '2019-11-10 21:53:30.32', null, 'f', '1182652266117599232', '2', '删', null, 'Base_User.Delete', 't', null, '0');
COMMIT;

-- ----------------------------
-- Table structure for Base_AppSecret
-- ----------------------------
CREATE TABLE "Base_AppSecret" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"AppId" varchar(50) COLLATE "default",
"AppSecret" varchar(50) COLLATE "default",
"AppName" varchar(50) COLLATE "default"
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_AppSecret
-- ----------------------------
BEGIN;
INSERT INTO "Base_AppSecret" VALUES ('1172497995938271232', '2019-09-13 21:11:20', 'Admin', 'f', 'PcAdmin', 'wtMaiTRPTT3hrf5e', '后台AppId');
INSERT INTO "Base_AppSecret" VALUES ('1173937877642383360', '2019-09-17 20:32:55', 'Admin', 'f', 'AppAdmin', 'IVh9LLSVFcoQPQ5K', 'APP密钥');
COMMIT;

-- ----------------------------
-- Table structure for Base_BuildTest
-- ----------------------------
CREATE TABLE "Base_BuildTest" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"Column1" varchar(50) COLLATE "default",
"Column2" varchar(50) COLLATE "default",
"Column3" varchar(50) COLLATE "default",
"Column4" varchar(50) COLLATE "default",
"Column5" varchar(50) COLLATE "default"
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_BuildTest
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_DbLink
-- ----------------------------
CREATE TABLE "Base_DbLink" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"LinkName" varchar(50) COLLATE "default",
"ConnectionStr" varchar(500) COLLATE "default",
"DbType" varchar(50) COLLATE "default"
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_DbLink
-- ----------------------------
BEGIN;
INSERT INTO "Base_DbLink" VALUES ('1183373232498020352', '2019-10-13 21:25:39', 'Admin', 'f', 'BaseDb', 'Data Source=.;Initial Catalog=Colder.Admin.AntdVue;Integrated Security=True', 'SqlServer');
COMMIT;

-- ----------------------------
-- Table structure for Base_Department
-- ----------------------------
CREATE TABLE "Base_Department" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"Name" varchar(50) COLLATE "default",
"ParentId" varchar(50) COLLATE "default"
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_Department
-- ----------------------------
BEGIN;
INSERT INTO "Base_Department" VALUES ('1181175685528424448', '2019-10-07 19:53:23', null, 'f', '宁波分公司', null);
INSERT INTO "Base_Department" VALUES ('1181175803631636480', '2019-10-07 19:53:51.427', null, 'f', '鄞州事业部', '1181175685528424448');
INSERT INTO "Base_Department" VALUES ('1181175865409540096', '2019-10-07 19:54:06', null, 'f', '海曙事业部', '1181175685528424448');
COMMIT;

-- ----------------------------
-- Table structure for Base_Log
-- ----------------------------
CREATE TABLE "Base_Log" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"CreatorRealName" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"Level" varchar(200) COLLATE "default",
"LogType" varchar(50) COLLATE "default",
"LogContent" text COLLATE "default",
"Data" text COLLATE "default"
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_Log
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_Role
-- ----------------------------
CREATE TABLE "Base_Role" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"RoleName" varchar(50) COLLATE "default"
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_Role
-- ----------------------------
BEGIN;
INSERT INTO "Base_Role" VALUES ('1180486275199668224', '2019-10-05 22:13:55', null, 'f', '超级管理员');
INSERT INTO "Base_Role" VALUES ('1180819481383931904', '2019-10-06 20:17:57', null, 'f', '部门管理员');
COMMIT;

-- ----------------------------
-- Table structure for Base_RoleAction
-- ----------------------------
CREATE TABLE "Base_RoleAction" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"RoleId" varchar(50) COLLATE "default",
"ActionId" varchar(50) COLLATE "default"
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_RoleAction
-- ----------------------------
BEGIN;
INSERT INTO "Base_RoleAction" VALUES ('1188801858282459136', '2019-10-28 20:57:04.543', null, 'f', '1180486275199668224', '1182654049414025216');
INSERT INTO "Base_RoleAction" VALUES ('1188801858282459137', '2019-10-28 20:57:04.543', null, 'f', '1180486275199668224', '1182654208411701248');
INSERT INTO "Base_RoleAction" VALUES ('1188801858282459138', '2019-10-28 20:57:04.543', null, 'f', '1180486275199668224', '1183370665412005888');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540544', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188044797802188800');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540545', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188044797802188801');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540546', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1182652433302556672');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540547', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1178957405992521728');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540548', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188801109783744512');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540549', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188801109783744513');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540550', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188801109783744514');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540551', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1182652266117599232');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540552', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188800845714558976');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540553', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188800845714558977');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540554', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188800845714558978');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540555', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1182652367447789568');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540556', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188801057778569216');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540557', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188801057778569217');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540558', '2019-10-28 20:57:34.62', null, 'f', '1180819481383931904', '1188801057778569218');
COMMIT;

-- ----------------------------
-- Table structure for Base_UnitTest
-- ----------------------------
CREATE TABLE "Base_UnitTest" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"UserId" varchar(50) COLLATE "default",
"UserName" varchar(50) COLLATE "default",
"Age" int4
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_UnitTest
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_UnitTest_0
-- ----------------------------
CREATE TABLE "Base_UnitTest_0" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"UserId" varchar(50) COLLATE "default",
"UserName" varchar(50) COLLATE "default",
"Age" int4
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_UnitTest_0
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_UnitTest_1
-- ----------------------------
CREATE TABLE "Base_UnitTest_1" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"UserId" varchar(50) COLLATE "default",
"UserName" varchar(50) COLLATE "default",
"Age" int4
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_UnitTest_1
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_UnitTest_2
-- ----------------------------
CREATE TABLE "Base_UnitTest_2" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"UserId" varchar(50) COLLATE "default",
"UserName" varchar(50) COLLATE "default",
"Age" int4
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_UnitTest_2
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
CREATE TABLE "Base_User" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"UserName" varchar(50) COLLATE "default",
"Password" varchar(50) COLLATE "default",
"RealName" varchar(50) COLLATE "default",
"Sex" int4 DEFAULT 0 NOT NULL,
"Birthday" date,
"DepartmentId" varchar(50) COLLATE "default"
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_User
-- ----------------------------
BEGIN;
INSERT INTO "Base_User" VALUES ('1181928860648738816', '2019-10-09 21:46:14', null, 'f', 'xiaohua', 'e10adc3949ba59abbe56e057f20f883e', '小花', '0', null, null);
INSERT INTO "Base_User" VALUES ('1183363221872971776', '2019-10-13 20:45:52.577', 'Admin', 'f', 'aa', null, 'aaa', '0', null, null);
INSERT INTO "Base_User" VALUES ('Admin', '2019-09-13 21:10:03', 'Admin', 'f', 'Admin', 'e10adc3949ba59abbe56e057f20f883e', '超级管理员', '1', '2019-09-13', null);
COMMIT;

-- ----------------------------
-- Table structure for Base_UserRole
-- ----------------------------
CREATE TABLE "Base_UserRole" (
"Id" varchar(50) COLLATE "default" NOT NULL,
"CreateTime" timestamp(6) NOT NULL,
"CreatorId" varchar(50) COLLATE "default",
"Deleted" bool DEFAULT false NOT NULL,
"UserId" varchar(50) COLLATE "default",
"RoleId" varchar(50) COLLATE "default"
)
WITH (OIDS=TRUE)

;

-- ----------------------------
-- Records of Base_UserRole
-- ----------------------------
BEGIN;
INSERT INTO "Base_UserRole" VALUES ('1181927367719784448', '2019-10-09 21:40:18.27', null, 'f', '1181922344629702656', '1180819481383931904');
INSERT INTO "Base_UserRole" VALUES ('1181927367719784449', '2019-10-09 21:40:18.27', null, 'f', '1181922344629702656', '1180486275199668224');
INSERT INTO "Base_UserRole" VALUES ('1181927783786352640', '2019-10-09 21:41:57.47', null, 'f', '1181927783727632384', '1180819481383931904');
INSERT INTO "Base_UserRole" VALUES ('1188802049190400000', '2019-10-28 20:57:50.057', null, 't', '1181928860648738816', '1180819481383931904');
INSERT INTO "Base_UserRole" VALUES ('1193844767398563840', '2019-11-11 18:55:47.76824', null, 'f', '1181928860648738816', '1180819481383931904');
COMMIT;

-- ----------------------------
-- Alter Sequences Owned By 
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_Action
-- ----------------------------
ALTER TABLE "Base_Action" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE "Base_AppSecret" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_BuildTest
-- ----------------------------
ALTER TABLE "Base_BuildTest" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_DbLink
-- ----------------------------
ALTER TABLE "Base_DbLink" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_Department
-- ----------------------------
ALTER TABLE "Base_Department" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_Log
-- ----------------------------
ALTER TABLE "Base_Log" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_Role
-- ----------------------------
ALTER TABLE "Base_Role" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_RoleAction
-- ----------------------------
ALTER TABLE "Base_RoleAction" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE "Base_UnitTest" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_UnitTest_0
-- ----------------------------
ALTER TABLE "Base_UnitTest_0" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_UnitTest_1
-- ----------------------------
ALTER TABLE "Base_UnitTest_1" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_UnitTest_2
-- ----------------------------
ALTER TABLE "Base_UnitTest_2" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_User
-- ----------------------------
ALTER TABLE "Base_User" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_UserRole
-- ----------------------------
ALTER TABLE "Base_UserRole" ADD PRIMARY KEY ("Id");
