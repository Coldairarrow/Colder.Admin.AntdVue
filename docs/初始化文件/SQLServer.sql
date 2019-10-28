/*
Navicat SQL Server Data Transfer

Source Server         : .@SQLServer
Source Server Version : 105000
Source Host           : .:1433
Source Database       : Colder.Fx.Core.AdtdVue
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 105000
File Encoding         : 65001

Date: 2019-10-28 21:27:14
*/


-- ----------------------------
-- Table structure for Base_Action
-- ----------------------------
CREATE TABLE [Base_Action] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[CreatorRealName] nvarchar(50) NULL ,
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
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Action'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
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
INSERT INTO [Base_Action] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1178957405992521728', N'2019-10-01 16:58:44.000', null, null, N'0', null, N'0', N'系统管理', N'', null, N'1', N'setting', N'0'), (N'1178957553778823168', N'2019-10-01 16:59:19.000', null, null, N'0', N'1178957405992521728', N'1', N'权限管理', N'/Base_Manage/Base_Action/List', null, N'1', null, N'20'), (N'1179018395304071168', N'2019-10-01 21:01:05.000', null, null, N'0', N'1178957405992521728', N'1', N'密钥管理', N'/Base_Manage/Base_AppSecret/List', null, N'1', null, N'15'), (N'1182652266117599232', N'2019-10-11 21:40:47.000', null, null, N'0', N'1178957405992521728', N'1', N'用户管理', N'/Base_Manage/Base_User/List', null, N'1', null, N'0'), (N'1182652367447789568', N'2019-10-11 21:41:11.000', null, null, N'0', N'1178957405992521728', N'1', N'角色管理', N'/Base_Manage/Base_Role/List', null, N'1', null, N'5'), (N'1182652433302556672', N'2019-10-11 21:41:27.000', null, null, N'0', N'1178957405992521728', N'1', N'部门管理', N'/Base_Manage/Base_Department/List', null, N'1', null, N'10'), (N'1182652599069839360', N'2019-10-11 21:42:06.000', null, null, N'0', N'1178957405992521728', N'1', N'系统日志', N'/Base_Manage/Base_Log/List', null, N'1', null, N'25'), (N'1182654049414025216', N'2019-10-11 21:47:52.000', null, null, N'0', null, N'0', N'开发', null, null, N'1', N'code', N'999'), (N'1182654208411701248', N'2019-10-11 21:48:30.000', null, null, N'0', N'1182654049414025216', N'1', N'图标选择', N'/Develop/IconSelectorView', null, N'1', null, N'999'), (N'1183370665412005888', N'2019-10-13 21:15:27.000', N'Admin', N'超级管理员', N'0', N'1182654049414025216', N'1', N'数据库连接', N'/Base_Manage/Base_DbLink/List', null, N'1', null, N'0'), (N'1188800845714558976', N'2019-10-28 20:53:03.127', null, null, N'0', N'1182652266117599232', N'2', N'增', null, N'Base_User.Add', N'1', null, N'0'), (N'1188800845714558977', N'2019-10-28 20:53:03.127', null, null, N'0', N'1182652266117599232', N'2', N'改', null, N'Base_User.Edit', N'1', null, N'0'), (N'1188800845714558978', N'2019-10-28 20:53:03.127', null, null, N'0', N'1182652266117599232', N'2', N'删', null, N'Base_User.Delete', N'1', null, N'0'), (N'1188801057778569216', N'2019-10-28 20:53:53.687', null, null, N'0', N'1182652367447789568', N'2', N'增', null, N'Base_Role.Add', N'1', null, N'0'), (N'1188801057778569217', N'2019-10-28 20:53:53.687', null, null, N'0', N'1182652367447789568', N'2', N'改', null, N'Base_Role.Edit', N'1', null, N'0'), (N'1188801057778569218', N'2019-10-28 20:53:53.687', null, null, N'0', N'1182652367447789568', N'2', N'删', null, N'Base_Role.Delete', N'1', null, N'0'), (N'1188801109783744512', N'2019-10-28 20:54:06.087', null, null, N'0', N'1182652433302556672', N'2', N'增', null, N'Base_Department.Add', N'1', null, N'0'), (N'1188801109783744513', N'2019-10-28 20:54:06.087', null, null, N'0', N'1182652433302556672', N'2', N'改', null, N'Base_Department.Edit', N'1', null, N'0'), (N'1188801109783744514', N'2019-10-28 20:54:06.087', null, null, N'0', N'1182652433302556672', N'2', N'删', null, N'Base_Department.Delete', N'1', null, N'0'), (N'1188801273885888512', N'2019-10-28 20:54:45.213', null, null, N'0', N'1179018395304071168', N'2', N'增', null, N'Base_AppSecret.Add', N'1', null, N'0'), (N'1188801273885888513', N'2019-10-28 20:54:45.213', null, null, N'0', N'1179018395304071168', N'2', N'改', null, N'Base_AppSecret.Edit', N'1', null, N'0'), (N'1188801273885888514', N'2019-10-28 20:54:45.213', null, null, N'0', N'1179018395304071168', N'2', N'删', null, N'Base_AppSecret.Delete', N'1', null, N'0'), (N'1188801341661646848', N'2019-10-28 20:55:01.370', null, null, N'0', N'1178957553778823168', N'2', N'增', null, N'Base_Action.Add', N'1', null, N'0'), (N'1188801341661646849', N'2019-10-28 20:55:01.370', null, null, N'0', N'1178957553778823168', N'2', N'改', null, N'Base_Action.Edit', N'1', null, N'0'), (N'1188801341661646850', N'2019-10-28 20:55:01.370', null, null, N'0', N'1178957553778823168', N'2', N'删', null, N'Base_Action.Delete', N'1', null, N'0')
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
[CreatorRealName] nvarchar(50) NULL ,
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
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
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
INSERT INTO [Base_AppSecret] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [AppId], [AppSecret], [AppName]) VALUES (N'1172497995938271232', N'2019-09-13 21:11:20.850', N'Admin', N'超级管理员', N'0', N'PcAdmin', N'wtMaiTRPTT3hrf5e', N'后台AppId'), (N'1173937877642383360', N'2019-09-17 20:32:55.000', N'Admin', N'超级管理员', N'0', N'AppAdmin', N'IVh9LLSVFcoQPQ5K', N'APP密钥')
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
[CreatorRealName] nvarchar(50) NULL ,
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
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DbLink'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
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
INSERT INTO [Base_DbLink] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [LinkName], [ConnectionStr], [DbType]) VALUES (N'1183373232498020352', N'2019-10-13 21:25:39.000', N'Admin', N'超级管理员', N'0', N'BaseDb', N'Data Source=.;Initial Catalog=Colder.Fx.Core.AdtdVue;Integrated Security=True', N'SqlServer')
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
[CreatorRealName] nvarchar(50) NULL ,
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
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Department'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
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
INSERT INTO [Base_Department] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [Name], [ParentId]) VALUES (N'1181175685528424448', N'2019-10-07 19:53:23.000', null, null, N'0', N'宁波分公司', null), (N'1181175803631636480', N'2019-10-07 19:53:51.427', null, null, N'0', N'鄞州事业部', N'1181175685528424448'), (N'1181175865409540096', N'2019-10-07 19:54:06.000', null, null, N'0', N'海曙事业部', N'1181175685528424448')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_Log
-- ----------------------------
CREATE TABLE [Base_Log] (
[Id] varchar(50) NOT NULL ,
[CreateTime] datetime NOT NULL ,
[CreatorId] varchar(50) NULL ,
[CreatorRealName] nvarchar(50) NULL ,
[Deleted] bit NOT NULL DEFAULT ('false') ,
[Level] varchar(200) NULL ,
[LogType] varchar(50) NULL ,
[LogContent] varchar(MAX) NULL ,
[Data] nvarchar(MAX) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统日志表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统日志表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
'COLUMN', N'CreateTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'CreateTime'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
'COLUMN', N'CreatorId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'CreatorId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
'COLUMN', N'Deleted')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'Deleted'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'否已删除'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'Deleted'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
'COLUMN', N'Level')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志级别'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'Level'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志级别'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'Level'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
'COLUMN', N'LogType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'LogType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'LogType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
'COLUMN', N'LogContent')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'LogContent'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'LogContent'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_Log', 
'COLUMN', N'Data')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'数据备份（转为JSON字符串）'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'Data'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'数据备份（转为JSON字符串）'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Log'
, @level2type = 'COLUMN', @level2name = N'Data'
GO

