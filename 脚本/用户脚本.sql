--SELECT   
--    f.name AS foreign_key_name  
--   ,OBJECT_NAME(f.parent_object_id) AS table_name  
--   ,COL_NAME(fc.parent_object_id, fc.parent_column_id) AS constraint_column_name  
--   ,OBJECT_NAME (f.referenced_object_id) AS referenced_object  
--   ,COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name  
--   ,is_disabled  
--   ,delete_referential_action_desc  
--   ,update_referential_action_desc  
--FROM sys.foreign_keys AS f  
--INNER JOIN sys.foreign_key_columns AS fc   
--   ON f.object_id = fc.constraint_object_id   
--WHERE f.parent_object_id = OBJECT_ID('TUserRole');

--新增用户
INSERT INTO TUser (Id, TenanId, IsDelete, Account, DisplayName, PasswordHash, Email, EmailConfirmed, Phone, PhoneConfirmed, LockoutEnd, LockoutEnabled)
VALUES('99BF7D96-BCD5-4083-8197-BFFBEBE4501F','8CAB1C61-A36B-4910-B8EC-7351EA11BA71','00000000-0000-0000-0000-000000000000','admin','管理员','202CB962AC59075B964B07152D234B70','1317932489@qq.com',1,'110',1,dateAdd(yy,100,getdate()),0)
--新增角色
INSERT INTO  dbo.TRole(Id, TenanId, IsDelete, Name, DisplayName, Remark)
VALUES('39BAFB2B-1A48-47D3-A668-2EA2E384CD33','8CAB1C61-A36B-4910-B8EC-7351EA11BA71','00000000-0000-0000-0000-000000000000','admintest','测试管理员','测试用')

INSERT INTO  dbo.TRole(Id, TenanId, IsDelete, Name, DisplayName, Remark)
VALUES('14961444-0DF0-4222-A4EA-0A8CE2AF1019','8CAB1C61-A36B-4910-B8EC-7351EA11BA71','00000000-0000-0000-0000-000000000000','usertest','测试用户','测试用')
--新增UserRole
INSERT INTO dbo.TUserRole(Id, TenanId, IsDelete, UserId, RoleId, RowNo)
VALUES(NEWID(),'8CAB1C61-A36B-4910-B8EC-7351EA11BA71','00000000-0000-0000-0000-000000000000','99BF7D96-BCD5-4083-8197-BFFBEBE4501F','39BAFB2B-1A48-47D3-A668-2EA2E384CD33',0 )

INSERT INTO dbo.TUserRole(Id, TenanId, IsDelete, UserId, RoleId, RowNo)
VALUES(NEWID(),'8CAB1C61-A36B-4910-B8EC-7351EA11BA71','00000000-0000-0000-0000-000000000000','99BF7D96-BCD5-4083-8197-BFFBEBE4501F','14961444-0DF0-4222-A4EA-0A8CE2AF1019',0 )
--新增菜单
INSERT into dbo.TModuleType(Id, TenanId, IsDelete, ParentId, Name, Path, DisplayName, Description)
VALUES('7F760025-821A-47D0-976C-0D3ECB0F7412', -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
NULL, -- ParentId - uniqueidentifier
'Manage' , -- Name - nvarchar(50)
'' , -- Path - nvarchar(500)
'后台管理' , -- DisplayName - nvarchar(50)
N'' -- Description - nvarchar(500)
    )

INSERT into dbo.TModuleType(Id, TenanId, IsDelete, ParentId, Name, Path, DisplayName, Description)
VALUES('94CC5387-667F-4360-946C-6BF7F5CECE44', -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'7F760025-821A-47D0-976C-0D3ECB0F7412', -- ParentId - uniqueidentifier
'VehicleManage' , -- Name - nvarchar(50)
'' , -- Path - nvarchar(500)
'车辆管理' , -- DisplayName - nvarchar(50)
N'' -- Description - nvarchar(500)
    )

INSERT into dbo.TModuleType(Id, TenanId, IsDelete, ParentId, Name, Path, DisplayName, Description)
VALUES('846D23C9-FF0E-411F-9D38-EBDC4F99549E', -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'7F760025-821A-47D0-976C-0D3ECB0F7412', -- ParentId - uniqueidentifier
'DriverManage' , -- Name - nvarchar(50)
'' , -- Path - nvarchar(500)
'司机管理' , -- DisplayName - nvarchar(50)
N'' -- Description - nvarchar(500)
    )

