CREATE PROCEDURE [dbo].[uspgetlistcity]
	
AS
BEGIN
	SET NOCOUNT ON;

	select [id], [name], [code]
	from city
END

