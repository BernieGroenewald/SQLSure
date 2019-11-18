USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ObjectGetVersionAtCheckOut]    Script Date: 2017/02/14 06:36:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 26 July 2016
-- Description:	Initial
-- =============================================

--Converted

CREATE PROCEDURE [dbo].[SVC_ObjectGetVersionAtCheckOut]( @ServerName varchar(50), 
														 @ObjectName varchar(255),
														 @DatabaseName varchar(255)											)
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @HistoryID int
	
	select top 1 @HistoryID = isnull(HistoryID, 0)
	  from SVC_ObjectHistory
	 where ObjectName = @ObjectName 
	   and DatabaseName = @DatabaseName
	   and VersionAtCheckOut = 'Y'
	   and ServerID in (select ServerID from SVC_Server where ServerAliasID = (select ServerAliasID from SVC_LU_ServerAlias where ServerAliasDesc = @ServerName))
	   order by HistoryDate desc

	if @HistoryID > 0
	begin
		select ObjectText
		  from SVC_ObjectHistory
		 where HistoryID = @HistoryID
	end
	else
	begin
		select '' as ObjectText
	end
END


GO


