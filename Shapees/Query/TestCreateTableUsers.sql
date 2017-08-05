USE [master]
GO
DROP TABLE [dbo].[users]
GO
CREATE TABLE [dbo].[users]
(
	[userid] INT NOT NULL PRIMARY KEY, 
    [username] NCHAR(10) NULL, 
    [email] NVARCHAR(50) NULL, 
    [pass] NCHAR(20) NULL, 
    [user_type] NVARCHAR(50) NULL,
)

INSERT [dbo].[users] (userid, username, email, pass, user_type)  
    VALUES (1147, 'io447', 'io447@uowmail.edu.au', 's#ap33$1', 'admin')  
GO  
INSERT [dbo].[users] (userid, username, email, pass, user_type)  
    VALUES (1111, 'xx111', 'xx111@uowmail.edu.au', 's#ap33$2', 'user')  
GO  