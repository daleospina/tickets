CREATE PROCEDURE [dbo].[uspgetcustomer]
	@id int
AS
	select id, firstname, lastname 
	from customer 
	where [id] = @id;

