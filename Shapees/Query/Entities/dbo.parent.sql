USE [master]
GO
DROP TABLE [dbo].[parent]
GO

CREATE TABLE [dbo].[parent]
(
	[parent_id] INT NOT NULL PRIMARY KEY, 
    [user_id] INT NOT NULL, 
    [other_contact] NCHAR(100) NULL,
	CONSTRAINT [FK_parent_parent] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id])
)
