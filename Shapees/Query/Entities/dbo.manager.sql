USE [master]
GO
DROP TABLE [dbo].[manager]
GO

CREATE TABLE [dbo].[manager]
(
	[manager_id] INT NOT NULL PRIMARY KEY, 
    [user_id] INT NOT NULL, 
    [employed_on] DATE NULL,
	[short_bio] NCHAR(250) NULL,
	CONSTRAINT [FK_manager_manager] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id])
)