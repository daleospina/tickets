CREATE PROCEDURE [dbo].[uspgetlistcustomer]
	
AS
BEGIN
	SET NOCOUNT ON;

	select id, firstname, lastname 
	from customer
END
