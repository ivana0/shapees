USE [master]
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


