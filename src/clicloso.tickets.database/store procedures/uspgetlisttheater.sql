CREATE PROCEDURE [dbo].[uspgetlisttheater]
	
AS
BEGIN
	SET NOCOUNT ON;

	select [id], [idcity], [name], [address],[state]
	from theater
END