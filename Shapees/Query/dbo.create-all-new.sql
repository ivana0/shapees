USE [master]
GO


CREATE TABLE [dbo].[address]
(
	[address_id] INT IDENTITY(1,1) PRIMARY KEY, 
    [street] NCHAR(50) NOT NULL, 
    [city] NCHAR(20) NOT NULL, 
    [postcode] INT NOT NULL, 
    [state] NCHAR(20) NOT NULL,

)

CREATE TABLE [dbo].[users]
(
	[userid] INT IDENTITY(1,1) PRIMARY KEY, 
    [username] NCHAR(20) NOT NULL, 
    [email] NCHAR(20) NOT NULL, 
    [pass] NCHAR(20) NOT NULL, 
    [user_type] INT NOT NULL, 
    [last_login] DATETIME NULL, 
    [is_logged_in] INT NOT NULL,
	[profile_id] INT NULL
	
)

CREATE TABLE [dbo].[userprofile]
(
	[profile_id] INT IDENTITY(1,1) PRIMARY KEY , 
    [first_name] NCHAR(50) NOT NULL, 
    [last_name] NCHAR(50) NOT NULL, 
    [dob] DATE NULL, 
    [home_phone] NCHAR(20) NULL, 
    [mobile_phone] NCHAR(20) NULL, 
    [address_id] INT NULL, 
	[userid] INT NOT NULL, 
    [profileimage] VARBINARY(MAX) NULL,

    CONSTRAINT [FK_userprofile_users_userid] FOREIGN KEY ([userid]) REFERENCES [users]([userid]) ON DELETE CASCADE,
    CONSTRAINT [FK_userprofile_address_address_id] FOREIGN KEY ([address_id]) REFERENCES [address]([address_id]) ON DELETE CASCADE
)



INSERT [dbo].[users] (username, email, pass, user_type, last_login, is_logged_in)  
    VALUES ('io447', 'io447@uowmail.edu.au', 's#ap33$1', 0, GETDATE(), 0)  
GO 

INSERT [dbo].[userprofile] (first_name, last_name, dob, home_phone, mobile_phone, userid)  
    VALUES ('Ivana', 'Ozakovic', GETDATE(), '06666666', '0451666666', 1) 
GO 

INSERT [dbo].[address] (street, city, postcode, state)   
    VALUES ('1 Porter Street', 'Wollongong', 2500, 'NSW') 
GO 