INSERT into dbo.TModuleType(Id, TenanId, IsDelete, ParentId, Name, Path, DisplayName, Description)
VALUES('F2D3F712-4082-428D-AE6B-67FD6B384194', -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'7F760025-821A-47D0-976C-0D3ECB0F7412', -- ParentId - uniqueidentifier
'UserManage' , -- Name - nvarchar(50)
'' , -- Path - nvarchar(500)
'用户管理' , -- DisplayName - nvarchar(50)
N'' -- Description - nvarchar(500)
    )

----------------------
INSERT into dbo.TRoleModule(Id, TenanId, IsDelete, RoleId, ModuleId, RowNo)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'39BAFB2B-1A48-47D3-A668-2EA2E384CD33', -- RoleId - uniqueidentifier
'7F760025-821A-47D0-976C-0D3ECB0F7412', -- ModuleId - uniqueidentifier
0   -- RowNo - int
    )
INSERT into dbo.TRoleModule(Id, TenanId, IsDelete, RoleId, ModuleId, RowNo)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'39BAFB2B-1A48-47D3-A668-2EA2E384CD33', -- RoleId - uniqueidentifier
'94CC5387-667F-4360-946C-6BF7F5CECE44', -- ModuleId - uniqueidentifier
1   -- RowNo - int
    )

INSERT into dbo.TRoleModule(Id, TenanId, IsDelete, RoleId, ModuleId, RowNo)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'39BAFB2B-1A48-47D3-A668-2EA2E384CD33', -- RoleId - uniqueidentifier
'846D23C9-FF0E-411F-9D38-EBDC4F99549E', -- ModuleId - uniqueidentifier
1   -- RowNo - int
    )

INSERT into dbo.TRoleModule(Id, TenanId, IsDelete, RoleId, ModuleId, RowNo)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'39BAFB2B-1A48-47D3-A668-2EA2E384CD33', -- RoleId - uniqueidentifier
'7F760025-821A-47D0-976C-0D3ECB0F7412', -- ModuleId - uniqueidentifier
1   -- RowNo - int
    )
	
----插入TPermissionType
SELECT * FROM	TPermissionType
INSERT INTO dbo.TPermissionType(Id, TenanId, IsDelete, ModuleId, Name, DisplayName, Remark, ApiPath, RowNo)
VALUES('44476BEE-FFDC-4A0C-A237-B9563A958F03', -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'94CC5387-667F-4360-946C-6BF7F5CECE44', -- ModuleId - uniqueidentifier
'VehicleList' , -- Name - nvarchar(50)
'获取车辆列表' , -- DisplayName - nvarchar(50)
N'' , -- Remark - nvarchar(50)
N'/Vehicle/List' , -- ApiPath - nvarchar(50)
0   -- RowNo - int
    )

INSERT INTO dbo.TPermissionType(Id, TenanId, IsDelete, ModuleId, Name, DisplayName, Remark, ApiPath, RowNo)
VALUES('06843524-0C68-40FE-A84C-862593123D27', -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'94CC5387-667F-4360-946C-6BF7F5CECE44', -- ModuleId - uniqueidentifier
'VehicleItme' , -- Name - nvarchar(50)
'获取车辆信息' , -- DisplayName - nvarchar(50)
N'' , -- Remark - nvarchar(50)
N'/Vehicle/Item' , -- ApiPath - nvarchar(50)
1   -- RowNo - int
    )

INSERT INTO dbo.TPermissionType(Id, TenanId, IsDelete, ModuleId, Name, DisplayName, Remark, ApiPath, RowNo)
VALUES('61FCBF22-E33B-47AF-B95D-6ED96F2A8415', -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'94CC5387-667F-4360-946C-6BF7F5CECE44', -- ModuleId - uniqueidentifier
'VehicleAdd' , -- Name - nvarchar(50)
'新增车辆信息' , -- DisplayName - nvarchar(50)
N'' , -- Remark - nvarchar(50)
N'/Vehicle/Item' , -- ApiPath - nvarchar(50)
2   -- RowNo - int
    )

INSERT INTO dbo.TPermissionType(Id, TenanId, IsDelete, ModuleId, Name, DisplayName, Remark, ApiPath, RowNo)
VALUES('9D2FBB20-C2BD-41DF-B727-7E554E2F3889', -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'94CC5387-667F-4360-946C-6BF7F5CECE44', -- ModuleId - uniqueidentifier
'VehicleUpdate' , -- Name - nvarchar(50)
'修改车辆信息' , -- DisplayName - nvarchar(50)
N'' , -- Remark - nvarchar(50)
N'/Vehicle/Add' , -- ApiPath - nvarchar(50)
3   -- RowNo - int
    )

