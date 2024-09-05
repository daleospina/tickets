CREATE PROCEDURE [dbo].[uspdeletecustomer]
	@id int	
AS
BEGIN
	DELETE FROM customer
	WHERE [id] = @id;
END