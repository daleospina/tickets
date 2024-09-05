CREATE PROCEDURE [dbo].[uspupdatecustomer]
	@id int,
	@firstname varchar(50),
	@lastname varchar(50)
AS
BEGIN
	update customer
	set  [firstname] = @firstname, [lastname] = @lastname
	where [id] = @id;
	
	end