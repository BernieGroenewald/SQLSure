Create PROCEDURE [dbo].[SVC_ServerGetServerAliasDetail](@ServerAlias varchar(50))
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerAliasID int

	select SA.ServerName, 
		   SR.ServerRoleDesc
      from SVC_LU_ServerAlias as SA inner join
           SVC_LU_ServerRole as SR on SA.ServerRoleID = SR.ServerRoleID
	 where SA.ServerAliasDesc = @ServerAlias
END