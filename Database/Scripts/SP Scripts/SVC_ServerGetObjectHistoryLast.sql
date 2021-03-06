USE [SQLSourceControl]
GO
/****** Object:  StoredProcedure [dbo].[SVC_ServerGetObjectHistoryLast]    Script Date: 2017/03/16 09:57:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

ALTER PROCEDURE [dbo].[SVC_ServerGetObjectHistoryLast](@ObjectName varchar(255), @ServerAliasID int)
	
AS
BEGIN
	SET NOCOUNT ON;

	select top(1) OS.ObjectStatusDesc as [Status], 
	       OH.HistoryDate as [Date], 
		   OH.UserName as [User], 
		   OH.ObjectName as [Name], 
		   OH.DatabaseName as [Database], 
           OH.Comment, 
		   SA.ServerName as [Server], 
		   OH.ObjectText as [Text], 
		   OS.AvailableForEdit, 
		   isnull(P.ProjectName, '') AS ProjectName
      from SVC_LU_ObjectStatus as OS inner join
           SVC_ObjectHistory as OH on OS.ObjectStatusID = OH.ObjectStatusID inner join
           SVC_Server as S on OH.ServerID = S.ServerID inner join
           SVC_LU_ServerAlias as SA on S.ServerAliasID = SA.ServerAliasID left outer join
           SVC_Project as P on OH.ProjectID = P.ProjectID
	 where OH.ObjectName = @ObjectName
	   and S.ServerAliasID = @ServerAliasID
  order by OH.HistoryID desc
END
