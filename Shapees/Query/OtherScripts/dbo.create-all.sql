﻿USE [master]
GO
ALTER TABLE [dbo].[users]
DROP CONSTRAINT [FK_users_users];
GO
ALTER TABLE [dbo].[userprofile]
DROP CONSTRAINT [FK_userprofile_userprofile];
GO
DROP TABLE [dbo].[userprofile]
GO

USE [master]
GO
DROP TABLE [dbo].[address]
GO


USE [master]
GO
ALTER TABLE [dbo].[manager]
DROP CONSTRAINT [FK_manager_manager];
GO
DROP TABLE [dbo].[manager]
GO

USE [master]
GO
ALTER TABLE [dbo].[educator]
DROP CONSTRAINT FK_educator_educator;
GO
DROP TABLE [dbo].[educator]
GO


USE [master]
GO
ALTER TABLE [dbo].[parent]
DROP CONSTRAINT FK_parent_parent;
GO
DROP TABLE [dbo].[parent]
GO

USE [master]
GO

DROP TABLE [dbo].[users]
GO





CREATE TABLE [dbo].[address]
(
	[address_id] INT IDENTITY(1,1) PRIMARY KEY, 
    [street] NCHAR(50) NOT NULL, 
    [city] NCHAR(20) NOT NULL, 
    [postcode] INT NOT NULL, 
    [state] NCHAR(20) NOT NULL,
)


CREATE TABLE [dbo].[userprofile]
(
	[profile_id] INT IDENTITY(1,1) PRIMARY KEY, 
    [first_name] NCHAR(50) NOT NULL, 
    [last_name] NCHAR(50) NOT NULL, 
    [dob] DATE NULL, 
    [home_phone] NCHAR(20) NULL, 
    [mobile_phone] NCHAR(20) NULL, 
    [address_id] INT NULL, 
    [profileimage] VARBINARY(MAX) NULL,

    CONSTRAINT [FK_userprofile_userprofile] FOREIGN KEY ([address_id]) REFERENCES [address]([address_id])
)

INSERT [dbo].[address] (street, city, postcode, state)   
    VALUES ('1 Porter Street', 'Wollongong', 2500, 'NSW') 
GO 

INSERT [dbo].[userprofile] (first_name, last_name, dob, home_phone, mobile_phone)  
    VALUES ('Ivana', 'Ozakovic', GETDATE(), '06666666', '0451666666') 
GO 


CREATE TABLE [dbo].[users]
(
	[userid] INT IDENTITY(1,1) PRIMARY KEY, 
    [username] NCHAR(20) NOT NULL, 
    [email] NCHAR(20) NOT NULL, 
    [pass] NCHAR(20) NOT NULL, 
    [user_type] INT NOT NULL, 
    [last_login] DATETIME NULL, 
    [is_logged_in] INT NOT NULL,
	[profile_id] INT NULL, 
    CONSTRAINT [FK_users_users] FOREIGN KEY ([profile_id]) REFERENCES [userprofile]([profile_id])
)

INSERT [dbo].[users] (username, email, pass, user_type, last_login, is_logged_in)  
    VALUES ('io447', 'io447@uowmail.edu.au', 's#ap33$1', 0, GETDATE(), 0)  
GO 



CREATE TABLE [dbo].[manager]
(
	[manager_id] INT IDENTITY(1,1) PRIMARY KEY, 
    [userid] INT NOT NULL, 
    [employed_on] DATE NULL,
	[short_bio] NCHAR(250) NULL,
	CONSTRAINT [FK_manager_manager] FOREIGN KEY ([userid]) REFERENCES [users]([userid])
)


CREATE TABLE [dbo].[educator]
(
	[educator_id] INT IDENTITY(1,1) PRIMARY KEY, 
    [userid] INT NOT NULL, 
    [employed_on] DATE NULL,
	[short_bio] NCHAR(250) NULL,
	CONSTRAINT [FK_educator_educator] FOREIGN KEY ([userid]) REFERENCES [users]([userid])
)


CREATE TABLE [dbo].[parent]
(
	[parent_id] INT IDENTITY(1,1) PRIMARY KEY, 
    [userid] INT NOT NULL, 
    [other_contact] NCHAR(100) NULL,
	CONSTRAINT [FK_parent_parent] FOREIGN KEY ([userid]) REFERENCES [users]([userid])
)


