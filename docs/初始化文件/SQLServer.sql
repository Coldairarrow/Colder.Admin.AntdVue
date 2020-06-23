/*
Navicat SQL Server Data Transfer

Source Server         : 127.0.0.1
Source Server Version : 150000
Source Host           : 127.0.0.1:1433
Source Database       : Colder.Admin.AntdVue
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 105000
File Encoding         : 65001

Date: 2020-04-19 15:03:39
*/


-- ----------------------------
-- Table structure for Base_Action
-- ----------------------------
CREATE TABLE [Base_Action] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
[ParentId] nvarchar(50) NULL ,
[Type] int NOT NULL DEFAULT ((0)) ,
[Name] nvarchar(50) NULL ,
[Url] nvarchar(500) NULL ,
[Value] nvarchar(50) NULL ,
[NeedAction] bit NOT NULL DEFAULT ((0)) ,
[Icon] nvarchar(50) NULL ,
[Sort] int NOT NULL DEFAULT ((0)) 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Deleted'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'ParentId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'父级Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'ParentId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'父级Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'ParentId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'Type')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'类型,菜单=0,页面=1,权限=2'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Type'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'类型,菜单=0,页面=1,权限=2'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Type'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'Name')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权限名/菜单名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Name'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权限名/菜单名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Name'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'Url')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'菜单地址'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Url'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'菜单地址'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Url'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'Value')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Value'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Value'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'NeedAction')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'是否需要权限(仅页面有效)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'NeedAction'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'是否需要权限(仅页面有效)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'NeedAction'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'Icon')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'图标'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Icon'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'图标'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Icon'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Action', 
'COLUMN', N'Sort')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'排序'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Sort'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'排序'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'Sort'
GO

