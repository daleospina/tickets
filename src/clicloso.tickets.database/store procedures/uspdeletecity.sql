CREATE PROCEDURE [dbo].[uspdeletecity]
	@id int	
AS
BEGIN
	DELETE FROM city
	WHERE [id] = @id;
END