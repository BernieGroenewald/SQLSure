USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_GetRestoreSetObject]    Script Date: 2017/02/14 06:34:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 27 August 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_GetRestoreSetObject](@BackupSetName varchar(50))
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @BackupSetID int

	set @BackupSetID = 0

	if @BackupSetName <> ''
	begin
		select @BackupSetID = BackupSetID
		  from SVC_BackupSet
		 where BackupSetName = @BackupSetName
	end

	select OH.HistoryDate, 
	       OH.UserName, 
		   OH.ObjectName, 
		   OH.DatabaseName, 
		   OH.ObjectText, 
           LOS.ObjectStatusDesc, 
		   BS.BackupSetName, 
		   BSO.CreateDate, 
		   BSO.ObjectType,
		   isnull(P.ProjectName, '') as ProjectName
      from SVC_ObjectHistory as OH inner join
           SVC_LU_ObjectStatus as LOS on OH.ObjectStatusID = LOS.ObjectStatusID inner join
           SVC_BackupSetObject as BSO on OH.HistoryID = BSO.HistoryID inner join
           SVC_BackupSet as BS on BSO.BackupSetID = BS.BackupSetID left outer join
           SVC_Project as P on OH.ProjectID = P.ProjectID
	 where BS.BackupSetID = @BackupSetID
  order by OH.DatabaseName, BSO.ObjectType, OH.ObjectName, BSO.CreateDate
END


GO


