USE [master]
GO
DROP TABLE [dbo].[address]
GO

CREATE TABLE [dbo].[address]
(
	[address_id] INT NOT NULL PRIMARY KEY, 
    [street] NCHAR(50) NOT NULL, 
    [city] NCHAR(20) NOT NULL, 
    [postcode] INT NOT NULL, 
    [state] NCHAR(20) NOT NULL,
    [country] NCHAR(20) NOT NULL
)
