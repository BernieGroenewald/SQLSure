USE [SQLSourceControl]
GO
/****** Object:  StoredProcedure [dbo].[SVC_ServerGetObjectStatusDetail]    Script Date: 2017/03/16 10:23:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

--Converted

ALTER PROCEDURE [dbo].[SVC_ServerGetObjectStatusDetail]( @ServerAlias varchar(50), 
											         @ObjectName varchar(255))
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerAliasID int

	select @ServerAliasID = ServerAliasID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerAlias

	select top (1) OS.ObjectStatusDesc, 
	       OH.HistoryID, 
		   OH.ServerID, 
		   OH.HistoryDate, 
		   OH.ObjectStatusID, 
		   OH.UserName, 
		   OH.ObjectName, 
		   OH.DatabaseName, 
		   OH.ObjectText, 
           OH.Comment, 
		   @ServerAlias as ServerAlias, 
		   SA.ServerName, 
		   P.ProjectName, 
		   OH.ObjectVersion
      from SVC_LU_ObjectStatus as OS inner join
           SVC_ObjectHistory as OH on OS.ObjectStatusID = OH.ObjectStatusID inner join
           SVC_Server as S on OH.ServerID = S.ServerID inner join
           SVC_LU_ServerAlias as SA on S.ServerAliasID = SA.ServerAliasID left outer join
           SVC_Project as P on OH.ProjectID = P.ProjectID
	 where OH.ObjectName = @ObjectName
  order by OH.HistoryID desc

END

