/*
Navicat SQL Server Data Transfer

Source Server         : .@SQLServer
Source Server Version : 105000
Source Host           : .:1433
Source Database       : Colder.Fx.Core.AdminLTE
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 105000
File Encoding         : 65001

Date: 2019-06-29 20:02:27
*/


-- ----------------------------
-- Table structure for Base_AppSecret
-- ----------------------------
CREATE TABLE [Base_AppSecret] (
[Id] varchar(50) NOT NULL ,
[AppId] varchar(50) NULL ,
[AppSecret] varchar(50) NULL ,
[AppName] varchar(255) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'应用密钥'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'应用密钥'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'AppId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'应用Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'应用Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'AppSecret')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'应用密钥'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppSecret'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'应用密钥'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppSecret'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'AppName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'应用名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'应用名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppName'
GO

-- ----------------------------
-- Records of Base_AppSecret
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_AppSecret] ([Id], [AppId], [AppSecret], [AppName]) VALUES (N'039e41170bc72-b89139b1-f3f4-430e-aed7-36b193d256dc', N'AppAdmin', N'VjxNekN2G2z0qrjW', N'超级权限')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_DatabaseLink
-- ----------------------------
CREATE TABLE [Base_DatabaseLink] (
[Id] varchar(50) NOT NULL ,
[LinkName] varchar(50) NULL ,
[ConnectionStr] varchar(1000) NULL ,
[DbType] varchar(50) NULL ,
[SortNum] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'数据库连接'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'数据库连接'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'LinkName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'连接名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'LinkName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'连接名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'LinkName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'ConnectionStr')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'连接字符串'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'ConnectionStr'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'连接字符串'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'ConnectionStr'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'DbType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'数据库类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'DbType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'数据库类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'DbType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'SortNum')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'排序编号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'SortNum'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'排序编号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'SortNum'
GO

-- ----------------------------
-- Records of Base_DatabaseLink
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_DatabaseLink] ([Id], [LinkName], [ConnectionStr], [DbType], [SortNum]) VALUES (N'039e900bc6bbb-a0070d5c-1fc7-4cf0-a177-e3aebc4633c5', N'SqlServer', N'Data Source=.;Initial Catalog=Colder.Fx.Net.AdminLTE;Integrated Security=True', N'SqlServer', N'aa')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_Department
-- ----------------------------
CREATE TABLE [Base_Department] (
[Id] varchar(50) NOT NULL ,
[Name] varchar(50) NULL ,
[ParentId] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Department', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'部门表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'部门表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Department', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Department', 
'COLUMN', N'Name')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'部门名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'Name'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'部门名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'Name'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Department', 
'COLUMN', N'ParentId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'上级部门Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'ParentId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'上级部门Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'ParentId'
GO

-- ----------------------------
-- Records of Base_Department
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_Department] ([Id], [Name], [ParentId]) VALUES (N'1139811378824089600', N'宁波分公司', null), (N'1139811435694657536', N'鄞州事业部', N'1139811378824089600'), (N'1139812293048143872', N'江北事业部', N'1139811378824089600')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_PermissionAppId
-- ----------------------------
CREATE TABLE [Base_PermissionAppId] (
[Id] varchar(50) NOT NULL ,
[AppId] varchar(50) NULL ,
[PermissionValue] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionAppId', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'AppId权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'AppId权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionAppId', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionAppId', 
'COLUMN', N'AppId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'AppId'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'AppId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'AppId'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'AppId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionAppId', 
'COLUMN', N'PermissionValue')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
GO

-- ----------------------------
-- Records of Base_PermissionAppId
-- ----------------------------
BEGIN TRANSACTION
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_PermissionRole
-- ----------------------------
CREATE TABLE [Base_PermissionRole] (
[Id] varchar(50) NOT NULL ,
[RoleId] varchar(50) NULL ,
[PermissionValue] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionRole', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'角色权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'角色权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionRole', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionRole', 
'COLUMN', N'RoleId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'角色主键Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'RoleId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'角色主键Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'RoleId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionRole', 
'COLUMN', N'PermissionValue')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
GO