INSERT INTO dbo.TPermissionType(Id, TenanId, IsDelete, ModuleId, Name, DisplayName, Remark, ApiPath, RowNo)
VALUES('76015348-F875-408B-A6D7-25814717688F', -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'94CC5387-667F-4360-946C-6BF7F5CECE44', -- ModuleId - uniqueidentifier
'VehicleDelete' , -- Name - nvarchar(50)
'删除车辆信息' , -- DisplayName - nvarchar(50)
N'' , -- Remark - nvarchar(50)
N'/Vehicle/Delete' , -- ApiPath - nvarchar(50)
4   -- RowNo - int
    )

-------------------------------------------------------
---------- TRolePermission-----------------------------
-------------------------------------------------------
INSERT INTO dbo.TRolePermission(Id, TenanId, IsDelete, RoleId,ModuleId, PermissionId, SeqNo)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'39BAFB2B-1A48-47D3-A668-2EA2E384CD33', -- RoleId - uniqueidentifier
'94CC5387-667F-4360-946C-6BF7F5CECE44',
'44476BEE-FFDC-4A0C-A237-B9563A958F03', -- PermissionId - uniqueidentifier
0   -- SeqNo - int
    )

INSERT INTO dbo.TRolePermission(Id, TenanId, IsDelete, RoleId,ModuleId, PermissionId, SeqNo)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'39BAFB2B-1A48-47D3-A668-2EA2E384CD33', -- RoleId - uniqueidentifier
'94CC5387-667F-4360-946C-6BF7F5CECE44',
'06843524-0C68-40FE-A84C-862593123D27', -- PermissionId - uniqueidentifier
1   -- SeqNo - int
    )

INSERT INTO dbo.TRolePermission(Id, TenanId, IsDelete, RoleId,ModuleId, PermissionId, SeqNo)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'39BAFB2B-1A48-47D3-A668-2EA2E384CD33', -- RoleId - uniqueidentifier
'94CC5387-667F-4360-946C-6BF7F5CECE44',
'61FCBF22-E33B-47AF-B95D-6ED96F2A8415', -- PermissionId - uniqueidentifier
2   -- SeqNo - int
    )

INSERT INTO dbo.TRolePermission(Id, TenanId, IsDelete, RoleId,ModuleId, PermissionId, SeqNo)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'39BAFB2B-1A48-47D3-A668-2EA2E384CD33', -- RoleId - uniqueidentifier
'94CC5387-667F-4360-946C-6BF7F5CECE44',
'9D2FBB20-C2BD-41DF-B727-7E554E2F3889', -- PermissionId - uniqueidentifier
3   -- SeqNo - int
    )

INSERT INTO dbo.TRolePermission(Id, TenanId, IsDelete, RoleId,ModuleId, PermissionId, SeqNo)
VALUES(NEWID(), -- Id - uniqueidentifier
'8CAB1C61-A36B-4910-B8EC-7351EA11BA71', -- TenanId - uniqueidentifier
'00000000-0000-0000-0000-000000000000', -- IsDelete - bit
'39BAFB2B-1A48-47D3-A668-2EA2E384CD33', -- RoleId - uniqueidentifier
'94CC5387-667F-4360-946C-6BF7F5CECE44',
'76015348-F875-408B-A6D7-25814717688F', -- PermissionId - uniqueidentifier
4   -- SeqNo - int
    )

SELECT * FROM dbo.TUser
SELECT * FROM dbo.TRole
SELECT * FROM dbo.TUserRole
SELECT * FROM dbo.TModuleType
SELECT * FROM dbo.TRoleModule
SELECT * FROM TPermissionType
SELECT * FROM TRolePermission
--SELECT NEWID()
--44476BEE-FFDC-4A0C-A237-B9563A958F03
--06843524-0C68-40FE-A84C-862593123D27
--61FCBF22-E33B-47AF-B95D-6ED96F2A8415
--9D2FBB20-C2BD-41DF-B727-7E554E2F3889
--76015348-F875-408B-A6D7-25814717688F


--SELECT [x.UserRoles].[Id], [x.UserRoles].[IsDelete], [x.UserRoles].[RoleId], [x.UserRoles].[RowNo], [x.UserRoles].[TenanId], [x.UserRoles].[UserId]
--      FROM [TUserRole] AS [x.UserRoles]
--      INNER JOIN (
--          SELECT TOP(1) [x0].[Id]
--          FROM [TUser] AS [x0]
--          WHERE ([x0].[IsDelete] = '00000000-0000-0000-0000-000000000000') AND ([x0].[Id] ='99BF7D96-BCD5-4083-8197-BFFBEBE4501F')
--          ORDER BY [x0].[Id]
--      ) AS [t] ON [x.UserRoles].[RoleId] = [t].[Id]
--      ORDER BY [t].[Id]