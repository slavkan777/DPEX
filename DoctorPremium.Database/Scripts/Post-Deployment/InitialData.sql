/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO [dbo].[AspNetRoles]([Name])VALUES('Administrator')
INSERT INTO [dbo].[AspNetRoles]([Name])VALUES('Doctor')
GO
INSERT INTO [dbo].[ScheduleRecordTypes]([ScheduleRecordTypeName]) VALUES ('Visit')
INSERT INTO [dbo].[ScheduleRecordTypes]([ScheduleRecordTypeName]) VALUES ('Task')
GO
INSERT INTO [dbo].[Languages]([LanguageShortName],[LanguageName]) VALUES ('EN' ,'English')
INSERT INTO [dbo].[Languages]([LanguageShortName],[LanguageName]) VALUES ('RU' ,'Русский')
