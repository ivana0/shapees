USE [master]
GO
DROP TABLE [dbo].[users]
GO

CREATE TABLE [dbo].[users]
(
	[userid] INT NOT NULL PRIMARY KEY, 
    [username] NCHAR(20) NOT NULL, 
    [email] NCHAR(20) NOT NULL, 
    [pass] NCHAR(20) NOT NULL, 
    [user_type] INT NOT NULL, 
    [last_login] DATETIME NULL, 
    [is_logged_in] INT NOT NULL,
	[profile_id] INT NULL, 
    CONSTRAINT [FK_users_users] FOREIGN KEY ([profile_id]) REFERENCES [userprofile]([profile_id])
)

INSERT [dbo].[users] (userid, username, email, pass, user_type, last_login, is_logged_in)  
    VALUES (11111111, 'io447', 'io447@uowmail.edu.au', 's#ap33$1', 0, GETDATE(), 0)  
GO 