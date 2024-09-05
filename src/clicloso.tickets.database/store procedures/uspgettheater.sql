CREATE PROCEDURE [dbo].[uspgettheater]
	@id int
AS
	select [id], [idcity], [name], [address], [state]
	from theater 
	where [id] = @id;