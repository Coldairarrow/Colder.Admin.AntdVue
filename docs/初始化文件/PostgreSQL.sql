/*
 Navicat Premium Data Transfer

 Source Server         : 192.168.56.103
 Source Server Type    : PostgreSQL
 Source Server Version : 120003
 Source Host           : 192.168.56.103:9999
 Source Catalog        : Colder.Admin.AntdVue
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 120003
 File Encoding         : 65001

 Date: 14/06/2020 22:06:21
*/


-- ----------------------------
-- Table structure for Base_Action
-- ----------------------------
CREATE TABLE "Base_Action" (
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "Deleted" bool NOT NULL DEFAULT false,
  "ParentId" varchar(50) COLLATE "pg_catalog"."default",
  "Type" int4 NOT NULL DEFAULT 0,
  "Name" varchar(50) COLLATE "pg_catalog"."default",
  "Url" varchar(500) COLLATE "pg_catalog"."default",
  "Value" varchar(50) COLLATE "pg_catalog"."default",
  "NeedAction" bool NOT NULL DEFAULT false,
  "Icon" varchar(50) COLLATE "pg_catalog"."default",
  "Sort" int4 NOT NULL DEFAULT 0
)
;

-- ----------------------------
-- Records of Base_Action
-- ----------------------------
BEGIN;
INSERT INTO "Base_Action" VALUES ('1178957405992521728', '2019-10-01 16:58:44', NULL, 'f', NULL, 0, '系统管理', '', NULL, 't', 'setting', 1);
INSERT INTO "Base_Action" VALUES ('1178957553778823168', '2019-10-01 16:59:19', NULL, 'f', '1178957405992521728', 1, '权限管理', '/Base_Manage/Base_Action/List', NULL, 't', NULL, 20);
INSERT INTO "Base_Action" VALUES ('1179018395304071168', '2019-10-01 21:01:05', NULL, 'f', '1178957405992521728', 1, '密钥管理', '/Base_Manage/Base_AppSecret/List', NULL, 't', NULL, 15);
INSERT INTO "Base_Action" VALUES ('1182652367447789568', '2019-10-11 21:41:11', NULL, 'f', '1178957405992521728', 1, '角色管理', '/Base_Manage/Base_Role/List', NULL, 't', NULL, 5);
INSERT INTO "Base_Action" VALUES ('1182652433302556672', '2019-10-11 21:41:27', NULL, 'f', '1178957405992521728', 1, '部门管理', '/Base_Manage/Base_Department/List', NULL, 't', NULL, 10);
INSERT INTO "Base_Action" VALUES ('1188801057778569216', '2019-10-28 20:53:53.687', NULL, 'f', '1182652367447789568', 2, '增', NULL, 'Base_Role.Add', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801057778569217', '2019-10-28 20:53:53.687', NULL, 'f', '1182652367447789568', 2, '改', NULL, 'Base_Role.Edit', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801057778569218', '2019-10-28 20:53:53.687', NULL, 'f', '1182652367447789568', 2, '删', NULL, 'Base_Role.Delete', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801109783744512', '2019-10-28 20:54:06.087', NULL, 'f', '1182652433302556672', 2, '增', NULL, 'Base_Department.Add', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801109783744513', '2019-10-28 20:54:06.087', NULL, 'f', '1182652433302556672', 2, '改', NULL, 'Base_Department.Edit', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801109783744514', '2019-10-28 20:54:06.087', NULL, 'f', '1182652433302556672', 2, '删', NULL, 'Base_Department.Delete', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801273885888512', '2019-10-28 20:54:45.213', NULL, 'f', '1179018395304071168', 2, '增', NULL, 'Base_AppSecret.Add', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801273885888513', '2019-10-28 20:54:45.213', NULL, 'f', '1179018395304071168', 2, '改', NULL, 'Base_AppSecret.Edit', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801273885888514', '2019-10-28 20:54:45.213', NULL, 'f', '1179018395304071168', 2, '删', NULL, 'Base_AppSecret.Delete', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801341661646848', '2019-10-28 20:55:01.37', NULL, 'f', '1178957553778823168', 2, '增', NULL, 'Base_Action.Add', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801341661646849', '2019-10-28 20:55:01.37', NULL, 'f', '1178957553778823168', 2, '改', NULL, 'Base_Action.Edit', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1188801341661646850', '2019-10-28 20:55:01.37', NULL, 'f', '1178957553778823168', 2, '删', NULL, 'Base_Action.Delete', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1193158266167758848', '2019-11-09 21:27:53', 'Admin', 'f', NULL, 0, '首页', NULL, NULL, 't', 'home', 0);
INSERT INTO "Base_Action" VALUES ('1193158630615027712', '2019-11-09 21:29:20.013', 'Admin', 'f', '1193158266167758848', 1, '框架介绍', '/Home/Introduce', NULL, 'f', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1193158780011941888', '2019-11-09 21:29:55.63', 'Admin', 'f', '1193158266167758848', 1, '运营统计', '/Home/Statis', NULL, 'f', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1251789009936453632', '2020-04-19 16:25:31.741292', 'Admin', 'f', '1178957405992521728', 1, '操作日志', '/Base_Manage/Base_UserLog/List', NULL, 'f', NULL, 23);
INSERT INTO "Base_Action" VALUES ('1182652266117599232', '2019-10-11 21:40:47', NULL, 'f', '1178957405992521728', 1, '用户管理', '/Base_Manage/Base_User/List', NULL, 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1272041182930669568', '2020-06-14 13:40:26.050826', NULL, 'f', '1182652266117599232', 2, '增', NULL, 'Base_User.Add', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1272041182930669569', '2020-06-14 13:40:26.050854', NULL, 'f', '1182652266117599232', 2, '改', NULL, 'Base_User.Edit', 't', NULL, 0);
INSERT INTO "Base_Action" VALUES ('1272041182930669570', '2020-06-14 13:40:26.050856', NULL, 'f', '1182652266117599232', 2, '删', NULL, 'Base_User.Delete', 't', NULL, 0);
COMMIT;

