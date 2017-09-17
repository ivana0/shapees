USE [master]
GO
DROP TABLE [dbo].[userprofile]
GO

CREATE TABLE [dbo].[userprofile]
(
	[profile_id] INT NOT NULL PRIMARY KEY, 
    [first_name] NCHAR(50) NOT NULL, 
    [last_name] NCHAR(50) NOT NULL, 
    [dob] DATE NULL, 
    [home_phone] NCHAR(20) NULL, 
    [mobile_phone] NCHAR(20) NULL, 
    [address_id] INT NULL, 
    [photo_path] NCHAR(50) NULL,
    CONSTRAINT [FK_userprofile_userprofile] FOREIGN KEY ([address_id]) REFERENCES [address]([address_id])
)