-- ----------------------------
-- Records of Base_Action
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_Action] ([Id], [CreateTime], [CreatorId], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1178957405992521728', N'2019-10-01 16:58:44.000', null, N'0', null, N'0', N'系统管理', N'', null, N'1', N'setting', N'1'), (N'1178957553778823168', N'2019-10-01 16:59:19.000', null, N'0', N'1178957405992521728', N'1', N'权限管理', N'/Base_Manage/Base_Action/List', null, N'1', null, N'20'), (N'1179018395304071168', N'2019-10-01 21:01:05.000', null, N'0', N'1178957405992521728', N'1', N'密钥管理', N'/Base_Manage/Base_AppSecret/List', null, N'1', null, N'15'), (N'1182652266117599232', N'2019-10-11 21:40:47.000', null, N'0', N'1178957405992521728', N'1', N'用户管理', N'/Base_Manage/Base_User/List', null, N'1', null, N'0'), (N'1182652367447789568', N'2019-10-11 21:41:11.000', null, N'0', N'1178957405992521728', N'1', N'角色管理', N'/Base_Manage/Base_Role/List', null, N'1', null, N'5'), (N'1182652433302556672', N'2019-10-11 21:41:27.000', null, N'0', N'1178957405992521728', N'1', N'部门管理', N'/Base_Manage/Base_Department/List', null, N'1', null, N'10'), , (N'1188801057778569216', N'2019-10-28 20:53:53.687', null, N'0', N'1182652367447789568', N'2', N'增', null, N'Base_Role.Add', N'1', null, N'0'), (N'1188801057778569217', N'2019-10-28 20:53:53.687', null, N'0', N'1182652367447789568', N'2', N'改', null, N'Base_Role.Edit', N'1', null, N'0'), (N'1188801057778569218', N'2019-10-28 20:53:53.687', null, N'0', N'1182652367447789568', N'2', N'删', null, N'Base_Role.Delete', N'1', null, N'0'), (N'1188801109783744512', N'2019-10-28 20:54:06.087', null, N'0', N'1182652433302556672', N'2', N'增', null, N'Base_Department.Add', N'1', null, N'0'), (N'1188801109783744513', N'2019-10-28 20:54:06.087', null, N'0', N'1182652433302556672', N'2', N'改', null, N'Base_Department.Edit', N'1', null, N'0'), (N'1188801109783744514', N'2019-10-28 20:54:06.087', null, N'0', N'1182652433302556672', N'2', N'删', null, N'Base_Department.Delete', N'1', null, N'0'), (N'1188801273885888512', N'2019-10-28 20:54:45.213', null, N'0', N'1179018395304071168', N'2', N'增', null, N'Base_AppSecret.Add', N'1', null, N'0'), (N'1188801273885888513', N'2019-10-28 20:54:45.213', null, N'0', N'1179018395304071168', N'2', N'改', null, N'Base_AppSecret.Edit', N'1', null, N'0'), (N'1188801273885888514', N'2019-10-28 20:54:45.213', null, N'0', N'1179018395304071168', N'2', N'删', null, N'Base_AppSecret.Delete', N'1', null, N'0'), (N'1188801341661646848', N'2019-10-28 20:55:01.370', null, N'0', N'1178957553778823168', N'2', N'增', null, N'Base_Action.Add', N'1', null, N'0'), (N'1188801341661646849', N'2019-10-28 20:55:01.370', null, N'0', N'1178957553778823168', N'2', N'改', null, N'Base_Action.Edit', N'1', null, N'0'), (N'1188801341661646850', N'2019-10-28 20:55:01.370', null, N'0', N'1178957553778823168', N'2', N'删', null, N'Base_Action.Delete', N'1', null, N'0'), (N'1193158266167758848', N'2019-11-09 21:27:53.000', N'Admin', N'0', null, N'0', N'首页', null, null, N'1', N'home', N'0'), (N'1193158630615027712', N'2019-11-09 21:29:20.013', N'Admin', N'0', N'1193158266167758848', N'1', N'框架介绍', N'/Home/Introduce', null, N'0', null, N'0'), (N'1193158780011941888', N'2019-11-09 21:29:55.630', N'Admin', N'0', N'1193158266167758848', N'1', N'运营统计', N'/Home/Statis', null, N'0', null, N'0'), (N'1193527101521661952', N'2019-11-10 21:53:30.320', null, N'0', N'1182652266117599232', N'2', N'增', null, N'Base_User.Add', N'1', null, N'0'), (N'1193527101521661953', N'2019-11-10 21:53:30.320', null, N'0', N'1182652266117599232', N'2', N'改', null, N'Base_User.Edit', N'1', null, N'0'), (N'1193527101521661954', N'2019-11-10 21:53:30.320', null, N'0', N'1182652266117599232', N'2', N'删', null, N'Base_User.Delete', N'1', null, N'0'), (N'1248570020770877440', N'2020-04-10 19:14:24.000', N'Admin', N'0', N'1178957405992521728', N'1', N'操作日志', N'/Base_Manage/Base_UserLog/List', null, N'0', null, N'22')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_AppSecret
-- ----------------------------
CREATE TABLE [Base_AppSecret] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
[AppId] varchar(50) NULL ,
[AppSecret] varchar(50) NULL ,
[AppName] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'应用密钥表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'应用密钥表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'Deleted'
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
INSERT INTO [Base_AppSecret] ([Id], [CreateTime], [CreatorId], [Deleted], [AppId], [AppSecret], [AppName]) VALUES (N'1172497995938271232', N'2019-09-13 21:11:20.000', N'Admin', N'0', N'PcAdmin', N'wtMaiTRPTT3hrf5e', N'后台AppId'), (N'1173937877642383360', N'2019-09-17 20:32:55.000', N'Admin', N'0', N'AppAdmin', N'IVh9LLSVFcoQPQ5K', N'APP密钥')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_BuildTest
-- ----------------------------
CREATE TABLE [Base_BuildTest] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
[Column1] varchar(50) NULL ,
[Column2] varchar(50) NULL ,
[Column3] varchar(50) NULL ,
[Column4] varchar(50) NULL ,
[Column5] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'生成测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'生成测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Deleted'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
'COLUMN', N'Column1')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'列1'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column1'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'列1'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column1'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
'COLUMN', N'Column2')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'列2'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column2'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'列2'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column2'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
'COLUMN', N'Column3')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'列3'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column3'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'列3'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column3'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
'COLUMN', N'Column4')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'列4'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column4'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'列4'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column4'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_BuildTest', 
'COLUMN', N'Column5')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'列5'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column5'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'列5'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_BuildTest'
, @level2type = 'COLUMN', @level2name = N'Column5'
GO

