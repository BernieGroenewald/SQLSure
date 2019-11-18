USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_SystemGetObjectDetail]    Script Date: 2017/02/14 07:50:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_SystemGetObjectDetail](@DatabaseName varchar(50), @DBOwner varchar(50), @ObjectName varchar(255))
AS
BEGIN
	SET NOCOUNT ON;

	declare @SQLString varchar(250)

	set @SQLString = 'select Name, create_date, modify_date from ' + @DatabaseName + '.sys.procedures where name = ' + ''' + @ObjectName + '''

	exec(@SQLString)
END

GO


