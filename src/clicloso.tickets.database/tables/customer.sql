CREATE TABLE [dbo].[customer]
(
	[id] INT NOT NULL , 
    [firstname] VARCHAR(50) NOT NULL, 
    [lastname] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_customer] PRIMARY KEY ([id])
)