-- ----------------------------
-- Records of Base_BuildTest
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_BuildTest] ([Id], [CreateTime], [CreatorId], [Deleted], [Column1], [Column2], [Column3], [Column4], [Column5]) VALUES (N'1251534328014311424', N'2020-04-18 23:33:30.000', N'Admin', N'0', N'asdas', N'sadsa', N'sad', N'sadsa', N'sadsad')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_DbLink
-- ----------------------------
CREATE TABLE [Base_DbLink] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
[LinkName] varchar(50) NULL ,
[ConnectionStr] varchar(500) NULL ,
[DbType] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DbLink', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'数据库连接表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'数据库连接表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DbLink', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DbLink', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DbLink', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DbLink', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'Deleted'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DbLink', 
'COLUMN', N'LinkName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'连接名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'LinkName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'连接名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'LinkName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DbLink', 
'COLUMN', N'ConnectionStr')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'连接字符串'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'ConnectionStr'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'连接字符串'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'ConnectionStr'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DbLink', 
'COLUMN', N'DbType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'数据库类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'DbType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'数据库类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'DbType'
GO

-- ----------------------------
-- Records of Base_DbLink
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_DbLink] ([Id], [CreateTime], [CreatorId], [Deleted], [LinkName], [ConnectionStr], [DbType]) VALUES (N'1183373232498020352', N'2019-10-13 21:25:39.000', N'Admin', N'0', N'BaseDb', N'Data Source=.;Initial Catalog=Colder.Admin.AntdVue;Integrated Security=True', N'SqlServer')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_Department
-- ----------------------------
CREATE TABLE [Base_Department] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
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
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Department', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Department', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Department', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'Deleted'
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
INSERT INTO [Base_Department] ([Id], [CreateTime], [CreatorId], [Deleted], [Name], [ParentId]) VALUES (N'1181175685528424448', N'2019-10-07 19:53:23.000', null, N'0', N'宁波分公司', null), (N'1181175803631636480', N'2019-10-07 19:53:51.427', null, N'0', N'鄞州事业部', N'1181175685528424448'), (N'1181175865409540096', N'2019-10-07 19:54:06.000', null, N'0', N'海曙事业部', N'1181175685528424448')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_Role
-- ----------------------------
CREATE TABLE [Base_Role] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
[RoleName] nvarchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Role', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统角色表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统角色表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Role', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Role', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Role', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Role', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'Deleted'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Role', 
'COLUMN', N'RoleName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'角色名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'RoleName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'角色名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'RoleName'
GO

-- ----------------------------
-- Records of Base_Role
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_Role] ([Id], [CreateTime], [CreatorId], [Deleted], [RoleName]) VALUES (N'1251144116734005248', N'2020-04-17 21:42:57.220', N'Admin', N'0', N'超级管理员'), (N'1251145272742907904', N'2020-04-17 21:47:32.833', N'Admin', N'0', N'部门管理员')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_RoleAction
-- ----------------------------
CREATE TABLE [Base_RoleAction] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
[RoleId] varchar(50) NULL ,
[ActionId] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_RoleAction', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'角色权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'角色权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_RoleAction', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_RoleAction', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_RoleAction', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_RoleAction', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'Deleted'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_RoleAction', 
'COLUMN', N'RoleId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'RoleId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'RoleId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_RoleAction', 
'COLUMN', N'ActionId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权限Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'ActionId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权限Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'ActionId'
GO

