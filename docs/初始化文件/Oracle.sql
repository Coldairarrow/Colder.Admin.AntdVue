/*
Navicat Oracle Data Transfer
Oracle Client Version : 10.2.0.5.0

Source Server         : 127.0.0.1@SYSTEM
Source Server Version : 110200
Source Host           : 127.0.0.1:1521
Source Schema         : COLDER.ADMIN.ANTDVUE

Target Server Type    : ORACLE
Target Server Version : 110200
File Encoding         : 65001

Date: 2020-04-19 16:14:00
*/


-- ----------------------------
-- Table structure for Base_Action
-- ----------------------------
CREATE TABLE "Base_Action" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"Deleted" NUMBER(1) DEFAULT 0  NOT NULL ,
"ParentId" NVARCHAR2(50) NULL ,
"Type" NUMBER(11) NOT NULL ,
"Name" NVARCHAR2(50) NULL ,
"Url" NVARCHAR2(500) NULL ,
"Value" NVARCHAR2(50) NULL ,
"NeedAction" NUMBER(1) DEFAULT 0  NOT NULL ,
"Icon" NVARCHAR2(50) NULL ,
"Sort" NUMBER(11) NOT NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_Action" IS '系统权限表';
COMMENT ON COLUMN "Base_Action"."Id" IS '主键';
COMMENT ON COLUMN "Base_Action"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_Action"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_Action"."Deleted" IS '否已删除';
COMMENT ON COLUMN "Base_Action"."ParentId" IS '父级Id';
COMMENT ON COLUMN "Base_Action"."Type" IS '类型,菜单=0,页面=1,权限=2';
COMMENT ON COLUMN "Base_Action"."Name" IS '权限名/菜单名';
COMMENT ON COLUMN "Base_Action"."Url" IS '菜单地址';
COMMENT ON COLUMN "Base_Action"."Value" IS '权限值';
COMMENT ON COLUMN "Base_Action"."NeedAction" IS '是否需要权限(仅页面有效)';
COMMENT ON COLUMN "Base_Action"."Icon" IS '图标';
COMMENT ON COLUMN "Base_Action"."Sort" IS '排序';