-- ----------------------------
-- Records of Base_Log
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_Log] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [Level], [LogType], [LogContent], [Data]) VALUES (N'1180765533973254144', N'2019-10-06 16:43:35.503', null, null, N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1180765921203982336', N'2019-10-06 16:45:07.813', null, null, N'0', N'Info', N'系统角色管理', N'删除角色:部门管理员', N'[{"Id":"1180668541095907328","CreateTime":"2019-10-06 10:18:10","CreatorId":null,"CreatorRealName":null,"Deleted":false,"RoleName":"部门管理员"}]'), (N'1180766173067743232', N'2019-10-06 16:46:07.890', null, null, N'0', N'Info', N'系统角色管理', N'添加角色:sada', null), (N'1180800131927117824', N'2019-10-06 19:01:04.297', null, null, N'0', N'Info', N'系统角色管理', N'删除角色:sada', N'[{"Id":"1180766161386606592","CreateTime":"2019-10-06 16:46:05","CreatorId":null,"CreatorRealName":null,"Deleted":false,"RoleName":"sada"}]'), (N'1180808892918009856', N'2019-10-06 19:35:53.080', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"zxz"}
', null), (N'1180809208111566848', N'2019-10-06 19:37:08.247', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809213983592448', N'2019-10-06 19:37:09.647', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809215162191872', N'2019-10-06 19:37:09.927', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809215724228608', N'2019-10-06 19:37:10.060', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809216307236864', N'2019-10-06 19:37:10.200', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809216894439424', N'2019-10-06 19:37:10.340', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809217422921728', N'2019-10-06 19:37:10.467', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809218052067328', N'2019-10-06 19:37:10.617', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809218626686976', N'2019-10-06 19:37:10.753', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809219171946496', N'2019-10-06 19:37:10.883', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809219754954752', N'2019-10-06 19:37:11.020', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809220296019968', N'2019-10-06 19:37:11.150', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809220828696576', N'2019-10-06 19:37:11.277', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809221986324480', N'2019-10-06 19:37:11.553', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809222535778304', N'2019-10-06 19:37:11.683', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809223076843520', N'2019-10-06 19:37:11.813', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809223680823296', N'2019-10-06 19:37:11.957', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809224737787904', N'2019-10-06 19:37:12.210', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809225278853120', N'2019-10-06 19:37:12.340', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809225866055680', N'2019-10-06 19:37:12.477', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809226482618368', N'2019-10-06 19:37:12.627', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809227082403840', N'2019-10-06 19:37:12.767', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809227682189312', N'2019-10-06 19:37:12.910', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809228298752000', N'2019-10-06 19:37:13.057', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809228923703296', N'2019-10-06 19:37:13.207', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809229477351424', N'2019-10-06 19:37:13.340', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809230077136896', N'2019-10-06 19:37:13.483', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809230614007808', N'2019-10-06 19:37:13.610', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809231180238848', N'2019-10-06 19:37:13.747', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809231771635712', N'2019-10-06 19:37:13.887', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809232333672448', N'2019-10-06 19:37:14.020', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809232904097792', N'2019-10-06 19:37:14.157', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809235114496000', N'2019-10-06 19:37:14.683', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809235752030208', N'2019-10-06 19:37:14.837', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809236335038464', N'2019-10-06 19:37:14.973', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809236943212544', N'2019-10-06 19:37:15.120', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809237559775232', N'2019-10-06 19:37:15.267', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809238142783488', N'2019-10-06 19:37:15.407', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809241225596928', N'2019-10-06 19:37:16.140', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809243356303360', N'2019-10-06 19:37:16.650', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809244455211008', N'2019-10-06 19:37:16.910', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809245126299648', N'2019-10-06 19:37:17.070', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809245705113600', N'2019-10-06 19:37:17.210', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809246338453504', N'2019-10-06 19:37:17.360', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809246934044672', N'2019-10-06 19:37:17.503', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809247571578880', N'2019-10-06 19:37:17.653', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809248142004224', N'2019-10-06 19:37:17.790', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809248745984000', N'2019-10-06 19:37:17.933', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809249349963776', N'2019-10-06 19:37:18.077', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180809249899417600', N'2019-10-06 19:37:18.207', null, null, N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    11111111
  位置:
       at Coldairarrow.Api.Controllers.Base_Manage.Base_RoleController.SaveData(Base_Role theData, String actionsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_RoleController.cs:line 69


url:http://localhost:40000/Base_Manage/Base_Role/SaveData
body:{"RoleName":"aa"}
', null), (N'1180819481866276864', N'2019-10-06 20:17:57.687', null, null, N'0', N'Info', N'系统角色管理', N'添加角色:部门管理员', null), (N'1181161020341620736', N'2019-10-07 18:55:06.800', null, null, N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1181175687411666944', N'2019-10-07 19:53:23.707', null, null, N'0', N'Info', N'部门管理', N'添加部门名:宁波市', null), (N'1181175775366221824', N'2019-10-07 19:53:44.690', null, null, N'0', N'Info', N'部门管理', N'修改部门名:宁波分公司', null), (N'1181175803677773824', N'2019-10-07 19:53:51.440', null, null, N'0', N'Info', N'部门管理', N'添加部门名:鄞州事业部', null), (N'1181175865455677440', N'2019-10-07 19:54:06.167', null, null, N'0', N'Info', N'部门管理', N'添加部门名:海曙事业部', null), (N'1181175990177501184', N'2019-10-07 19:54:35.903', null, null, N'0', N'Info', N'部门管理', N'添加部门名:象山事业部', null), (N'1181176264753418240', N'2019-10-07 19:55:41.367', null, null, N'0', N'Info', N'部门管理', N'修改部门名:海曙事业部', null), (N'1181177169729032192', N'2019-10-07 19:59:17.130', null, null, N'0', N'Info', N'部门管理', N'删除部门名:象山事业部', N'[{"Id":"1181175990127169536","CreateTime":"2019-10-07 19:54:35","CreatorId":null,"CreatorRealName":null,"Deleted":false,"Name":"象山事业部","ParentId":"1181175685528424448"}]'), (N'1181206879452270592', N'2019-10-07 21:57:20.467', null, null, N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1181919153460613120', N'2019-10-09 21:07:39.747', null, N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1181922344835223552', N'2019-10-09 21:20:20.713', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'添加用户:小明', null), (N'1181922529548177408', N'2019-10-09 21:21:04.757', null, N'xiaoming', N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    Error converting value "1180819481383931904" to type ''System.Collections.Generic.List`1[System.String]''. Path '''', line 1, position 21.
  位置:
       at Coldairarrow.Util.Extention.ToList[T](String jsonStr) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Util\Extention\Extention.String.cs:line 434
       at Coldairarrow.Api.Controllers.Base_Manage.Base_UserController.SaveData(Base_User theData, String newPwd, String roleIdsJson) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_UserController.cs:line 55


2层错误:
  消息:
    Could not cast or convert from System.String to System.Collections.Generic.List`1[System.String].
  位置:
    无

url:http://localhost:40000/Base_Manage/Base_User/SaveData
body:{"RoleNames":"","RoleIdList":"1180819481383931904","RoleNameList":[],"RoleType":0,"DepartmentName":"鄞州事业部","SexText":"女","Id":"1181922344629702656","CreateTime":"2019-10-09 21:20:20","CreatorId":null,"CreatorRealName":"超级管理员","Deleted":false,"UserName":"xiaoming","Password":"e10adc3949ba59abbe56e057f20f883e","RealName":"小明","Sex":0,"Birthday":"2019-10-17T16:00:00.000Z","DepartmentId":"1181175803631636480","roleIdsJson":"\"1180819481383931904\""}
', null), (N'1181923586319847424', N'2019-10-09 21:25:16.690', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181923754964422656', N'2019-10-09 21:25:56.920', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181923836560412672', N'2019-10-09 21:26:16.373', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181924671881220096', N'2019-10-09 21:29:35.530', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181924880178745344', N'2019-10-09 21:30:25.190', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181925235671175168', N'2019-10-09 21:31:49.923', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181925621169655808', N'2019-10-09 21:33:21.857', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181925659098746880', N'2019-10-09 21:33:30.900', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181925705865236480', N'2019-10-09 21:33:42.050', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181926201950736384', N'2019-10-09 21:35:40.327', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181926225086517248', N'2019-10-09 21:35:45.843', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181926547053875200', N'2019-10-09 21:37:02.607', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181926566779686912', N'2019-10-09 21:37:07.310', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181926589508620288', N'2019-10-09 21:37:12.727', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181927320219291648', N'2019-10-09 21:40:06.923', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181927367820447744', N'2019-10-09 21:40:18.290', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'修改用户:小明', null), (N'1181927667742543872', N'2019-10-09 21:41:29.800', null, N'Admin', N'0', N'Info', N'系统用户管理', N'删除用户:小明', N'[{"Id":"1181922344629702656","CreateTime":"2019-10-09 21:20:20","CreatorId":null,"CreatorRealName":"超级管理员","Deleted":false,"UserName":"xiaoming","Password":"e10adc3949ba59abbe56e057f20f883e","RealName":"小明","Sex":0,"Birthday":"2019-10-09 00:00:00","DepartmentId":"1181175803631636480"}]'), (N'1181927783870238720', N'2019-10-09 21:41:57.480', null, N'xiaoming', N'0', N'Info', N'系统用户管理', N'添加用户:小明', null), (N'1181927929332895744', N'2019-10-09 21:42:32.167', null, N'Admin', N'0', N'Info', N'系统用户管理', N'删除用户:小明', N'[{"Id":"1181927783727632384","CreateTime":"2019-10-09 21:41:57","CreatorId":null,"CreatorRealName":"超级管理员","Deleted":false,"UserName":"xiaoming","Password":"e10adc3949ba59abbe56e057f20f883e","RealName":"小明","Sex":1,"Birthday":"2019-10-09 00:00:00","DepartmentId":"1181175803631636480"}]'), (N'1181929563970605056', N'2019-10-09 21:49:01.883', null, null, N'0', N'Info', N'系统用户管理', N'修改用户:小花', null), (N'1181929575056150528', N'2019-10-09 21:49:04.540', null, null, N'0', N'Info', N'系统用户管理', N'修改用户:小花', null), (N'1181929991655395328', N'2019-10-09 21:50:43.843', null, null, N'0', N'Info', N'系统用户管理', N'修改用户:小花', null), (N'1181930432216698880', N'2019-10-09 21:51:58.817', null, null, N'0', N'Info', N'系统用户管理', N'修改用户:小花', null), (N'1181930473392181248', N'2019-10-09 21:52:36.127', null, null, N'0', N'Info', N'系统用户管理', N'修改用户:小花', null), (N'1181930822769315840', N'2019-10-09 21:53:52.180', null, null, N'0', N'Info', N'系统用户管理', N'修改用户:小花', null), (N'1183360538407604224', N'2019-10-13 20:35:12.773', N'Admin', N'Admin', N'0', N'Info', N'系统用户管理', N'添加用户:aa', null), (N'1183360648063488000', N'2019-10-13 20:35:38.930', N'Admin', N'Admin', N'0', N'Info', N'系统用户管理', N'删除用户:aa', N'[{"Id":"1183360537623269376","CreateTime":"2019-10-13 20:35:12","CreatorId":"Admin","CreatorRealName":"超级管理员","Deleted":false,"UserName":"zxz","Password":null,"RealName":"aa","Sex":0,"Birthday":null,"DepartmentId":null}]'), (N'1183363222611169280', N'2019-10-13 20:45:52.737', N'Admin', N'Admin', N'0', N'Info', N'系统用户管理', N'添加用户:aaa', null), (N'1183372597610418176', N'2019-10-13 21:23:07.913', N'Admin', N'Admin', N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    An error occurred while updating the entries. See the inner exception for details.
  位置:
       at Coldairarrow.DataRepository.RepositoryDbContext.SaveChanges() in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\DbContext\RepositoryDbContext.cs:line 93
       at Coldairarrow.DataRepository.DbRepository.CommitDb() in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 217
       at Coldairarrow.DataRepository.DbRepository.PackWork(IEnumerable`1 entityTypes, Action work) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 94
       at Coldairarrow.DataRepository.DbRepository.Insert(List`1 entities) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 306
       at Coldairarrow.DataRepository.DbRepository.Insert[T](T entity) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 287
       at Coldairarrow.Business.BaseBusiness`1.Insert(T entity) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Business\04Business\BaseBusiness.T.cs:line 157
       at Coldairarrow.Business.Base_Manage.Base_DbLinkBusiness.AddData(Base_DbLink newData) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Business\04Business\Base_Manage\Base_DbLinkBusiness.cs:line 33
       at Coldairarrow.Util.Interceptor.Intercept(IInvocation invocation) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Util\DI\Interceptor.cs:line 27
       at Coldairarrow.Api.Controllers.Base_Manage.Base_DbLinkController.SaveData(Base_DbLink theData) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_DbLinkController.cs:line 61


2层错误:
  消息:
    将截断字符串或二进制数据。
语句已终止。
  位置:
    无

url:http://localhost:40000/Base_Manage/Base_DbLink/SaveData
body:{"DbType":"SqlServer","LinkName":"BaseDb","ConnectionStr":"Data Source=.;Initial Catalog=Colder.Fx.Core.AdtdVue;Integrated Security=True"}
', null), (N'1183372960069586944', N'2019-10-13 21:24:34.340', N'Admin', N'Admin', N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    An error occurred while updating the entries. See the inner exception for details.
  位置:
       at Coldairarrow.DataRepository.RepositoryDbContext.SaveChanges() in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\DbContext\RepositoryDbContext.cs:line 93
       at Coldairarrow.DataRepository.DbRepository.CommitDb() in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 217
       at Coldairarrow.DataRepository.DbRepository.PackWork(IEnumerable`1 entityTypes, Action work) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 94
       at Coldairarrow.DataRepository.DbRepository.Insert(List`1 entities) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 306
       at Coldairarrow.DataRepository.DbRepository.Insert[T](T entity) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 287
       at Coldairarrow.Business.BaseBusiness`1.Insert(T entity) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Business\04Business\BaseBusiness.T.cs:line 157
       at Coldairarrow.Business.Base_Manage.Base_DbLinkBusiness.AddData(Base_DbLink newData) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Business\04Business\Base_Manage\Base_DbLinkBusiness.cs:line 33
       at Coldairarrow.Util.Interceptor.Intercept(IInvocation invocation) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Util\DI\Interceptor.cs:line 27
       at Coldairarrow.Api.Controllers.Base_Manage.Base_DbLinkController.SaveData(Base_DbLink theData) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_DbLinkController.cs:line 61


2层错误:
  消息:
    将截断字符串或二进制数据。
语句已终止。
  位置:
    无

url:http://localhost:40000/Base_Manage/Base_DbLink/SaveData
body:{"DbType":"SqlServer","LinkName":"BaseDb","ConnectionStr":"Data Source=.;Initial Catalog=Colder.Fx.Core.AdtdVue;Integrated Security=True"}
', null), (N'1183373811987255296', N'2019-10-13 21:27:57.457', N'Admin', N'Admin', N'0', N'Error', N'系统异常', N'
1层错误:
  消息:
    列名 ''SortNum'' 无效。
  位置:
       at Coldairarrow.Business.Base_Manage.Base_DbLinkBusiness.GetDataList(Pagination pagination) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Business\04Business\Base_Manage\Base_DbLinkBusiness.cs:line 14
       at Coldairarrow.Util.Interceptor.Intercept(IInvocation invocation) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Util\DI\Interceptor.cs:line 27
       at Coldairarrow.Api.Controllers.Base_Manage.Base_DbLinkController.GetDataList(Pagination pagination) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdtdVue\src\Coldairarrow.Api\Controllers\Base_Manage\Base_DbLinkController.cs:line 32


url:http://localhost:40000/Base_Manage/Base_DbLink/GetDataList
body:{"PageIndex":1,"PageRows":10,"SortField":"Id","SortType":"asc"}
', null), (N'1183377076766380032', N'2019-10-13 21:40:55.820', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null)
GO
GO
INSERT INTO [Base_Log] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [Level], [LogType], [LogContent], [Data]) VALUES (N'1183377103635091456', N'2019-10-13 21:41:02.247', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185739043120353280', N'2019-10-20 10:06:32.450', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1185739109860118528', N'2019-10-20 10:06:48.410', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1185739344275574784', N'2019-10-20 10:07:44.300', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1185739516615331840', N'2019-10-20 10:08:25.390', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1185741121528008704', N'2019-10-20 10:14:48.030', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185741149193637888', N'2019-10-20 10:14:54.627', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1185741199168770048', N'2019-10-20 10:15:06.543', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185741221356638208', N'2019-10-20 10:15:11.833', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185741248384733184', N'2019-10-20 10:15:18.277', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185741264620883968', N'2019-10-20 10:15:22.147', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185741403456540672', N'2019-10-20 10:15:55.247', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185742814831448064', N'2019-10-20 10:21:31.747', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185742832837595136', N'2019-10-20 10:21:36.040', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185742840290873344', N'2019-10-20 10:21:37.817', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185742862499713024', N'2019-10-20 10:21:43.110', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185742878610034688', N'2019-10-20 10:21:46.953', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185742904048488448', N'2019-10-20 10:21:53.017', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185742925879840768', N'2019-10-20 10:21:58.220', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1185744782094241792', N'2019-10-20 10:29:20.777', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1186259513032839168', N'2019-10-21 20:34:42.090', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1187731887334887424', N'2019-10-25 22:05:23.567', N'Admin', N'Admin', N'0', N'Info', N'系统用户管理', N'修改用户:小花', null), (N'1187731989378109440', N'2019-10-25 22:05:47.913', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188046981080027136', N'2019-10-26 18:57:27.670', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188047050097299456', N'2019-10-26 18:57:44.243', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188047062839595008', N'2019-10-26 18:57:47.283', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188047082296971264', N'2019-10-26 18:57:51.920', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188047203881455616', N'2019-10-26 18:58:20.907', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188047392084070400', N'2019-10-26 18:59:05.780', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188047514490638336', N'2019-10-26 18:59:34.963', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188047657394769920', N'2019-10-26 19:00:09.033', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188050329095114752', N'2019-10-26 19:10:46.017', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188050345704558592', N'2019-10-26 19:10:49.977', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188050376155205632', N'2019-10-26 19:10:57.237', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188050431624876032', N'2019-10-26 19:11:10.463', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188050502072406016', N'2019-10-26 19:11:27.260', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188051312969781248', N'2019-10-26 19:14:40.593', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188051355940425728', N'2019-10-26 19:14:50.837', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188051447837626368', N'2019-10-26 19:15:12.747', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188051487071145984', N'2019-10-26 19:15:22.103', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188051686057316352', N'2019-10-26 19:16:09.543', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188055526169120768', N'2019-10-26 19:31:25.097', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188055640682008576', N'2019-10-26 19:31:52.397', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188055771363938304', N'2019-10-26 19:32:23.557', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188055801307074560', N'2019-10-26 19:32:30.693', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188056203993812992', N'2019-10-26 19:34:06.453', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188057317082402816', N'2019-10-26 19:38:31.830', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188057363421073408', N'2019-10-26 19:38:43.130', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188057453753798656', N'2019-10-26 19:39:04.670', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188057498569936896', N'2019-10-26 19:39:15.353', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188058124985044992', N'2019-10-26 19:41:44.703', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188058296393666560', N'2019-10-26 19:42:25.570', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188058375800229888', N'2019-10-26 19:42:44.503', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188058403834957824', N'2019-10-26 19:42:51.187', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188063507766054912', N'2019-10-26 20:03:08.060', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188063554066976768', N'2019-10-26 20:03:19.097', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188063608412573696', N'2019-10-26 20:03:32.057', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188063788029448192', N'2019-10-26 20:04:14.877', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188063820673716224', N'2019-10-26 20:04:22.660', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188063924428214272', N'2019-10-26 20:04:47.397', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188063977695875072', N'2019-10-26 20:05:00.100', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188064005051125760', N'2019-10-26 20:05:06.620', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188064082951933952', N'2019-10-26 20:05:25.193', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188064517880287232', N'2019-10-26 20:07:08.887', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188065666679181312', N'2019-10-26 20:11:42.783', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188065680616853504', N'2019-10-26 20:11:46.107', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188065711893778432', N'2019-10-26 20:11:53.563', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188065725420408832', N'2019-10-26 20:11:56.787', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188065738892513280', N'2019-10-26 20:12:00.000', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188065755220938752', N'2019-10-26 20:12:03.893', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188065770815361024', N'2019-10-26 20:12:07.613', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188065789953970176', N'2019-10-26 20:12:12.173', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188068980368084992', N'2019-10-26 20:24:52.827', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188069000106479616', N'2019-10-26 20:24:57.533', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188075611466240000', N'2019-10-26 20:51:13.807', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188076129982877696', N'2019-10-26 20:53:17.430', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188077467886161920', N'2019-10-26 20:58:36.410', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188277587219058688', N'2019-10-27 10:13:48.480', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188801821116731392', N'2019-10-28 20:56:55.663', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188801858408288256', N'2019-10-28 20:57:04.570', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:超级管理员', null), (N'1188801941426147328', N'2019-10-28 20:57:24.360', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188801984497455104', N'2019-10-28 20:57:34.633', N'Admin', N'Admin', N'0', N'Info', N'系统角色管理', N'修改角色:部门管理员', null), (N'1188802031159087104', N'2019-10-28 20:57:45.753', N'Admin', N'Admin', N'0', N'Info', N'系统用户管理', N'修改用户:小花', null), (N'1188802049257508864', N'2019-10-28 20:57:50.070', N'Admin', N'Admin', N'0', N'Info', N'系统用户管理', N'修改用户:小花', null), (N'1188808476202110976', N'2019-10-28 21:23:22.360', N'Admin', N'Admin', N'0', N'Info', N'接口密钥管理', N'修改应用Id:AppAdmin', null)
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
[CreatorRealName] nvarchar(50) NULL ,
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
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_Role'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
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
INSERT INTO [Base_Role] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [RoleName]) VALUES (N'1180486275199668224', N'2019-10-05 22:13:55.000', null, null, N'0', N'超级管理员'), (N'1180819481383931904', N'2019-10-06 20:17:57.000', null, null, N'0', N'部门管理员')
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
[CreatorRealName] nvarchar(50) NULL ,
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
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_RoleAction'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
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
INSERT INTO [Base_RoleAction] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [RoleId], [ActionId]) VALUES (N'1188801858282459136', N'2019-10-28 20:57:04.543', null, null, N'0', N'1180486275199668224', N'1182654049414025216'), (N'1188801858282459137', N'2019-10-28 20:57:04.543', null, null, N'0', N'1180486275199668224', N'1182654208411701248'), (N'1188801858282459138', N'2019-10-28 20:57:04.543', null, null, N'0', N'1180486275199668224', N'1183370665412005888'), (N'1188801984434540544', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188044797802188800'), (N'1188801984434540545', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188044797802188801'), (N'1188801984434540546', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1182652433302556672'), (N'1188801984434540547', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1178957405992521728'), (N'1188801984434540548', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188801109783744512'), (N'1188801984434540549', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188801109783744513'), (N'1188801984434540550', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188801109783744514'), (N'1188801984434540551', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1182652266117599232'), (N'1188801984434540552', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188800845714558976'), (N'1188801984434540553', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188800845714558977'), (N'1188801984434540554', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188800845714558978'), (N'1188801984434540555', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1182652367447789568'), (N'1188801984434540556', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188801057778569216'), (N'1188801984434540557', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188801057778569217'), (N'1188801984434540558', N'2019-10-28 20:57:34.620', null, null, N'0', N'1180819481383931904', N'1188801057778569218')
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
[CreatorRealName] nvarchar(50) NULL ,
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
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
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
INSERT INTO [Base_User] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [UserName], [Password], [RealName], [Sex], [Birthday], [DepartmentId]) VALUES (N'1181928860648738816', N'2019-10-09 21:46:14.000', null, N'超级管理员', N'0', N'xiaohua', N'e10adc3949ba59abbe56e057f20f883e', N'小花', N'0', null, null), (N'1183363221872971776', N'2019-10-13 20:45:52.577', N'Admin', N'超级管理员', N'0', N'aa', null, N'aaa', N'0', null, null), (N'Admin', N'2019-09-13 21:10:03.000', N'Admin', N'超级管理员', N'0', N'Admin', N'e10adc3949ba59abbe56e057f20f883e', N'超级管理员', N'1', N'2019-09-13', null)
GO
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
[CreatorRealName] nvarchar(50) NULL ,
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
'COLUMN', N'CreatorRealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'创建人姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UserRole'
, @level2type = 'COLUMN', @level2name = N'CreatorRealName'
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
INSERT INTO [Base_UserRole] ([Id], [CreateTime], [CreatorId], [CreatorRealName], [Deleted], [UserId], [RoleId]) VALUES (N'1181927367719784448', N'2019-10-09 21:40:18.270', null, null, N'0', N'1181922344629702656', N'1180819481383931904'), (N'1181927367719784449', N'2019-10-09 21:40:18.270', null, null, N'0', N'1181922344629702656', N'1180486275199668224'), (N'1181927783786352640', N'2019-10-09 21:41:57.470', null, null, N'0', N'1181927783727632384', N'1180819481383931904'), (N'1188802049190400000', N'2019-10-28 20:57:50.057', null, null, N'0', N'1181928860648738816', N'1180819481383931904')
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
-- Indexes structure for table Base_Log
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_Log
-- ----------------------------
ALTER TABLE [Base_Log] ADD PRIMARY KEY ([Id])
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
-- Indexes structure for table Base_UserRole
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UserRole
-- ----------------------------
ALTER TABLE [Base_UserRole] ADD PRIMARY KEY ([Id])
GO