-- ----------------------------
-- Records of Base_PermissionRole
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'1139819691020259328', N'1133011663516209152', N'sysuser.search'), (N'1139819691020259329', N'1133011663516209152', N'sysrole.search'), (N'1139819691020259330', N'1133011663516209152', N'department.search'), (N'1139819691020259331', N'1133011663516209152', N'appsecret.search'), (N'1139819691020259332', N'1133011663516209152', N'sysLog.search')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_PermissionUser
-- ----------------------------
CREATE TABLE [Base_PermissionUser] (
[Id] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[PermissionValue] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionUser', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionUser', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionUser', 
'COLUMN', N'UserId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户主键Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'UserId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户主键Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'UserId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionUser', 
'COLUMN', N'PermissionValue')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权限'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权限'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
GO

-- ----------------------------
-- Records of Base_PermissionUser
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_PermissionUser] ([Id], [UserId], [PermissionValue]) VALUES (N'1133345814723301376', N'1133345545746780160', N'sysLog.search')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_SysLog
-- ----------------------------
CREATE TABLE [Base_SysLog] (
[Id] varchar(50) NOT NULL ,
[LogType] varchar(255) NULL ,
[LogContent] varchar(MAX) NULL ,
[OpUserName] varchar(255) NULL ,
[OpTime] datetime NULL ,
[Data] text NULL ,
[Level] varchar(255) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统日志表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统日志表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'LogType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'LogType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'LogType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'LogContent')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'LogContent'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'LogContent'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'OpUserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'操作员用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'OpUserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'操作员用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'OpUserName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'OpTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志记录时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'OpTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志记录时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'OpTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'Data')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'数据备份'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'Data'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'数据备份'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'Data'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'Level')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志级别'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'Level'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志级别'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'Level'
GO

-- ----------------------------
-- Records of Base_SysLog
-- ----------------------------
BEGIN TRANSACTION
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_SysRole
-- ----------------------------
CREATE TABLE [Base_SysRole] (
[Id] varchar(50) NOT NULL ,
[RoleName] nvarchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysRole', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统角色'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统角色'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysRole', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysRole', 
'COLUMN', N'RoleName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'角色名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
, @level2type = 'COLUMN', @level2name = N'RoleName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'角色名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
, @level2type = 'COLUMN', @level2name = N'RoleName'
GO

-- ----------------------------
-- Records of Base_SysRole
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_SysRole] ([Id], [RoleName]) VALUES (N'1133011623854870528', N'超级管理员'), (N'1133011663516209152', N'部门管理员')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_UnitTest
-- ----------------------------
CREATE TABLE [Base_UnitTest] (
[Id] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[UserName] varchar(50) NULL ,
[Age] int NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest'
, @level2type = 'COLUMN', @level2name = N'Id'
GO

-- ----------------------------
-- Records of Base_UnitTest
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_UnitTest] ([Id], [UserId], [UserName], [Age]) VALUES (N'8f3f78cb-8744-4453-b966-866030cc6c3c', N'2', N'190c2e3d-c3e1-4324-b61d-5f120696da10', null), (N'c20126d4-f895-476d-8dde-d9ba403f1d1c', N'1', N'e5ae00c6-f2fb-4955-9398-3b3b3645d8c8', null)
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
CREATE TABLE [Base_User] (
[Id] varchar(50) NOT NULL ,
[UserName] varchar(255) NULL ,
[Password] varchar(255) NULL ,
[RealName] varchar(50) NULL ,
[Sex] int NULL ,
[Birthday] date NULL ,
[DepartmentId] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统，用户表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统，用户表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'UserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'UserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'UserName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Password')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'密码'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Password'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'密码'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Password'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'RealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'真实姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'RealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'真实姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'RealName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Sex')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'性别(1为男，0为女)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Sex'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'性别(1为男，0为女)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Sex'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Birthday')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'出生日期'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Birthday'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'出生日期'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Birthday'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'DepartmentId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'所属部门Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'DepartmentId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'所属部门Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'DepartmentId'
GO

-- ----------------------------
-- Records of Base_User
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_User] ([Id], [UserName], [Password], [RealName], [Sex], [Birthday], [DepartmentId]) VALUES (N'1133345545746780160', N'xiaoming', N'e10adc3949ba59abbe56e057f20f883e', N'xiaoming', N'10000', null, N'1139811435694657536'), (N'Admin', N'Admin', N'e10adc3949ba59abbe56e057f20f883e', N'超级管理员', N'1', N'2017-12-15', N'1139811378824089600')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_UserRoleMap
-- ----------------------------
CREATE TABLE [Base_UserRoleMap] (
[Id] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[RoleId] varchar(50) NULL 
)


