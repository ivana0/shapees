USE [master]
GO


CREATE TABLE [dbo].[userinfo]
(
	[userid] INT IDENTITY(1,1) PRIMARY KEY, 
    [username] NCHAR(20) NOT NULL, 
    [email] NCHAR(20) NOT NULL, 
    [pass] NCHAR(20) NOT NULL, 
    [usertype] INT NOT NULL, 
    [lastlogin] DATETIME NULL, 
    [isloggedin] INT NOT NULL,

    [street] NCHAR(50) NULL, 
    [city] NCHAR(20) NULL, 
    [postcode] INT NULL, 
    [state] NCHAR(20) NULL,

	[firstname] NCHAR(50) NOT NULL, 
    [lastname] NCHAR(50) NOT NULL, 
    [dob] DATE NULL, 
    [homephone] NCHAR(20) NULL, 
    [mobilephone] NCHAR(20) NULL, 

	[employedon] DATE NULL,
	[shortbio] NCHAR(250) NULL,

	[othercontact] NCHAR(100) NULL,

    [profileimage] VARBINARY(MAX) NULL

	
)


INSERT [dbo].[users] (username, email, pass, user_type, last_login, is_logged_in, 
						first_name, last_name, dob, home_phone, mobile_phone,  
						street, city, postcode, state)  
    VALUES ('io447', 'io447@uowmail.edu.au', 'password123', 0, GETDATE(), -1, 
		'Ivana', 'Ozakovic', 14/03/1993, '06666666', '0451666666', 
		'1 Porter Street', 'Wollongong', 2500, 'NSW')  
GO 

USE [master]
GO


CREATE TABLE [dbo].[childinfo]
(
	[childid] INT IDENTITY(1,1) PRIMARY KEY, 

	[inroom] NCHAR(50) NOT NULL, 
    [educatorassigned] NCHAR(100) NOT NULL, 
	[educatorid] int NOT NULL, 

    [street] NCHAR(50) NULL, 
    [city] NCHAR(20) NULL, 
    [postcode] INT NULL, 
    [state] NCHAR(20) NULL,

	[firstname] NCHAR(50) NOT NULL, 
    [lastname] NCHAR(50) NOT NULL, 
    [dob] DATE NOT NULL, 
	[currentage] NCHAR(30) NULL,
    [contacnumber1] NCHAR(20) NULL, 
	[contacnumber2] NCHAR(20) NULL,

    [parent1] NCHAR(20) NULL,
	[parent2] NCHAR(20) NULL,

	[shortinfo] NCHAR(250) NULL,
	[specialneeds] VARCHAR(10) NOT NULL CHECK (mycol IN('Yes', 'No')),


    [profilepicture] VARBINARY(MAX) NULL

	
)

CREATE TABLE [dbo].[report]
(
	[reportid] INT IDENTITY(1,1) PRIMARY KEY, 
	[reporttype] VARCHAR(10) NOT NULL CHECK (mycol IN('Yes', 'No')),

    [authorfullname] NCHAR(100) NOT NULL, 
	[authorid] int NOT NULL, 

	[childid] int NOT NULL, 

	[datecreated] DATE NOT NULL, 
	[lastmodified] DATE NOT NULL, 

    [street] NCHAR(50) NULL, 
    [city] NCHAR(20) NULL, 
    [postcode] INT NULL, 
    [state] NCHAR(20) NULL,

	[firstname] NCHAR(50) NOT NULL, 
    [lastname] NCHAR(50) NOT NULL, 
    [dob] DATE NOT NULL, 
    [contacnumber1] NCHAR(20) NULL, 
	[contacnumber2] NCHAR(20) NULL,

    [parent1] NCHAR(20) NULL,
	[parent2] NCHAR(20) NULL,

	[shortinfo] NCHAR(250) NULL,
	[yesno] VARCHAR(10) NOT NULL CHECK (mycol IN('Yes', 'No')),


    [profilepicture] VARBINARY(MAX) NULL

	
)