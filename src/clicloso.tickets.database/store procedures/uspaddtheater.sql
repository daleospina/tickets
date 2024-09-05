CREATE PROCEDURE [dbo].[uspaddtheater]
	@id int,
	@idcity int,
	@name varchar(50),
	@address varchar(50),
	@state bit
AS
BEGIN
	insert into theater([id], [idcity], [name],[address],[state]) 
	values(@id, @idcity, @name, @address, @state)
END
