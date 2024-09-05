CREATE PROCEDURE [dbo].[uspdeletetheater]
	@id int	
AS
BEGIN
	DELETE FROM theater
	WHERE [id] = @id;
END