-- ----------------------------
-- Records of Base_Action
-- ----------------------------
INSERT INTO "Base_Action" VALUES ('1178957405992521728', TO_DATE('2019-10-01 16:58:44', 'YYYY-MM-DD HH24:MI:SS'), null, '0', null, '0', '系统管理', null, null, '1', 'setting', '1');
INSERT INTO "Base_Action" VALUES ('1178957553778823168', TO_DATE('2019-10-01 16:59:19', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1178957405992521728', '1', '权限管理', '/Base_Manage/Base_Action/List', null, '1', null, '20');
INSERT INTO "Base_Action" VALUES ('1179018395304071168', TO_DATE('2019-10-01 21:01:05', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1178957405992521728', '1', '密钥管理', '/Base_Manage/Base_AppSecret/List', null, '1', null, '15');
INSERT INTO "Base_Action" VALUES ('1182652266117599232', TO_DATE('2019-10-11 21:40:47', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1178957405992521728', '1', '用户管理', '/Base_Manage/Base_User/List', null, '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1182652367447789568', TO_DATE('2019-10-11 21:41:11', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1178957405992521728', '1', '角色管理', '/Base_Manage/Base_Role/List', null, '1', null, '5');
INSERT INTO "Base_Action" VALUES ('1182652433302556672', TO_DATE('2019-10-11 21:41:27', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1178957405992521728', '1', '部门管理', '/Base_Manage/Base_Department/List', null, '1', null, '10');
INSERT INTO "Base_Action" VALUES ('1188801057778569216', TO_DATE('2019-10-28 20:53:53', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1182652367447789568', '2', '增', null, 'Base_Role.Add', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801057778569217', TO_DATE('2019-10-28 20:53:53', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1182652367447789568', '2', '改', null, 'Base_Role.Edit', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801057778569218', TO_DATE('2019-10-28 20:53:53', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1182652367447789568', '2', '删', null, 'Base_Role.Delete', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801109783744512', TO_DATE('2019-10-28 20:54:06', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1182652433302556672', '2', '增', null, 'Base_Department.Add', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801109783744513', TO_DATE('2019-10-28 20:54:06', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1182652433302556672', '2', '改', null, 'Base_Department.Edit', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801109783744514', TO_DATE('2019-10-28 20:54:06', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1182652433302556672', '2', '删', null, 'Base_Department.Delete', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801273885888512', TO_DATE('2019-10-28 20:54:45', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1179018395304071168', '2', '增', null, 'Base_AppSecret.Add', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801273885888513', TO_DATE('2019-10-28 20:54:45', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1179018395304071168', '2', '改', null, 'Base_AppSecret.Edit', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801273885888514', TO_DATE('2019-10-28 20:54:45', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1179018395304071168', '2', '删', null, 'Base_AppSecret.Delete', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801341661646848', TO_DATE('2019-10-28 20:55:01', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1178957553778823168', '2', '增', null, 'Base_Action.Add', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801341661646849', TO_DATE('2019-10-28 20:55:01', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1178957553778823168', '2', '改', null, 'Base_Action.Edit', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1188801341661646850', TO_DATE('2019-10-28 20:55:01', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1178957553778823168', '2', '删', null, 'Base_Action.Delete', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1193158266167758848', TO_DATE('2019-11-09 21:27:53', 'YYYY-MM-DD HH24:MI:SS'), 'Admin', '0', null, '0', '首页', null, null, '1', 'home', '0');
INSERT INTO "Base_Action" VALUES ('1193158630615027712', TO_DATE('2019-11-09 21:29:20', 'YYYY-MM-DD HH24:MI:SS'), 'Admin', '0', '1193158266167758848', '1', '框架介绍', '/Home/Introduce', null, '0', null, '0');
INSERT INTO "Base_Action" VALUES ('1193158780011941888', TO_DATE('2019-11-09 21:29:55', 'YYYY-MM-DD HH24:MI:SS'), 'Admin', '0', '1193158266167758848', '1', '运营统计', '/Home/Statis', null, '0', null, '0');
INSERT INTO "Base_Action" VALUES ('1251785113994268674', TO_DATE('2020-04-19 16:10:02', 'YYYY-MM-DD HH24:MI:SS'), null, '1', '1182652266117599232', '2', '删', null, 'Base_User.Delete', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1251785113994268673', TO_DATE('2020-04-19 16:10:02', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1182652266117599232', '2', '改', null, 'Base_User.Edit', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1251785113994268672', TO_DATE('2020-04-19 16:10:02', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1182652266117599232', '2', '增', null, 'Base_User.Add', '1', null, '0');
INSERT INTO "Base_Action" VALUES ('1251785605155655680', TO_DATE('2020-04-19 16:11:59', 'YYYY-MM-DD HH24:MI:SS'), 'Admin', '0', '1178957405992521728', '1', '操作日志', '/Base_Manage/Base_UserLog/List', null, '0', null, '23');

-- ----------------------------
-- Table structure for Base_AppSecret
-- ----------------------------
CREATE TABLE "Base_AppSecret" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"Deleted" NUMBER(1) DEFAULT 0  NOT NULL ,
"AppId" VARCHAR2(50 CHAR) NULL ,
"AppSecret" VARCHAR2(50 CHAR) NULL ,
"AppName" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_AppSecret" IS '应用密钥表';
COMMENT ON COLUMN "Base_AppSecret"."Id" IS '自然主键';
COMMENT ON COLUMN "Base_AppSecret"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_AppSecret"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_AppSecret"."Deleted" IS '否已删除';
COMMENT ON COLUMN "Base_AppSecret"."AppId" IS '应用Id';
COMMENT ON COLUMN "Base_AppSecret"."AppSecret" IS '应用密钥';
COMMENT ON COLUMN "Base_AppSecret"."AppName" IS '应用名';

-- ----------------------------
-- Records of Base_AppSecret
-- ----------------------------
INSERT INTO "Base_AppSecret" VALUES ('1172497995938271232', TO_DATE('2019-09-13 21:11:20', 'YYYY-MM-DD HH24:MI:SS'), 'Admin', '0', 'PcAdmin', 'wtMaiTRPTT3hrf5e', '后台AppId');
INSERT INTO "Base_AppSecret" VALUES ('1173937877642383360', TO_DATE('2019-09-17 20:32:55', 'YYYY-MM-DD HH24:MI:SS'), 'Admin', '0', 'AppAdmin', 'IVh9LLSVFcoQPQ5K', 'APP密钥');

-- ----------------------------
-- Table structure for Base_BuildTest
-- ----------------------------
CREATE TABLE "Base_BuildTest" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"Deleted" NUMBER(1) DEFAULT 0  NOT NULL ,
"Column1" VARCHAR2(50 CHAR) NULL ,
"Column2" VARCHAR2(50 CHAR) NULL ,
"Column3" VARCHAR2(50 CHAR) NULL ,
"Column4" VARCHAR2(50 CHAR) NULL ,
"Column5" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_BuildTest" IS '生成测试表';
COMMENT ON COLUMN "Base_BuildTest"."Id" IS '自然主键';
COMMENT ON COLUMN "Base_BuildTest"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_BuildTest"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_BuildTest"."Deleted" IS '否已删除';
COMMENT ON COLUMN "Base_BuildTest"."Column1" IS '列1';
COMMENT ON COLUMN "Base_BuildTest"."Column2" IS '列2';
COMMENT ON COLUMN "Base_BuildTest"."Column3" IS '列3';
COMMENT ON COLUMN "Base_BuildTest"."Column4" IS '列4';
COMMENT ON COLUMN "Base_BuildTest"."Column5" IS '列5';

-- ----------------------------
-- Records of Base_BuildTest
-- ----------------------------

-- ----------------------------
-- Table structure for Base_DbLink
-- ----------------------------
CREATE TABLE "Base_DbLink" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"Deleted" NUMBER(1) DEFAULT 0  NOT NULL ,
"LinkName" VARCHAR2(50 CHAR) NULL ,
"ConnectionStr" VARCHAR2(500 CHAR) NULL ,
"DbType" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_DbLink" IS '数据库连接表';
COMMENT ON COLUMN "Base_DbLink"."Id" IS '自然主键';
COMMENT ON COLUMN "Base_DbLink"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_DbLink"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_DbLink"."Deleted" IS '否已删除';
COMMENT ON COLUMN "Base_DbLink"."LinkName" IS '连接名';
COMMENT ON COLUMN "Base_DbLink"."ConnectionStr" IS '连接字符串';
COMMENT ON COLUMN "Base_DbLink"."DbType" IS '数据库类型';

-- ----------------------------
-- Records of Base_DbLink
-- ----------------------------
INSERT INTO "Base_DbLink" VALUES ('1183373232498020352', TO_DATE('2019-10-13 21:25:39', 'YYYY-MM-DD HH24:MI:SS'), 'Admin', '0', 'BaseDb', 'Data Source=.;Initial Catalog=Colder.Admin.AntdVue;Integrated Security=True', 'SqlServer');

-- ----------------------------
-- Table structure for Base_Department
-- ----------------------------
CREATE TABLE "Base_Department" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"Deleted" NUMBER DEFAULT 0  NOT NULL ,
"Name" VARCHAR2(50 CHAR) NULL ,
"ParentId" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_Department" IS '部门表';
COMMENT ON COLUMN "Base_Department"."Id" IS '主键';
COMMENT ON COLUMN "Base_Department"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_Department"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_Department"."Deleted" IS '否已删除';
COMMENT ON COLUMN "Base_Department"."Name" IS '部门名';
COMMENT ON COLUMN "Base_Department"."ParentId" IS '上级部门Id';

-- ----------------------------
-- Records of Base_Department
-- ----------------------------
INSERT INTO "Base_Department" VALUES ('1181175685528424448', TO_DATE('2019-10-07 19:53:23', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '宁波分公司', null);
INSERT INTO "Base_Department" VALUES ('1181175803631636480', TO_DATE('2019-10-07 19:53:51', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '鄞州事业部', '1181175685528424448');
INSERT INTO "Base_Department" VALUES ('1181175865409540096', TO_DATE('2019-10-07 19:54:06', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '海曙事业部', '1181175685528424448');

-- ----------------------------
-- Table structure for Base_Role
-- ----------------------------
CREATE TABLE "Base_Role" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"Deleted" NUMBER(1) DEFAULT 0  NOT NULL ,
"RoleName" NVARCHAR2(50) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_Role" IS '系统角色表';
COMMENT ON COLUMN "Base_Role"."Id" IS '主键';
COMMENT ON COLUMN "Base_Role"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_Role"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_Role"."Deleted" IS '否已删除';
COMMENT ON COLUMN "Base_Role"."RoleName" IS '角色名';

-- ----------------------------
-- Records of Base_Role
-- ----------------------------
INSERT INTO "Base_Role" VALUES ('1180486275199668224', TO_DATE('2019-10-05 22:13:55', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '超级管理员');
INSERT INTO "Base_Role" VALUES ('1180819481383931904', TO_DATE('2019-10-06 20:17:57', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '部门管理员');

-- ----------------------------
-- Table structure for Base_RoleAction
-- ----------------------------
CREATE TABLE "Base_RoleAction" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"Deleted" NUMBER(1) DEFAULT 0  NOT NULL ,
"RoleId" VARCHAR2(50 CHAR) NULL ,
"ActionId" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_RoleAction" IS '角色权限表';
COMMENT ON COLUMN "Base_RoleAction"."Id" IS '主键';
COMMENT ON COLUMN "Base_RoleAction"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_RoleAction"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_RoleAction"."Deleted" IS '否已删除';
COMMENT ON COLUMN "Base_RoleAction"."RoleId" IS '用户Id';
COMMENT ON COLUMN "Base_RoleAction"."ActionId" IS '权限Id';

-- ----------------------------
-- Records of Base_RoleAction
-- ----------------------------
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540544', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188044797802188800');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540545', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188044797802188801');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540546', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1182652433302556672');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540547', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1178957405992521728');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540548', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188801109783744512');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540549', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188801109783744513');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540550', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188801109783744514');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540551', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1182652266117599232');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540552', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188800845714558976');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540553', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188800845714558977');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540554', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188800845714558978');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540555', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1182652367447789568');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540556', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188801057778569216');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540557', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188801057778569217');
INSERT INTO "Base_RoleAction" VALUES ('1188801984434540558', TO_DATE('2019-10-28 20:57:34', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180819481383931904', '1188801057778569218');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728962', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1179018395304071168');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728990', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1179018395304071168');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728991', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1182652266117599232');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728992', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1182652367447789568');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728993', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1182652433302556672');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728994', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1182652599069839360');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728995', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188800845714558976');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728996', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188800845714558977');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728997', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188800845714558978');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728998', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801057778569216');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728999', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801057778569217');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729000', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801057778569218');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728989', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1178957553778823168');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729001', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801109783744512');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729003', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801109783744514');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729004', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801273885888512');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729005', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801273885888513');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729006', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801273885888514');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729007', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801341661646848');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729008', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801341661646849');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729009', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801341661646850');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729010', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193158266167758848');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729011', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193158630615027712');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729012', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193158780011941888');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729013', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193527101521661952');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729002', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801109783744513');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728988', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1178957405992521728');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728987', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193527101521661954');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728986', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193527101521661953');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728961', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1178957553778823168');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728960', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1178957405992521728');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728963', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1182652266117599232');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728964', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1182652367447789568');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728965', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1182652433302556672');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728966', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1182652599069839360');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728967', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188800845714558976');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728968', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188800845714558977');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728969', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188800845714558978');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728970', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801057778569216');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728971', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801057778569217');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728972', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801057778569218');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728973', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801109783744512');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728974', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801109783744513');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728975', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801109783744514');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728976', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801273885888512');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728977', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801273885888513');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728978', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801273885888514');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728979', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801341661646848');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728980', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801341661646849');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728981', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1188801341661646850');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728982', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193158266167758848');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728983', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193158630615027712');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728984', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193158780011941888');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050728985', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193527101521661952');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729014', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193527101521661953');
INSERT INTO "Base_RoleAction" VALUES ('1251784960050729015', TO_DATE('2020-04-19 16:09:26', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1180486275199668224', '1193527101521661954');

-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
CREATE TABLE "Base_User" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"Deleted" NUMBER(1) DEFAULT 0  NOT NULL ,
"UserName" VARCHAR2(50 CHAR) NULL ,
"Password" VARCHAR2(50 CHAR) NULL ,
"RealName" NVARCHAR2(50) NULL ,
"Sex" NUMBER(11) NOT NULL ,
"Birthday" DATE NULL ,
"DepartmentId" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_User" IS '系统用户表';
COMMENT ON COLUMN "Base_User"."Id" IS '主键';
COMMENT ON COLUMN "Base_User"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_User"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_User"."Deleted" IS '否已删除';
COMMENT ON COLUMN "Base_User"."UserName" IS '用户名';
COMMENT ON COLUMN "Base_User"."Password" IS '密码';
COMMENT ON COLUMN "Base_User"."RealName" IS '姓名';
COMMENT ON COLUMN "Base_User"."Sex" IS '性别(1为男，0为女)';
COMMENT ON COLUMN "Base_User"."Birthday" IS '出生日期';
COMMENT ON COLUMN "Base_User"."DepartmentId" IS '所属部门Id';

-- ----------------------------
-- Records of Base_User
-- ----------------------------
INSERT INTO "Base_User" VALUES ('1181928860648738816', TO_DATE('2019-10-09 21:46:14', 'YYYY-MM-DD HH24:MI:SS'), null, '0', 'xiaohua', 'e10adc3949ba59abbe56e057f20f883e', '小花', '0', null, null);
INSERT INTO "Base_User" VALUES ('Admin', TO_DATE('2019-09-13 21:10:03', 'YYYY-MM-DD HH24:MI:SS'), 'Admin', '0', 'Admin', 'e10adc3949ba59abbe56e057f20f883e', '超级管理员', '1', TO_DATE('2019-09-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null);

-- ----------------------------
-- Table structure for Base_UserLog
-- ----------------------------
CREATE TABLE "Base_UserLog" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"CreatorRealName" NVARCHAR2(50) NULL ,
"LogType" VARCHAR2(50 CHAR) NULL ,
"LogContent" CLOB NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_UserLog" IS '系统日志表';
COMMENT ON COLUMN "Base_UserLog"."Id" IS '自然主键';
COMMENT ON COLUMN "Base_UserLog"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_UserLog"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_UserLog"."CreatorRealName" IS '创建人姓名';
COMMENT ON COLUMN "Base_UserLog"."LogType" IS '日志类型';
COMMENT ON COLUMN "Base_UserLog"."LogContent" IS '日志内容';

-- ----------------------------
-- Records of Base_UserLog
-- ----------------------------

-- ----------------------------
-- Table structure for Base_UserRole
-- ----------------------------
CREATE TABLE "Base_UserRole" (
"Id" VARCHAR2(50 CHAR) NOT NULL ,
"CreateTime" DATE NOT NULL ,
"CreatorId" VARCHAR2(50 CHAR) NULL ,
"Deleted" NUMBER(1) DEFAULT 0  NOT NULL ,
"UserId" VARCHAR2(50 CHAR) NULL ,
"RoleId" VARCHAR2(50 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "Base_UserRole" IS '用户角色表';
COMMENT ON COLUMN "Base_UserRole"."Id" IS '主键';
COMMENT ON COLUMN "Base_UserRole"."CreateTime" IS '创建时间';
COMMENT ON COLUMN "Base_UserRole"."CreatorId" IS '创建人Id';
COMMENT ON COLUMN "Base_UserRole"."Deleted" IS '否已删除';
COMMENT ON COLUMN "Base_UserRole"."UserId" IS '用户Id';
COMMENT ON COLUMN "Base_UserRole"."RoleId" IS '角色Id';

-- ----------------------------
-- Records of Base_UserRole
-- ----------------------------
INSERT INTO "Base_UserRole" VALUES ('1181927367719784448', TO_DATE('2019-10-09 21:40:18', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1181922344629702656', '1180819481383931904');
INSERT INTO "Base_UserRole" VALUES ('1181927367719784449', TO_DATE('2019-10-09 21:40:18', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1181922344629702656', '1180486275199668224');
INSERT INTO "Base_UserRole" VALUES ('1181927783786352640', TO_DATE('2019-10-09 21:41:57', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1181927783727632384', '1180819481383931904');
INSERT INTO "Base_UserRole" VALUES ('1251784944565358592', TO_DATE('2020-04-19 16:09:22', 'YYYY-MM-DD HH24:MI:SS'), null, '0', '1181928860648738816', '1180819481383931904');

-- ----------------------------
-- Indexes structure for table Base_Action
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_Action
-- ----------------------------
ALTER TABLE "Base_Action" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Type" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("NeedAction" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Sort" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Type" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("NeedAction" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Sort" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Type" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("NeedAction" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Sort" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Type" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("NeedAction" IS NOT NULL);
ALTER TABLE "Base_Action" ADD CHECK ("Sort" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_Action
-- ----------------------------
ALTER TABLE "Base_Action" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_AppSecret
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE "Base_AppSecret" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_AppSecret" ADD CHECK ("Deleted" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE "Base_AppSecret" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_BuildTest
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_BuildTest
-- ----------------------------
ALTER TABLE "Base_BuildTest" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_BuildTest" ADD CHECK ("Deleted" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_BuildTest
-- ----------------------------
ALTER TABLE "Base_BuildTest" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_DbLink
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_DbLink
-- ----------------------------
ALTER TABLE "Base_DbLink" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_DbLink" ADD CHECK ("Deleted" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_DbLink
-- ----------------------------
ALTER TABLE "Base_DbLink" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_Department
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_Department
-- ----------------------------
ALTER TABLE "Base_Department" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Department" ADD CHECK ("Deleted" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_Department
-- ----------------------------
ALTER TABLE "Base_Department" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table Base_Role
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_Role
-- ----------------------------
ALTER TABLE "Base_Role" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_Role" ADD CHECK ("Deleted" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_Role
-- ----------------------------
ALTER TABLE "Base_Role" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Checks structure for table Base_RoleAction
-- ----------------------------
ALTER TABLE "Base_RoleAction" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_RoleAction" ADD CHECK ("Deleted" IS NOT NULL);

-- ----------------------------
-- Checks structure for table Base_User
-- ----------------------------
ALTER TABLE "Base_User" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Sex" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Sex" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Sex" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_User" ADD CHECK ("Sex" IS NOT NULL);

-- ----------------------------
-- Indexes structure for table Base_UserLog
-- ----------------------------

-- ----------------------------
-- Checks structure for table Base_UserLog
-- ----------------------------
ALTER TABLE "Base_UserLog" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_UserLog" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_UserLog" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_UserLog" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_UserLog" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_UserLog" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_UserLog" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_UserLog" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_UserLog" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_UserLog" ADD CHECK ("CreateTime" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table Base_UserLog
-- ----------------------------
ALTER TABLE "Base_UserLog" ADD PRIMARY KEY ("Id");

-- ----------------------------
-- Checks structure for table Base_UserRole
-- ----------------------------
ALTER TABLE "Base_UserRole" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("Deleted" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("Id" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("CreateTime" IS NOT NULL);
ALTER TABLE "Base_UserRole" ADD CHECK ("Deleted" IS NOT NULL);
