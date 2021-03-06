CREATE PROCEDURE [dbo].[SVC_ServerSaveGroup]( @ServerGroup varchar(50), 
											@ServerAlias varchar(50), 
											@ServerName varchar(250), 
											@ServerRole varchar(50),
											@ReleaseOrder varchar(10))
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerRoleID int
	declare @ServerAliasID int
	declare @ServerGroupID int

	set @ServerRoleID = 0
	set @ServerAliasID = 0
	set @ServerGroupID = 0

	select @ServerAliasID = isnull(ServerAliasID, 0)
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerAlias

	select @ServerRoleID = ServerRoleID
	  from SVC_LU_ServerRole
	 where ServerRoleDesc = @ServerRole

    
	if exists(select ServerGroupID from SVC_LU_ServerGroup where ServerGroupDesc =  @ServerGroup)
	begin
		select @ServerGroupID 
		  from SVC_LU_ServerGroup 
		 where ServerGroupDesc =  @ServerGroup

		if exists(select ServerAliasID from SVC_LU_ServerAlias where ServerGroupID = @ServerGroupID and ServerRoleID = @ServerRoleID)
		begin
			select @ServerAliasID = ServerAliasID
			  from SVC_LU_ServerAlias
			 where ServerGroupID = @ServerGroupID
			   and ServerRoleID = @ServerRoleID

			update SVC_LU_ServerAlias
			   set ServerAliasDesc = @ServerAlias,
			       ServerName = @ServerName,
			       ReleaseOrder = @ReleaseOrder
			 where ServerAliasID = @ServerAliasID
		end
		else
		begin
			insert into SVC_LU_ServerAlias (ServerAliasDesc, ReleaseOrder, ServerGroupID, ServerRoleID, ServerName)
		         select @ServerAlias, @ReleaseOrder, @ServerGroupID, @ServerRoleID, @ServerName
		end
	end
	else
	begin
		insert into SVC_LU_ServerGroup (ServerGroupDesc)
		     select @ServerGroup

		select @ServerGroupID =  ident_current('SVC_LU_ServerGroup')

		insert into SVC_LU_ServerAlias (ServerAliasDesc, ReleaseOrder, ServerGroupID, ServerRoleID, ServerName)
		select @ServerAlias, @ReleaseOrder, @ServerGroupID, @ServerRoleID, @ServerName
	end
END
