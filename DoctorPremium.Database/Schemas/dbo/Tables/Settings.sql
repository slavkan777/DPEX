CREATE TABLE [dbo].[Settings]
(
	[SettingId]   INT IDENTITY (1, 1),
	[SettingName]            NVARCHAR(25)            NOT NULL, 
	[SettingValue]            NVARCHAR(15)            NOT NULL, 
	CONSTRAINT [PK_Setting] PRIMARY KEY ([SettingId])
)
