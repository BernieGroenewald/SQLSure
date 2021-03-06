USE [SQLSourceControl]
GO
/****** Object:  StoredProcedure [dbo].[SVC_ServerSaveSettings]    Script Date: 2017/03/16 05:54:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

ALTER PROCEDURE [dbo].[SVC_ServerSaveSettings]( @ServerAlias varchar(50), 
												@UserName varchar(50), 
												@Password varchar(50), 
												@IntegratedSecurity varchar(1),
												@UserID int,
												@PassPhrase nvarchar(128))
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerAliasID int

	select @ServerAliasID = ServerAliasID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerAlias

	if exists(select ServerID from SVC_Server where ServerAliasID = @ServerAliasID and UserID = @UserID)
	begin
		update SVC_Server
		   set UserName = @UserName,
			   [Password] = EncryptByPassPhrase(@PassPhrase, @Password),
			   IntegratedSecurity = @IntegratedSecurity
		 where ServerAliasID = @ServerAliasID
		   and UserID = @UserID
	end
	else
	begin
		insert into SVC_Server (ServerAliasID, UserName, [Password], IntegratedSecurity, CreateDate, UserID)
		select @ServerAliasID, @UserName, EncryptByPassPhrase(@PassPhrase, @Password), @IntegratedSecurity, getdate(), @UserID
	end
END
