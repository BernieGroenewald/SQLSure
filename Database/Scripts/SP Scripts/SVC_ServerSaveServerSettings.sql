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

create PROCEDURE [dbo].[SVC_ServerSaveServerSettings]( @ServerAlias varchar(50), 
													   @UserName varchar(50), 
													   @Password varbinary(256), 
													   @IntegratedSecurity varchar(1),
													   @UserID int)
	
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
			   [Password] = @Password,
			   IntegratedSecurity = @IntegratedSecurity
		 where ServerAliasID = @ServerAliasID
		   and UserID = @UserID
	end
	else
	begin
		insert into SVC_Server (ServerAliasID, UserName, [Password], IntegratedSecurity, CreateDate, UserID)
		select @ServerAliasID, @UserName, @Password, @IntegratedSecurity, getdate(), @UserID
	end
END
