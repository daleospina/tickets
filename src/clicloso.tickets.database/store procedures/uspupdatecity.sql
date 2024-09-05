CREATE PROCEDURE [dbo].[uspupdatecity]
	@id int,
	@name varchar(50),
	@code varchar(4)
AS
BEGIN
	update city
	set  [name] = @name, [code] = @code
	where [id] = @id;
	
END