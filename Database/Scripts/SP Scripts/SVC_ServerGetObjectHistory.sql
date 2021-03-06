USE [SQLSourceControl]
GO
/****** Object:  StoredProcedure [dbo].[SVC_ServerGetObjectHistory]    Script Date: 2017/03/16 09:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Converted

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

ALTER PROCEDURE [dbo].[SVC_ServerGetObjectHistory](@ObjectName varchar(255))
	
AS
BEGIN
	SET NOCOUNT ON;

	select OS.ObjectStatusDesc as [Status], 
	       OH.HistoryDate as [Date], 
		   OH.UserName as [User], 
		   OH.ObjectName as [Name], 
		   OH.DatabaseName as [Database], 
		   OH.Comment as [Comment], 
		   LSA.ServerAliasDesc as [Alias], 
		   LSA.ServerName as [Server],
		   OH.ObjectText as [Text],
		   OH.ObjectVersion as [ObjectVersion]
      from SVC_LU_ObjectStatus as OS inner join
           SVC_ObjectHistory as OH on OS.ObjectStatusID = OH.ObjectStatusID inner join
           SVC_Server as S on OH.ServerID = S.ServerID inner join
           SVC_LU_ServerAlias as LSA on S.ServerAliasID = LSA.ServerAliasID
	 where OH.ObjectName = @ObjectName
  order by OH.HistoryID desc
END
