CREATE PROCEDURE [dbo].[uspupdatetheater]

	@id int,
	@idcity int,
	@name varchar(50),
	@address varchar(50),
	@state bit

AS
BEGIN

	update theater
	set  [idcity] = @idcity, [name] = @name, [address] = @address, [state] = @state
	where [id] = @id;
	
END