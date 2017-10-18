/*	DROPPING CONSTRAINTS - required before dropping tables	*/

USE [master]
GO
ALTER TABLE [dbo].[childinfo]
DROP CONSTRAINT [FK_childinfo_room_roomid];
GO
ALTER TABLE [dbo].[childinfo]
DROP CONSTRAINT [FK_childinfo_userinfo_educatorid];
GO
ALTER TABLE [dbo].[childinfo]
DROP CONSTRAINT [FK_childinfo_userinfo_parent1];
GO
ALTER TABLE [dbo].[childinfo]
DROP CONSTRAINT [FK_childinfo_userinfo_parent2];
GO


USE [master]
GO
ALTER TABLE [dbo].[userinfo]
DROP CONSTRAINT [FK_userinfo_room_roomid];
GO


USE [master]
GO
ALTER TABLE [dbo].[task]
DROP CONSTRAINT [FK_task_userinfo_assignedforid];
GO
ALTER TABLE [dbo].[task]
DROP CONSTRAINT [FK_task_childinfo_childid];
GO
ALTER TABLE [dbo].[task]
DROP CONSTRAINT [FK_task_report_reportid];
GO


ALTER TABLE [dbo].[document]
DROP CONSTRAINT [FK_document_userinfo_userid];
GO
ALTER TABLE [dbo].[document]
DROP CONSTRAINT [FK_document_childinfo_childid];
GO

ALTER TABLE [dbo].[media]
DROP CONSTRAINT [FK_media_childinfo_childid];
GO
ALTER TABLE [dbo].[media]
DROP CONSTRAINT [FK_media_userinfo_userid];
GO


ALTER TABLE [dbo].[report]
DROP CONSTRAINT [FK_report_userinfo_userid];
GO
ALTER TABLE [dbo].[report]
DROP CONSTRAINT [FK_report_childinfo_childid];
GO

ALTER TABLE [dbo].[message]
DROP CONSTRAINT [FK_message_userinfo_authorid];
GO
ALTER TABLE [dbo].[message]
DROP CONSTRAINT [FK_message_userinfo_receiverid];
GO




/*	DROPPING TABLES	*/
DROP TABLE [dbo].[announcement]
GO

DROP TABLE [dbo].[message]
GO

DROP TABLE [dbo].[report]
GO

DROP TABLE [dbo].[media]
GO

DROP TABLE [dbo].[document]
GO

DROP TABLE [dbo].[task]
GO

DROP TABLE [dbo].[childinfo]
GO

DROP TABLE [dbo].[userinfo]
GO

DROP TABLE [dbo].[room]
GO


/*	CREATING TABLES	*/
CREATE TABLE [dbo].[room]
(
	[roomid] INT IDENTITY(1,1) PRIMARY KEY, 

	[roomname] NCHAR(100) NOT NULL, 
	[roomagegroup] NCHAR(100) NULL, 

	[info] NCHAR(255) NULL,
	[description] VARCHAR(MAX) NULL
)

/*	usertype: 1 (parent user), 2 (educator user), 3 (director user)
	usertypename: Parent, Educator, Director (set automatically after adding user)
	isloggedin: 0 (logged in), -1 (not logged in - inital setup)
	taskscompleted, totaltasks fields - initialized to 0 (Educators only)
*/
CREATE TABLE [dbo].[userinfo]
(
	[userid] INT IDENTITY(1,1) PRIMARY KEY, 
    [username] NCHAR(50) NOT NULL, 
    [email] NCHAR(100) NOT NULL, 
    [pass] NCHAR(20) NOT NULL, 
    [usertype] INT NOT NULL, 
	[usertypename] NCHAR(20) NULL,
    [lastlogin] DATETIME NULL, 
    [isloggedin] INT NOT NULL,

    [street] NCHAR(100) NULL, 
    [city] NCHAR(100) NULL, 
    [postcode] INT NULL, 
    [state] NCHAR(20) NULL,

	[firstname] NCHAR(100) NOT NULL, 
    [lastname] NCHAR(100) NOT NULL, 
    [dob] DATE NULL, 
    [homephone] NCHAR(20) NULL, 
    [mobilephone] NCHAR(20) NULL,
	[profileimage] NCHAR(255) NULL,
	/*	EDUCATORS AND DIRECTORS	ONLY	*/
	[employedon] DATE NULL,
	[shortbio] NCHAR(250) NULL,
	/*	EDUCATORS ONLY	*/
	[roomassigned] NCHAR(250) NULL,
	[roomid] INT NULL,
	[taskscompleted] INT NULL,
	[totaltasks] INT NULL,
	/*	PARENTS ONLY	*/
	[othercontact] NCHAR(100) NULL,
	[parentof] NCHAR(255) NULL,

	CONSTRAINT [FK_userinfo_room_roomid] FOREIGN KEY ([roomid]) REFERENCES [room]([roomid])
	
)

