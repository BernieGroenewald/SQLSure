USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_GetProjectObjects]    Script Date: 2017/02/14 06:34:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 27 August 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_GetProjectObjects]( @ProjectName varchar(50))
	
AS
BEGIN
	SET NOCOUNT ON;

	if exists(select PO.ProjectObjectID
				from SVC_ProjectObject as PO inner join
                     SVC_Project as P on PO.ProjectID = P.ProjectID left outer join
                     SVC_LU_ObjectStatus as LU on P.ObjectStatusID = LU.ObjectStatusID left outer join
                     SVC_FileObject as FO on PO.FileObjectID = FO.FileObjectID
			   where P.ProjectName = @ProjectName)
	begin
		select PO.ProjectObjectID, 
			   PO.ProjectID, 
			   isnull(PO.FileObjectID, 0) as FileObjectID, 
			   isnull(PO.DBName, '') as DBName, 
			   isnull(PO.DBObjectName, '') as DBObjectName, 
			   PO.ObjectSequence, 
			   PO.ObjectSelected, 
			   PO.DateModified, 
			   PO.UserID, 
			   FO.FileObject, 
			   isnull(FO.ObjectDescription, '') as ObjectDescription, 
			   P.ProjectName, 
			   isnull(P.ProjectDesc, '') as ProjectDesc, 
			   LU.ObjectStatusDesc, 
			   P.Comment
		  from SVC_ProjectObject as PO inner join
               SVC_Project as P on PO.ProjectID = P.ProjectID left outer join
               SVC_LU_ObjectStatus as LU on P.ObjectStatusID = LU.ObjectStatusID left outer join
               SVC_FileObject as FO on PO.FileObjectID = FO.FileObjectID
		 where P.ProjectName = @ProjectName
	  order by PO.ObjectSequence
  end
  else
  begin
	  select distinct 0 as ProjectObjectID,
			          P.ProjectID,
					  0 as FileObjectID,
					  OH.DatabaseName as DBName,
					  OH.ObjectName as DBObjectName,
					  999 as ObjectSequence,
					  'N' as ObjectSelected,
					  '1 Jan 2000' as DateModified,
					  0 as UserID,
					  0 as FileObject,
					  '' as ObjectDescription,
					  P.ProjectName, 
					  ISNULL(P.ProjectDesc, '') AS ProjectDesc, 
					  LU.ObjectStatusDesc, 
					  P.Comment
	             from SVC_Project as P inner join
                      SVC_ObjectHistory as OH on P.ProjectID = OH.ProjectID left outer join
                      SVC_LU_ObjectStatus as LU on P.ObjectStatusID = LU.ObjectStatusID
				where P.ProjectName = @ProjectName
	         order by OH.DatabaseName, OH.ObjectName
	end
END
GO