-- ----------------------------
-- Table structure for Base_AppSecret
-- ----------------------------
CREATE TABLE "Base_AppSecret" (
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "Deleted" bool NOT NULL DEFAULT false,
  "AppId" varchar(50) COLLATE "pg_catalog"."default",
  "AppSecret" varchar(50) COLLATE "pg_catalog"."default",
  "AppName" varchar(50) COLLATE "pg_catalog"."default"
)
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
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "Deleted" bool NOT NULL DEFAULT false,
  "Column1" varchar(50) COLLATE "pg_catalog"."default",
  "Column2" varchar(50) COLLATE "pg_catalog"."default",
  "Column3" varchar(50) COLLATE "pg_catalog"."default",
  "Column4" varchar(50) COLLATE "pg_catalog"."default",
  "Column5" varchar(50) COLLATE "pg_catalog"."default"
)
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
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "Deleted" bool NOT NULL DEFAULT false,
  "LinkName" varchar(50) COLLATE "pg_catalog"."default",
  "ConnectionStr" varchar(500) COLLATE "pg_catalog"."default",
  "DbType" varchar(50) COLLATE "pg_catalog"."default"
)
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
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "Deleted" bool NOT NULL DEFAULT false,
  "Name" varchar(50) COLLATE "pg_catalog"."default",
  "ParentId" varchar(50) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Records of Base_Department
-- ----------------------------
BEGIN;
INSERT INTO "Base_Department" VALUES ('1181175685528424448', '2019-10-07 19:53:23', NULL, 'f', '宁波分公司', NULL);
INSERT INTO "Base_Department" VALUES ('1181175803631636480', '2019-10-07 19:53:51.427', NULL, 'f', '鄞州事业部', '1181175685528424448');
INSERT INTO "Base_Department" VALUES ('1181175865409540096', '2019-10-07 19:54:06', NULL, 'f', '海曙事业部', '1181175685528424448');
COMMIT;

