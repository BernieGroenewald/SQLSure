USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetBackupSetDetail]    Script Date: 2017/02/14 07:35:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 7 Aug 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ServerGetBackupSetDetail](@SetName varchar(50))
AS
BEGIN
	SET NOCOUNT ON;

	select BS.BackupSetName, 
	       BS.BackupSetDesc, 
		   BS.CreateDate, 
		   BS.Active, 
		   LSA.ServerAliasDesc
      from SVC_BackupSet as BS inner join
           SVC_LU_ServerAlias as LSA on BS.ServerAliasID = LSA.ServerAliasID
	 where BackupSetName = @SetName
END


GO


