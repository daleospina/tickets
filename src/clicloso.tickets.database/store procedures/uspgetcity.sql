CREATE PROCEDURE [dbo].[uspgetcity]
	@id int
AS
	select [id], [name], [code] 
	from city 
	where [id] = @id;