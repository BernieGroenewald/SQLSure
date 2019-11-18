USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetProjectActive]    Script Date: 2017/02/14 07:38:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 7 Aug 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ServerGetProjectActive](@ProjectName varchar(50))
AS
BEGIN
	SET NOCOUNT ON;

	select Active
	  from SVC_Project
	 where ProjectName = @ProjectName
END


GO


