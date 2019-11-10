/*
Navicat MySQL Data Transfer

Source Server         : .MySQL
Source Server Version : 50717
Source Host           : 127.0.0.1:3306
Source Database       : colder.admin.antdvue

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2019-11-10 23:01:30
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for base_action
-- ----------------------------
CREATE TABLE `base_action` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除' ,
`ParentId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父级Id' ,
`Type`  int(11) NOT NULL COMMENT '类型,菜单=0,页面=1,权限=2' ,
`Name`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限名/菜单名' ,
`Url`  text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '菜单地址' ,
`Value`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限值' ,
`NeedAction`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否需要权限(仅页面有效)' ,
`Icon`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标' ,
`Sort`  int(11) NOT NULL DEFAULT 0 COMMENT '排序' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='系统权限表'

;

-- ----------------------------
-- Records of base_action
-- ----------------------------
BEGIN;
INSERT INTO `base_action` VALUES ('1178957405992521728', '2019-10-01 16:58:44', null, null, '0', null, '0', '系统管理', '', null, '1', 'setting', '1'), ('1178957553778823168', '2019-10-01 16:59:19', null, null, '0', '1178957405992521728', '1', '权限管理', '/Base_Manage/Base_Action/List', null, '1', null, '20'), ('1179018395304071168', '2019-10-01 21:01:05', null, null, '0', '1178957405992521728', '1', '密钥管理', '/Base_Manage/Base_AppSecret/List', null, '1', null, '15'), ('1182652266117599232', '2019-10-11 21:40:47', null, null, '0', '1178957405992521728', '1', '用户管理', '/Base_Manage/Base_User/List', null, '1', null, '0'), ('1182652367447789568', '2019-10-11 21:41:11', null, null, '0', '1178957405992521728', '1', '角色管理', '/Base_Manage/Base_Role/List', null, '1', null, '5'), ('1182652433302556672', '2019-10-11 21:41:27', null, null, '0', '1178957405992521728', '1', '部门管理', '/Base_Manage/Base_Department/List', null, '1', null, '10'), ('1182652599069839360', '2019-10-11 21:42:06', null, null, '0', '1178957405992521728', '1', '系统日志', '/Base_Manage/Base_Log/List', null, '1', null, '25'), ('1188800845714558976', '2019-10-28 20:53:03', null, null, '1', '1182652266117599232', '2', '增', null, 'Base_User.Add', '1', null, '0'), ('1188800845714558977', '2019-10-28 20:53:03', null, null, '1', '1182652266117599232', '2', '改', null, 'Base_User.Edit', '1', null, '0'), ('1188800845714558978', '2019-10-28 20:53:03', null, null, '1', '1182652266117599232', '2', '删', null, 'Base_User.Delete', '1', null, '0'), ('1188801057778569216', '2019-10-28 20:53:53', null, null, '0', '1182652367447789568', '2', '增', null, 'Base_Role.Add', '1', null, '0'), ('1188801057778569217', '2019-10-28 20:53:53', null, null, '0', '1182652367447789568', '2', '改', null, 'Base_Role.Edit', '1', null, '0'), ('1188801057778569218', '2019-10-28 20:53:53', null, null, '0', '1182652367447789568', '2', '删', null, 'Base_Role.Delete', '1', null, '0'), ('1188801109783744512', '2019-10-28 20:54:06', null, null, '0', '1182652433302556672', '2', '增', null, 'Base_Department.Add', '1', null, '0'), ('1188801109783744513', '2019-10-28 20:54:06', null, null, '0', '1182652433302556672', '2', '改', null, 'Base_Department.Edit', '1', null, '0'), ('1188801109783744514', '2019-10-28 20:54:06', null, null, '0', '1182652433302556672', '2', '删', null, 'Base_Department.Delete', '1', null, '0'), ('1188801273885888512', '2019-10-28 20:54:45', null, null, '0', '1179018395304071168', '2', '增', null, 'Base_AppSecret.Add', '1', null, '0'), ('1188801273885888513', '2019-10-28 20:54:45', null, null, '0', '1179018395304071168', '2', '改', null, 'Base_AppSecret.Edit', '1', null, '0'), ('1188801273885888514', '2019-10-28 20:54:45', null, null, '0', '1179018395304071168', '2', '删', null, 'Base_AppSecret.Delete', '1', null, '0'), ('1188801341661646848', '2019-10-28 20:55:01', null, null, '0', '1178957553778823168', '2', '增', null, 'Base_Action.Add', '1', null, '0'), ('1188801341661646849', '2019-10-28 20:55:01', null, null, '0', '1178957553778823168', '2', '改', null, 'Base_Action.Edit', '1', null, '0'), ('1188801341661646850', '2019-10-28 20:55:01', null, null, '0', '1178957553778823168', '2', '删', null, 'Base_Action.Delete', '1', null, '0'), ('1193158266167758848', '2019-11-09 21:27:53', 'Admin', '超级管理员', '0', null, '0', '首页', null, null, '1', 'home', '0'), ('1193158630615027712', '2019-11-09 21:29:20', 'Admin', '超级管理员', '0', '1193158266167758848', '1', '框架介绍', '/Home/Introduce', null, '0', null, '0'), ('1193158780011941888', '2019-11-09 21:29:55', 'Admin', '超级管理员', '0', '1193158266167758848', '1', '运营统计', '/Home/Statis', null, '0', null, '0'), ('1193527101521661952', '2019-11-10 21:53:30', null, null, '0', '1182652266117599232', '2', '增', null, 'Base_User.Add', '1', null, '0'), ('1193527101521661953', '2019-11-10 21:53:30', null, null, '0', '1182652266117599232', '2', '改', null, 'Base_User.Edit', '1', null, '0'), ('1193527101521661954', '2019-11-10 21:53:30', null, null, '0', '1182652266117599232', '2', '删', null, 'Base_User.Delete', '1', null, '0');
COMMIT;

-- ----------------------------
-- Table structure for base_appsecret
-- ----------------------------
CREATE TABLE `base_appsecret` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除' ,
`AppId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '应用Id' ,
`AppSecret`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '应用密钥' ,
`AppName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '应用名' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='应用密钥表'

;

-- ----------------------------
-- Records of base_appsecret
-- ----------------------------
BEGIN;
INSERT INTO `base_appsecret` VALUES ('1172497995938271232', '2019-09-13 21:11:20', 'Admin', '超级管理员', '0', 'PcAdmin', 'wtMaiTRPTT3hrf5e', '后台AppId'), ('1173937877642383360', '2019-09-17 20:32:55', 'Admin', '超级管理员', '0', 'AppAdmin', 'IVh9LLSVFcoQPQ5K', 'APP密钥');
COMMIT;

-- ----------------------------
-- Table structure for base_buildtest
-- ----------------------------
CREATE TABLE `base_buildtest` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除' ,
`Column1`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列1' ,
`Column2`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列2' ,
`Column3`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列3' ,
`Column4`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列4' ,
`Column5`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列5' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='生成测试表'

;

-- ----------------------------
-- Records of base_buildtest
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for base_dblink
-- ----------------------------
CREATE TABLE `base_dblink` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除' ,
`LinkName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '连接名' ,
`ConnectionStr`  text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '连接字符串' ,
`DbType`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '数据库类型' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='数据库连接表'

;

-- ----------------------------
-- Records of base_dblink
-- ----------------------------
BEGIN;
INSERT INTO `base_dblink` VALUES ('1183373232498020352', '2019-10-13 21:25:39', 'Admin', '超级管理员', '0', 'BaseDb', 'Data Source=.;Initial Catalog=Colder.Admin.AntdVue;Integrated Security=True', 'SqlServer');
COMMIT;

-- ----------------------------
-- Table structure for base_department
-- ----------------------------
CREATE TABLE `base_department` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除' ,
`Name`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '部门名' ,
`ParentId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上级部门Id' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='部门表'

;

-- ----------------------------
-- Records of base_department
-- ----------------------------
BEGIN;
INSERT INTO `base_department` VALUES ('1181175685528424448', '2019-10-07 19:53:23', null, null, '0', '宁波分公司', null), ('1181175803631636480', '2019-10-07 19:53:51', null, null, '0', '鄞州事业部', '1181175685528424448'), ('1181175865409540096', '2019-10-07 19:54:06', null, null, '0', '海曙事业部', '1181175685528424448');
COMMIT;

-- ----------------------------
-- Table structure for base_log
-- ----------------------------
CREATE TABLE `base_log` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL COMMENT '否已删除' ,
`Level`  varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '日志级别' ,
`LogType`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '日志类型' ,
`LogContent`  longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '日志内容' ,
`Data`  longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '数据备份（转为JSON字符串）' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='系统日志表'

;

-- ----------------------------
-- Records of base_log
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for base_role
-- ----------------------------
CREATE TABLE `base_role` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除' ,
`RoleName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色名' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='系统角色表'

;

-- ----------------------------
-- Records of base_role
-- ----------------------------
BEGIN;
INSERT INTO `base_role` VALUES ('1180486275199668224', '2019-10-05 22:13:55', null, null, '0', '超级管理员'), ('1180819481383931904', '2019-10-06 20:17:57', null, null, '0', '部门管理员');
COMMIT;

-- ----------------------------
-- Table structure for base_roleaction
-- ----------------------------
CREATE TABLE `base_roleaction` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除' ,
`RoleId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户Id' ,
`ActionId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限Id' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='角色权限表'

;

-- ----------------------------
-- Records of base_roleaction
-- ----------------------------
BEGIN;
INSERT INTO `base_roleaction` VALUES ('1188801858282459136', '2019-10-28 20:57:04', null, null, '0', '1180486275199668224', '1182654049414025216'), ('1188801858282459137', '2019-10-28 20:57:04', null, null, '0', '1180486275199668224', '1182654208411701248'), ('1188801858282459138', '2019-10-28 20:57:04', null, null, '0', '1180486275199668224', '1183370665412005888'), ('1188801984434540544', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188044797802188800'), ('1188801984434540545', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188044797802188801'), ('1188801984434540546', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1182652433302556672'), ('1188801984434540547', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1178957405992521728'), ('1188801984434540548', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188801109783744512'), ('1188801984434540549', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188801109783744513'), ('1188801984434540550', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188801109783744514'), ('1188801984434540551', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1182652266117599232'), ('1188801984434540552', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188800845714558976'), ('1188801984434540553', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188800845714558977'), ('1188801984434540554', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188800845714558978'), ('1188801984434540555', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1182652367447789568'), ('1188801984434540556', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188801057778569216'), ('1188801984434540557', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188801057778569217'), ('1188801984434540558', '2019-10-28 20:57:34', null, null, '0', '1180819481383931904', '1188801057778569218');
COMMIT;

-- ----------------------------
-- Table structure for base_unittest
-- ----------------------------
CREATE TABLE `base_unittest` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`UserId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户Id' ,
`UserName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名' ,
`Age`  int(11) NULL DEFAULT NULL COMMENT '年龄' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='单元测试表'

;

-- ----------------------------
-- Records of base_unittest
-- ----------------------------
BEGIN;
INSERT INTO `base_unittest` VALUES ('10', null, null, null), ('1193526482081681408', '1193526482081681409', '超级管理员', '22'), ('232504dc-bfcf-4ad5-80b1-a5be0de4978a', 'Admin', '1193526486141767680', '22'), ('57fcab2a-502b-4f04-bd7f-8f90ef17c158', 'Admin', '超级管理员', '22');
COMMIT;

-- ----------------------------
-- Table structure for base_unittest_0
-- ----------------------------
CREATE TABLE `base_unittest_0` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`UserId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户Id' ,
`UserName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名' ,
`Age`  int(11) NULL DEFAULT NULL COMMENT '年龄' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='单元测试表'

;

-- ----------------------------
-- Records of base_unittest_0
-- ----------------------------
BEGIN;
INSERT INTO `base_unittest_0` VALUES ('4242e164-fa1b-46a3-920f-f2d7b16f8b5d', 'Admin2', '超级管理员', '22');
COMMIT;

-- ----------------------------
-- Table structure for base_unittest_1
-- ----------------------------
CREATE TABLE `base_unittest_1` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`UserId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户Id' ,
`UserName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名' ,
`Age`  int(11) NULL DEFAULT NULL COMMENT '年龄' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='单元测试表'

;

-- ----------------------------
-- Records of base_unittest_1
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for base_unittest_2
-- ----------------------------
CREATE TABLE `base_unittest_2` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`UserId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户Id' ,
`UserName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名' ,
`Age`  int(11) NULL DEFAULT NULL COMMENT '年龄' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='单元测试表'

;

-- ----------------------------
-- Records of base_unittest_2
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for base_user
-- ----------------------------
CREATE TABLE `base_user` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除' ,
`UserName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名' ,
`Password`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码' ,
`RealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '姓名' ,
`Sex`  int(11) NOT NULL DEFAULT 0 COMMENT '性别(1为男，0为女)' ,
`Birthday`  date NULL DEFAULT NULL COMMENT '出生日期' ,
`DepartmentId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所属部门Id' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='系统用户表'

;

-- ----------------------------
-- Records of base_user
-- ----------------------------
BEGIN;
INSERT INTO `base_user` VALUES ('1181928860648738816', '2019-10-09 21:46:14', null, '超级管理员', '0', 'xiaohua', 'e10adc3949ba59abbe56e057f20f883e', '小花', '0', null, null), ('1183363221872971776', '2019-10-13 20:45:52', 'Admin', '超级管理员', '0', 'aa', null, 'aaa', '0', null, null), ('Admin', '2019-09-13 21:10:03', 'Admin', '超级管理员', '0', 'Admin', 'e10adc3949ba59abbe56e057f20f883e', '超级管理员', '1', '2019-09-13', null);
COMMIT;

-- ----------------------------
-- Table structure for base_userrole
-- ----------------------------
CREATE TABLE `base_userrole` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键' ,
`CreateTime`  datetime NOT NULL COMMENT '创建时间' ,
`CreatorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id' ,
`CreatorRealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名' ,
`Deleted`  tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除' ,
`UserId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户Id' ,
`RoleId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色Id' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='用户角色表'

;

-- ----------------------------
-- Records of base_userrole
-- ----------------------------
BEGIN;
INSERT INTO `base_userrole` VALUES ('1181927367719784448', '2019-10-09 21:40:18', null, null, '0', '1181922344629702656', '1180819481383931904'), ('1181927367719784449', '2019-10-09 21:40:18', null, null, '0', '1181922344629702656', '1180486275199668224'), ('1181927783786352640', '2019-10-09 21:41:57', null, null, '0', '1181927783727632384', '1180819481383931904'), ('1188802049190400000', '2019-10-28 20:57:50', null, null, '0', '1181928860648738816', '1180819481383931904');
COMMIT;
