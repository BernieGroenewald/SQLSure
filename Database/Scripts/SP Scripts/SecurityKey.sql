create procedure SVC_SecurityKey (@PassPhrase varchar(128) output) with encryption
as
begin
	select @PassPhrase = 'BMWP@$$w0rd'
end