CREATE TABLE [dbo].[childinfo]
(
	[childid] INT IDENTITY(1,1) PRIMARY KEY, 

	[roomid] INT NULL,
	[inroom] NCHAR(30) NOT NULL, 
    [educatorfname] NCHAR(100) NOT NULL, 
	[educatorlname] NCHAR(100) NOT NULL, 
	[educatorid] INT NOT NULL, 

    [street] NCHAR(50) NULL, 
    [city] NCHAR(20) NULL, 
    [postcode] INT NULL, 
    [state] NCHAR(20) NULL,

	[childfirstname] NCHAR(100) NOT NULL, 
    [childlastname] NCHAR(100) NOT NULL, 
    [dob] DATE NOT NULL, 
	[currentage] NCHAR(30) NULL,
    [contacnumber1] NCHAR(20) NULL, 
	[contacnumber2] NCHAR(20) NULL,

    [parent1] INT NULL, 
	[parent1fname] NCHAR(100) NULL, 
	[parent1lname] NCHAR(100) NULL, 
	[parent2] INT NULL, 
	[parent2fname] NCHAR(100) NULL, 
	[parent2lname] NCHAR(100) NULL, 

	[shortinfo] NCHAR(250) NULL,
	[specialneeds] VARCHAR(10) NOT NULL CHECK (specialneeds IN('Yes', 'No')),


    [profileimage] NCHAR(255) NULL,

	CONSTRAINT [FK_childinfo_room_roomid] FOREIGN KEY ([roomid]) REFERENCES [room]([roomid]),

	CONSTRAINT [FK_childinfo_userinfo_educatorid] FOREIGN KEY ([educatorid]) REFERENCES [userinfo]([userid]),

	CONSTRAINT [FK_childinfo_userinfo_parent1] FOREIGN KEY ([parent1]) REFERENCES [userinfo]([userid]),
	CONSTRAINT [FK_childinfo_userinfo_parent2] FOREIGN KEY ([parent2]) REFERENCES [userinfo]([userid])

)

CREATE TABLE [dbo].[report]
(
	[reportid] INT IDENTITY(1,1) PRIMARY KEY, 
	[reporttype] VARCHAR(50) NOT NULL,

	[authorfirst] NCHAR(50) NOT NULL, 
    [authorlast] NCHAR(50) NOT NULL, 
	[authorid] INT NOT NULL, 

	[childid] INT NOT NULL, 
	[childfirst] NCHAR(50) NOT NULL, 
    [childlast] NCHAR(50) NOT NULL, 

	[datecreated] DATE NOT NULL, 
	[lastmodified] DATE NOT NULL, 

	[title] NCHAR(255) NULL,
	[subject] NCHAR(255) NULL,
	[bodytext] VARCHAR(MAX) NULL, 

	[filepath] NCHAR(255) NULL,
	
	[datesubmitted] DATE NULL,
	[datecompleted] DATE NULL,
	[issubmitted] INT NOT NULL,
	[iscompleted] INT NOT NULL,

	[taskid] INT NULL,
	[duedate] DATE NULL,

	[attachmentpaths] VARCHAR(MAX) NULL,
	[attachmentcount] INT NULL,


	CONSTRAINT [FK_report_userinfo_userid] FOREIGN KEY ([authorid]) REFERENCES [userinfo]([userid]),
	CONSTRAINT [FK_report_childinfo_childid] FOREIGN KEY ([childid]) REFERENCES [childinfo]([childid])

)


CREATE TABLE [dbo].[task]
(
	[taskid] INT IDENTITY(1,1) PRIMARY KEY, 

	[taskforeducator] VARCHAR(10) NOT NULL CHECK (taskforeducator IN('ONE', 'ALL', 'NONE')),
	[taskforchild] VARCHAR(10) NOT NULL CHECK (taskforchild IN('ONE', 'ALL', 'NONE')),

	[assignedforid] INT NOT NULL,
	[taskforfirst] INT NOT NULL,
	[taskforlast] INT NOT NULL,

	[authorfirst] NCHAR(50) NOT NULL, 
    [authorlast] NCHAR(50) NOT NULL, 
	[authorid] INT NOT NULL, 

	[childid] INT NOT NULL, 
	[childfirst] NCHAR(50) NOT NULL, 
    [childlast] NCHAR(50) NOT NULL, 

	[dateassigned] DATE NOT NULL, 
	[datecompleted] DATE NOT NULL, 
	[duedate] DATE NOT NULL, 

	[reportid] INT NOT NULL,
	[reportpath] NCHAR(255) NULL,

	[issubmitted] INT NOT NULL,
	[iscompleted] INT NOT NULL,

	CONSTRAINT [FK_task_userinfo_assignedforid] FOREIGN KEY ([assignedforid]) REFERENCES [userinfo]([userid]),
	CONSTRAINT [FK_task_childinfo_childid] FOREIGN KEY ([childid]) REFERENCES [childinfo]([childid]),
	CONSTRAINT [FK_task_report_reportid] FOREIGN KEY ([reportid]) REFERENCES [report]([reportid])

)



