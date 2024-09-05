CREATE PROCEDURE [dbo].[uspaddcity]
	@id int,
	@name varchar(50),
	@code varchar(4)
AS
BEGIN
	insert into city([id], [name], [code]) 
	values(@id, @name, @code)
END
