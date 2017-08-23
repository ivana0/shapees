USE [master]
GO
DROP TABLE [dbo].[educator]
GO

CREATE TABLE [dbo].[educator]
(
	[educator_id] INT NOT NULL PRIMARY KEY, 
    [user_id] INT NOT NULL, 
    [employed_on] DATE NULL,
	[short_bio] NCHAR(250) NULL,
	CONSTRAINT [FK_educator_educator] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id])
)