USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ObjectCreate]    Script Date: 2017/02/14 06:35:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 26 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ObjectCreate]( @ObjectText varchar(max))
	
AS
BEGIN
	SET NOCOUNT ON;

	exec(@ObjectText)
END

GO


