CREATE PROCEDURE [dbo].[uspaddcustomer]
	@id int,
	@firstname varchar(50),
	@lastname varchar(50)
AS
BEGIN
	insert into customer([id], [firstname], [lastname]) 
	values(@id, @firstname, @lastname)
END