-- ----------------------------
-- Records of Base_RoleAction
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_RoleAction] ([Id], [CreateTime], [CreatorId], [Deleted], [RoleId], [ActionId]) VALUES (N'1188801858282459136', N'2019-10-28 20:57:04.543', null, N'0', N'1180486275199668224', N'1182654049414025216'), (N'1188801858282459137', N'2019-10-28 20:57:04.543', null, N'0', N'1180486275199668224', N'1182654208411701248'), (N'1188801858282459138', N'2019-10-28 20:57:04.543', null, N'0', N'1180486275199668224', N'1183370665412005888'), (N'1188801984434540544', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188044797802188800'), (N'1188801984434540545', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188044797802188801'), (N'1188801984434540546', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1182652433302556672'), (N'1188801984434540547', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1178957405992521728'), (N'1188801984434540548', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188801109783744512'), (N'1188801984434540549', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188801109783744513'), (N'1188801984434540550', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188801109783744514'), (N'1188801984434540551', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1182652266117599232'), (N'1188801984434540552', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188800845714558976'), (N'1188801984434540553', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188800845714558977'), (N'1188801984434540554', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188800845714558978'), (N'1188801984434540555', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1182652367447789568'), (N'1188801984434540556', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188801057778569216'), (N'1188801984434540557', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188801057778569217'), (N'1188801984434540558', N'2019-10-28 20:57:34.620', null, N'0', N'1180819481383931904', N'1188801057778569218')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
CREATE TABLE [Base_User] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
[UserName] varchar(50) NULL ,
[Password] varchar(50) NULL ,
[RealName] nvarchar(50) NULL ,
[Sex] int NOT NULL DEFAULT ((0)) ,
[Birthday] date NULL ,
[DepartmentId] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统用户表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统用户表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Deleted'
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
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'RealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'姓名'
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
INSERT INTO [Base_User] ([Id], [CreateTime], [CreatorId], [Deleted], [UserName], [Password], [RealName], [Sex], [Birthday], [DepartmentId]) VALUES (N'Admin', N'2019-09-13 21:10:03.000', N'Admin', N'0', N'Admin', N'e10adc3949ba59abbe56e057f20f883e', N'超级管理员', N'1', N'2019-09-13', N'1181175685528424448')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_UserLog
-- ----------------------------
CREATE TABLE [Base_UserLog] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[CreatorRealName] nvarchar(50) NULL ,
[LogType] varchar(50) NULL ,
[LogContent] varchar(MAX) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserLog', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统日志表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统日志表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserLog', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserLog', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserLog', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserLog', 
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserLog', 
'COLUMN', N'LogType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'LogType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'LogType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserLog', 
'COLUMN', N'LogContent')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'LogContent'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserLog'
, @level2type = 'COLUMN', @level2name = N'LogContent'
GO

-- ----------------------------
-- Records of Base_UserLog
-- ----------------------------
BEGIN TRANSACTION
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_UserRole
-- ----------------------------
CREATE TABLE [Base_UserRole] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
[UserId] varchar(50) NULL ,
[RoleId] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserRole', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户角色表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户角色表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserRole', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserRole', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserRole', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserRole', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'Deleted'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserRole', 
'COLUMN', N'UserId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'UserId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'UserId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UserRole', 
'COLUMN', N'RoleId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'角色Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'RoleId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'角色Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'RoleId'
GO

-- ----------------------------
-- Records of Base_UserRole
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_UserRole] ([Id], [CreateTime], [CreatorId], [Deleted], [UserId], [RoleId]) VALUES (N'1181927367719784448', N'2019-10-09 21:40:18.270', null, N'0', N'1181922344629702656', N'1180819481383931904'), (N'1181927367719784449', N'2019-10-09 21:40:18.270', null, N'0', N'1181922344629702656', N'1180486275199668224'), (N'1181927783786352640', N'2019-10-09 21:41:57.470', null, N'0', N'1181927783727632384', N'1180819481383931904'), (N'1188802049190400000', N'2019-10-28 20:57:50.057', null, N'0', N'1181928860648738816', N'1180819481383931904'), (N'1251386547933024256', N'2020-04-18 13:46:17.323', null, N'0', N'1251386547391959040', N'1251144116734005248'), (N'1251423773970665472', N'2020-04-18 16:14:12.703', null, N'0', N'1251390402238353408', N'1251145272742907904'), (N'1251423773970665473', N'2020-04-18 16:14:12.703', null, N'0', N'1251390402238353408', N'1251144116734005248'), (N'1251518746514690048', N'2020-04-18 22:31:35.920', null, N'0', N'Admin', N'1251144116734005248')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Indexes structure for table Base_Action
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_Action
-- ----------------------------
ALTER TABLE [Base_Action] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_AppSecret
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE [Base_AppSecret] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_BuildTest
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_BuildTest
-- ----------------------------
ALTER TABLE [Base_BuildTest] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_DbLink
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_DbLink
-- ----------------------------
ALTER TABLE [Base_DbLink] ADD PRIMARY KEY ([Id])
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
-- Indexes structure for table Base_Role
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_Role
-- ----------------------------
ALTER TABLE [Base_Role] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_RoleAction
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_RoleAction
-- ----------------------------
ALTER TABLE [Base_RoleAction] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_User
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_User
-- ----------------------------
ALTER TABLE [Base_User] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_UserLog
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UserLog
-- ----------------------------
ALTER TABLE [Base_UserLog] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_UserRole
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UserRole
-- ----------------------------
ALTER TABLE [Base_UserRole] ADD PRIMARY KEY ([Id])
GO