CREATE TABLE [dbo].[document]
(
	[documentid] INT IDENTITY(1,1) PRIMARY KEY, 
	[documenttype] VARCHAR(50) NOT NULL,

	[authorfirst] NCHAR(100) NOT NULL, 
    [authorlast] NCHAR(100) NOT NULL, 
	[authorid] INT NOT NULL, 

	[childid] INT NOT NULL, 
	[childfirst] NCHAR(100) NOT NULL, 
    [childlast] NCHAR(100) NOT NULL, 

	[dateuploaded] DATE NOT NULL, 

	[title] NCHAR(255) NULL,
	[description] NCHAR(255) NULL,

	[filepath] NCHAR(255) NULL,

	CONSTRAINT [FK_document_userinfo_userid] FOREIGN KEY ([authorid]) REFERENCES [userinfo]([userid]),
	CONSTRAINT [FK_document_childinfo_childid] FOREIGN KEY ([childid]) REFERENCES [childinfo]([childid])
	
)

CREATE TABLE [dbo].[media]
(
	[mediaid] INT IDENTITY(1,1) PRIMARY KEY, 
	[mediatype] VARCHAR(50) NOT NULL,

	[authorfirst] NCHAR(100) NOT NULL, 
    [authorlast] NCHAR(100) NOT NULL, 
	[authorid] INT NOT NULL, 

	[childid] INT NOT NULL, 
	[childfirst] NCHAR(100) NOT NULL, 
    [childlast] NCHAR(100) NOT NULL, 

	[dateuploaded] DATE NOT NULL, 

	[title] NCHAR(255) NULL,
	[description] NCHAR(255) NULL,

	[filepath] NCHAR(255) NULL,

	CONSTRAINT [FK_media_userinfo_userid] FOREIGN KEY ([authorid]) REFERENCES [userinfo]([userid]),
	CONSTRAINT [FK_media_childinfo_childid] FOREIGN KEY ([childid]) REFERENCES [childinfo]([childid])
	
)


CREATE TABLE [dbo].[announcement]
(
	[announcementid] INT IDENTITY(1,1) PRIMARY KEY, 
	[announcementtype] VARCHAR(50) NOT NULL,

	[datecreated] DATE NOT NULL, 

	[title] NCHAR(255) NOT NULL,
	[description] VARCHAR(MAX) NOT NULL,
	[isdisplayed] INT NOT NULL,

	[announcementfor] VARCHAR(20) NOT NULL CHECK (announcementfor IN('EDUCATORS', 'PARENTS', 'ALL'))

)


CREATE TABLE [dbo].[message]
(
	[messageid] INT IDENTITY(1,1) PRIMARY KEY, 

	[authorfirst] NCHAR(100) NOT NULL, 
    [authorlast] NCHAR(100) NOT NULL, 
	[authorid] INT NOT NULL, 

	[receiverfirst] NCHAR(100) NOT NULL, 
    [receiverlast] NCHAR(100) NOT NULL, 
	[receiverid] INT NOT NULL, 

	[datesent] DATE NOT NULL, 
	[datereceived] DATE NULL, 
	[dateopened] DATE NULL,
	
	[isopened] INT NOT NULL,
	[isrepliedto] INT NULL,

	[subject] NCHAR(255) NOT NULL,
	[description] VARCHAR(MAX) NOT NULL,

	CONSTRAINT [FK_message_userinfo_authorid] FOREIGN KEY ([authorid]) REFERENCES [userinfo]([userid]),
	CONSTRAINT [FK_message_userinfo_receiverid] FOREIGN KEY ([receiverid]) REFERENCES [userinfo]([userid])

	 
)

/*	INSERT ROOMS	*/
INSERT [dbo].[room] (roomname, roomagegroup, info, description)  
    VALUES ('Room 1', '6 weeks - 2 years', 'Room 1 (6 weeks - 2 years age)', 'Youngest children in this group.') 
GO
INSERT [dbo].[room] (roomname, roomagegroup, info, description)  
    VALUES ('Room 2', '2 years - 6 years', 'Room 2 (2 years - 6 years age)', 'Children in between in this group.') 
GO
INSERT [dbo].[room] (roomname, roomagegroup, info, description)  
    VALUES ('Room 3', '6 years - 12 years', 'Room 3 (6 years - 12 years age)', 'Eldest children in this group.') 
GO

/*	INSERT ADMIN USER - profile image can be uploaded later on*/
INSERT [dbo].[userinfo] (username, email, pass, usertype, usertypename, isloggedin, 
							street, city, postcode, state, 
							firstname, lastname, dob, 
							homephone, mobilephone, 
							employedon, shortbio)  
    VALUES ('admin', 'admin@admin.com', 'admin', 3, 'Director', -1, 
							'1 Street', 'City', 2000, 'NSW', 
							'Admin', 'Admin', GETDATE(), 
							'02123456', '0451123456', 
							GETDATE(), 'Admin of the system.' ) 
GO