GO

-- ----------------------------
-- Records of Base_UserRoleMap
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_UserRoleMap] ([Id], [UserId], [RoleId]) VALUES (N'1139822682855051264', N'1133345545746780160', N'1133011663516209152')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Dev_Project
-- ----------------------------
CREATE TABLE [Dev_Project] (
[Id] varchar(50) NOT NULL ,
[ProjectId] varchar(50) NOT NULL ,
[ProjectName] varchar(255) NOT NULL ,
[ProjectTypeId] varchar(50) NULL ,
[ProjectManagerId] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'ProjectId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'ProjectName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'ProjectTypeId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目类型Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目类型Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'ProjectManagerId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目经理Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectManagerId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目经理Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectManagerId'
GO

-- ----------------------------
-- Records of Dev_Project
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Dev_Project] ([Id], [ProjectId], [ProjectName], [ProjectTypeId], [ProjectManagerId]) VALUES (N'039e943dea9f4-30e0e19b-828e-4938-98b6-da3941987925', N'asdsa', N'厉害了', N'5645646', N'zxzx')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Dev_ProjectType
-- ----------------------------
CREATE TABLE [Dev_ProjectType] (
[Id] varchar(50) NOT NULL ,
[ProjectTypeId] varchar(50) NULL ,
[ProjectTypeName] varchar(255) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_ProjectType', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目类型表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目类型表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_ProjectType', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_ProjectType', 
'COLUMN', N'ProjectTypeId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目类型Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目类型Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_ProjectType', 
'COLUMN', N'ProjectTypeName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目类型名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目类型名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeName'
GO

-- ----------------------------
-- Records of Dev_ProjectType
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Dev_ProjectType] ([Id], [ProjectTypeId], [ProjectTypeName]) VALUES (N'1133722179070988288', N'sadsa', N'sdsadasdsa')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Indexes structure for table Base_AppSecret
-- ----------------------------
CREATE CLUSTERED INDEX [AppId] ON [Base_AppSecret]
([AppId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE [Base_AppSecret] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_DatabaseLink
-- ----------------------------
CREATE CLUSTERED INDEX [LinkName] ON [Base_DatabaseLink]
([LinkName] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_DatabaseLink
-- ----------------------------
ALTER TABLE [Base_DatabaseLink] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_Department
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_Department
-- ----------------------------
ALTER TABLE [Base_Department] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_PermissionAppId
-- ----------------------------
CREATE CLUSTERED INDEX [RoleId] ON [Base_PermissionAppId]
([AppId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_PermissionAppId
-- ----------------------------
ALTER TABLE [Base_PermissionAppId] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_PermissionRole
-- ----------------------------
CREATE CLUSTERED INDEX [RoleId] ON [Base_PermissionRole]
([RoleId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_PermissionRole
-- ----------------------------
ALTER TABLE [Base_PermissionRole] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_PermissionUser
-- ----------------------------
CREATE CLUSTERED INDEX [UserId] ON [Base_PermissionUser]
([UserId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_PermissionUser
-- ----------------------------
ALTER TABLE [Base_PermissionUser] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_SysLog
-- ----------------------------
CREATE CLUSTERED INDEX [OpTime] ON [Base_SysLog]
([OpTime] ASC) 
GO
CREATE INDEX [LogType] ON [Base_SysLog]
([LogType] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_SysLog
-- ----------------------------
ALTER TABLE [Base_SysLog] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_SysRole
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_SysRole
-- ----------------------------
ALTER TABLE [Base_SysRole] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_UnitTest
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE [Base_UnitTest] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Uniques structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE [Base_UnitTest] ADD UNIQUE ([UserId] ASC)
GO

-- ----------------------------
-- Indexes structure for table Base_User
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_User
-- ----------------------------
ALTER TABLE [Base_User] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Uniques structure for table Base_User
-- ----------------------------
ALTER TABLE [Base_User] ADD UNIQUE ([UserName] ASC)
GO

-- ----------------------------
-- Indexes structure for table Base_UserRoleMap
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UserRoleMap
-- ----------------------------
ALTER TABLE [Base_UserRoleMap] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Dev_Project
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Dev_Project
-- ----------------------------
ALTER TABLE [Dev_Project] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Dev_ProjectType
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Dev_ProjectType
-- ----------------------------
ALTER TABLE [Dev_ProjectType] ADD PRIMARY KEY ([Id])
GO
