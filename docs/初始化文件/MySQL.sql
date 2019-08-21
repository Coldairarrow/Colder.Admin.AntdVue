/*
Navicat MySQL Data Transfer

Source Server         : .MySQL
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : Colder.Fx.Net.AdminLTE

Target Server Type    : MYSQL
Target Server Version : 50699
File Encoding         : 65001

Date: 2019-06-15 20:50:31
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for Base_AppSecret
-- ----------------------------
DROP TABLE IF EXISTS `Base_AppSecret`;
CREATE TABLE `Base_AppSecret` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '代理主键' ,
`AppId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '应用Id' ,
`AppSecret`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '应用密钥' ,
`AppName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '应用名' ,
PRIMARY KEY (`Id`),
INDEX `AppId` (`AppId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='应用密钥'

;

-- ----------------------------
-- Records of Base_AppSecret
-- ----------------------------
BEGIN;
INSERT INTO `Base_AppSecret` VALUES ('039e41170bc72-b89139b1-f3f4-430e-aed7-36b193d256dc', 'AppAdmin', 'VjxNekN2G2z0qrjW', '超级权限');
COMMIT;

-- ----------------------------
-- Table structure for Base_DatabaseLink
-- ----------------------------
DROP TABLE IF EXISTS `Base_DatabaseLink`;
CREATE TABLE `Base_DatabaseLink` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '代理主键' ,
`LinkName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '连接名' ,
`ConnectionStr`  text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '连接字符串' ,
`DbType`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '数据库类型' ,
`SortNum`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '排序编号' ,
PRIMARY KEY (`Id`),
INDEX `LinkName` (`LinkName`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='数据库连接'

;

-- ----------------------------
-- Records of Base_DatabaseLink
-- ----------------------------
BEGIN;
INSERT INTO `Base_DatabaseLink` VALUES ('039e900bc6bbb-a0070d5c-1fc7-4cf0-a177-e3aebc4633c5', 'SqlServer', 'Data Source=.;Initial Catalog=Colder.Fx.Net.AdminLTE;Integrated Security=True', 'SqlServer', 'aa');
COMMIT;

-- ----------------------------
-- Table structure for Base_Department
-- ----------------------------
DROP TABLE IF EXISTS `Base_Department`;
CREATE TABLE `Base_Department` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`Name`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '部门名' ,
`ParentId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上级部门Id' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='部门表'

;

-- ----------------------------
-- Records of Base_Department
-- ----------------------------
BEGIN;
INSERT INTO `Base_Department` VALUES ('1139811378824089600', '宁波分公司', null), ('1139811435694657536', '鄞州事业部', '1139811378824089600'), ('1139812293048143872', '江北事业部', '1139811378824089600');
COMMIT;

-- ----------------------------
-- Table structure for Base_PermissionAppId
-- ----------------------------
DROP TABLE IF EXISTS `Base_PermissionAppId`;
CREATE TABLE `Base_PermissionAppId` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '代理主键' ,
`AppId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'AppId' ,
`PermissionValue`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限值' ,
PRIMARY KEY (`Id`),
INDEX `RoleId` (`AppId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='AppId权限表'

;

-- ----------------------------
-- Records of Base_PermissionAppId
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_PermissionRole
-- ----------------------------
DROP TABLE IF EXISTS `Base_PermissionRole`;
CREATE TABLE `Base_PermissionRole` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '代理主键' ,
`RoleId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色主键Id' ,
`PermissionValue`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限值' ,
PRIMARY KEY (`Id`),
INDEX `RoleId` (`RoleId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='角色权限表'

;

-- ----------------------------
-- Records of Base_PermissionRole
-- ----------------------------
BEGIN;
INSERT INTO `Base_PermissionRole` VALUES ('1139819691020259328', '1133011663516209152', 'sysuser.search'), ('1139819691020259329', '1133011663516209152', 'sysrole.search'), ('1139819691020259330', '1133011663516209152', 'department.search'), ('1139819691020259331', '1133011663516209152', 'appsecret.search'), ('1139819691020259332', '1133011663516209152', 'sysLog.search');
COMMIT;

-- ----------------------------
-- Table structure for Base_PermissionUser
-- ----------------------------
DROP TABLE IF EXISTS `Base_PermissionUser`;
CREATE TABLE `Base_PermissionUser` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '代理主键' ,
`UserId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户主键Id' ,
`PermissionValue`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限' ,
PRIMARY KEY (`Id`),
INDEX `UserId` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='用户权限表'

;

-- ----------------------------
-- Records of Base_PermissionUser
-- ----------------------------
BEGIN;
INSERT INTO `Base_PermissionUser` VALUES ('1133345814723301376', '1133345545746780160', 'sysLog.search');
COMMIT;

-- ----------------------------
-- Table structure for Base_SysLog
-- ----------------------------
DROP TABLE IF EXISTS `Base_SysLog`;
CREATE TABLE `Base_SysLog` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '代理主键' ,
`LogType`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '日志类型' ,
`LogContent`  longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '日志内容' ,
`OpUserName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作员用户名' ,
`OpTime`  datetime NULL DEFAULT NULL COMMENT '日志记录时间' ,
`Data`  longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '数据备份' ,
`Level`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '日志级别' ,
PRIMARY KEY (`Id`),
INDEX `OpTime` (`OpTime`) USING BTREE ,
INDEX `LogType` (`LogType`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='系统日志表'

;

-- ----------------------------
-- Records of Base_SysLog
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for Base_SysRole
-- ----------------------------
DROP TABLE IF EXISTS `Base_SysRole`;
CREATE TABLE `Base_SysRole` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '代理主键' ,
`RoleName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色名' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='系统角色'

;

-- ----------------------------
-- Records of Base_SysRole
-- ----------------------------
BEGIN;
INSERT INTO `Base_SysRole` VALUES ('1133011623854870528', '超级管理员'), ('1133011663516209152', '部门管理员');
COMMIT;

-- ----------------------------
-- Table structure for Base_UnitTest
-- ----------------------------
DROP TABLE IF EXISTS `Base_UnitTest`;
CREATE TABLE `Base_UnitTest` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '代理主键' ,
`UserId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`UserName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`Age`  int(11) NULL DEFAULT NULL ,
PRIMARY KEY (`Id`),
UNIQUE INDEX `UserId` (`UserId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='单元测试表'

;

-- ----------------------------
-- Records of Base_UnitTest
-- ----------------------------
BEGIN;
INSERT INTO `Base_UnitTest` VALUES ('10', null, null, null), ('1139855817357529088', '1139855817357529089', '超级管理员', '22'), ('6a1230b5-43fa-4d4c-8c3e-59f8e10d89a1', 'Admin', '超级管理员', '22');
COMMIT;

-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
DROP TABLE IF EXISTS `Base_User`;
CREATE TABLE `Base_User` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '代理主键' ,
`UserName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名' ,
`Password`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码' ,
`RealName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '真实姓名' ,
`Sex`  int(11) NULL DEFAULT NULL COMMENT '性别(1为男，0为女)' ,
`Birthday`  date NULL DEFAULT NULL COMMENT '出生日期' ,
`DepartmentId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所属部门Id' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='系统，用户表'

;

-- ----------------------------
-- Records of Base_User
-- ----------------------------
BEGIN;
INSERT INTO `Base_User` VALUES ('1133345545746780160', 'xiaoming', 'e10adc3949ba59abbe56e057f20f883e', 'xiaoming', '1', null, '1139811435694657536'), ('Admin', 'Admin', 'e10adc3949ba59abbe56e057f20f883e', '超级管理员', '1', '2017-12-15', '1139811378824089600');
COMMIT;

-- ----------------------------
-- Table structure for Base_UserRoleMap
-- ----------------------------
DROP TABLE IF EXISTS `Base_UserRoleMap`;
CREATE TABLE `Base_UserRoleMap` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL ,
`UserId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
`RoleId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci

;

-- ----------------------------
-- Records of Base_UserRoleMap
-- ----------------------------
BEGIN;
INSERT INTO `Base_UserRoleMap` VALUES ('1139822682855051264', '1133345545746780160', '1133011663516209152');
COMMIT;

-- ----------------------------
-- Table structure for Dev_Project
-- ----------------------------
DROP TABLE IF EXISTS `Dev_Project`;
CREATE TABLE `Dev_Project` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`ProjectId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '项目Id' ,
`ProjectName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '项目名' ,
`ProjectTypeId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '项目类型Id' ,
`ProjectManagerId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '项目经理Id' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='项目表'

;

-- ----------------------------
-- Records of Dev_Project
-- ----------------------------
BEGIN;
INSERT INTO `Dev_Project` VALUES ('039e943dea9f4-30e0e19b-828e-4938-98b6-da3941987925', 'asdsa', '厉害了', '5645646', 'zxzx');
COMMIT;

-- ----------------------------
-- Table structure for Dev_ProjectType
-- ----------------------------
DROP TABLE IF EXISTS `Dev_ProjectType`;
CREATE TABLE `Dev_ProjectType` (
`Id`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键' ,
`ProjectTypeId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '项目类型Id' ,
`ProjectTypeName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '项目类型名' ,
PRIMARY KEY (`Id`)
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
COMMENT='项目类型表'

;

-- ----------------------------
-- Records of Dev_ProjectType
-- ----------------------------
BEGIN;
INSERT INTO `Dev_ProjectType` VALUES ('1133722179070988288', 'sadsa', 'sdsadasdsa');
COMMIT;