-- ----------------------------
-- Table structure for Base_Role
-- ----------------------------
CREATE TABLE "Base_Role" (
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "Deleted" bool NOT NULL DEFAULT false,
  "RoleName" varchar(50) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Records of Base_Role
-- ----------------------------
BEGIN;
INSERT INTO "Base_Role" VALUES ('1180486275199668224', '2019-10-05 22:13:55', NULL, 'f', '超级管理员');
INSERT INTO "Base_Role" VALUES ('1180819481383931904', '2019-10-06 20:17:57', NULL, 'f', '部门管理员');
COMMIT;

-- ----------------------------
-- Table structure for Base_RoleAction
-- ----------------------------
CREATE TABLE "Base_RoleAction" (
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "Deleted" bool NOT NULL DEFAULT false,
  "RoleId" varchar(50) COLLATE "pg_catalog"."default",
  "ActionId" varchar(50) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Records of Base_RoleAction
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
CREATE TABLE "Base_User" (
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "Deleted" bool NOT NULL DEFAULT false,
  "UserName" varchar(50) COLLATE "pg_catalog"."default",
  "Password" varchar(50) COLLATE "pg_catalog"."default",
  "RealName" varchar(50) COLLATE "pg_catalog"."default",
  "Sex" int4 NOT NULL DEFAULT 0,
  "Birthday" date,
  "DepartmentId" varchar(50) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Records of Base_User
-- ----------------------------
BEGIN;
INSERT INTO "Base_User" VALUES ('Admin', '2019-09-13 21:10:03', 'Admin', 'f', 'Admin', 'e10adc3949ba59abbe56e057f20f883e', '超级管理员', 1, '2019-09-13', NULL);
COMMIT;

-- ----------------------------
-- Table structure for Base_UserLog
-- ----------------------------
CREATE TABLE "Base_UserLog" (
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "CreatorRealName" varchar(50) COLLATE "pg_catalog"."default",
  "LogType" varchar(50) COLLATE "pg_catalog"."default",
  "LogContent" text COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Records of Base_UserLog
-- ----------------------------
BEGIN;
INSERT INTO "Base_UserLog" VALUES ('1272029420646830080', '2020-06-14 12:53:41.703516', 'Admin', '超级管理员', '系统用户管理', '添加用户:sadsadsa');
INSERT INTO "Base_UserLog" VALUES ('1272029650280779776', '2020-06-14 12:54:36.452629', 'Admin', '超级管理员', '系统用户管理', '修改用户:sadsadsa');
INSERT INTO "Base_UserLog" VALUES ('1272030872345776128', '2020-06-14 12:59:27.815138', 'Admin', '超级管理员', '系统角色管理', '修改角色:超级管理员');
INSERT INTO "Base_UserLog" VALUES ('1272030891475996672', '2020-06-14 12:59:32.376166', 'Admin', '超级管理员', '系统角色管理', '修改角色:超级管理员');
INSERT INTO "Base_UserLog" VALUES ('1272030918021746688', '2020-06-14 12:59:38.705268', 'Admin', '超级管理员', '系统角色管理', '修改角色:超级管理员');
INSERT INTO "Base_UserLog" VALUES ('1272030948765995008', '2020-06-14 12:59:46.03599', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272030966604369920', '2020-06-14 12:59:50.288714', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272030994970447872', '2020-06-14 12:59:57.05113', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272033561364402176', '2020-06-14 13:10:08.927689', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272033590300905472', '2020-06-14 13:10:15.826545', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272033850259673088', '2020-06-14 13:11:17.805003', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272036901221568512', '2020-06-14 13:23:25.211097', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272036916170067968', '2020-06-14 13:23:28.775327', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272037356295163904', '2020-06-14 13:25:13.709362', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272037462578827264', '2020-06-14 13:25:39.049447', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272037849323016192', '2020-06-14 13:27:11.256078', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272040662048444416', '2020-06-14 13:38:21.862887', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272040701143552000', '2020-06-14 13:38:31.183958', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO "Base_UserLog" VALUES ('1272040740783919104', '2020-06-14 13:38:40.634052', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
COMMIT;

-- ----------------------------
-- Table structure for Base_UserRole
-- ----------------------------
CREATE TABLE "Base_UserRole" (
  "Id" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) NOT NULL,
  "CreatorId" varchar(50) COLLATE "pg_catalog"."default",
  "Deleted" bool NOT NULL DEFAULT false,
  "UserId" varchar(50) COLLATE "pg_catalog"."default",
  "RoleId" varchar(50) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Records of Base_UserRole
-- ----------------------------
BEGIN;
INSERT INTO "Base_UserRole" VALUES ('1181927367719784448', '2019-10-09 21:40:18.27', NULL, 'f', '1181922344629702656', '1180819481383931904');
INSERT INTO "Base_UserRole" VALUES ('1181927367719784449', '2019-10-09 21:40:18.27', NULL, 'f', '1181922344629702656', '1180486275199668224');
INSERT INTO "Base_UserRole" VALUES ('1181927783786352640', '2019-10-09 21:41:57.47', NULL, 'f', '1181927783727632384', '1180819481383931904');
INSERT INTO "Base_UserRole" VALUES ('1251788815895367680', '2020-04-19 16:24:45.478714', NULL, 'f', '1181928860648738816', '1180819481383931904');
INSERT INTO "Base_UserRole" VALUES ('1251788815895367681', '2020-04-19 16:24:45.478899', NULL, 'f', '1181928860648738816', '1180819481383931904');
INSERT INTO "Base_UserRole" VALUES ('1272029650192699392', '2020-06-14 12:54:36.431934', NULL, 'f', '1272029420349034496', '1180486275199668224');
INSERT INTO "Base_UserRole" VALUES ('1272029650196893696', '2020-06-14 12:54:36.432072', NULL, 'f', '1272029420349034496', '1180819481383931904');
COMMIT;

-- ----------------------------
-- Primary Key structure for table Base_Action
-- ----------------------------
ALTER TABLE "Base_Action" ADD CONSTRAINT "Base_Action_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE "Base_AppSecret" ADD CONSTRAINT "Base_AppSecret_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_BuildTest
-- ----------------------------
ALTER TABLE "Base_BuildTest" ADD CONSTRAINT "Base_BuildTest_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_DbLink
-- ----------------------------
ALTER TABLE "Base_DbLink" ADD CONSTRAINT "Base_DbLink_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_Department
-- ----------------------------
ALTER TABLE "Base_Department" ADD CONSTRAINT "Base_Department_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_Role
-- ----------------------------
ALTER TABLE "Base_Role" ADD CONSTRAINT "Base_Role_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_RoleAction
-- ----------------------------
ALTER TABLE "Base_RoleAction" ADD CONSTRAINT "Base_RoleAction_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_User
-- ----------------------------
ALTER TABLE "Base_User" ADD CONSTRAINT "Base_User_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_UserLog
-- ----------------------------
ALTER TABLE "Base_UserLog" ADD CONSTRAINT "Base_UserLog_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table Base_UserRole
-- ----------------------------
ALTER TABLE "Base_UserRole" ADD CONSTRAINT "Base_UserRole_pkey" PRIMARY KEY ("Id");
