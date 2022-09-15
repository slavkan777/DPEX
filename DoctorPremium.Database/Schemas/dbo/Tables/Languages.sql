CREATE TABLE [dbo].[Languages]
(
	[LanguageId]   INT IDENTITY (1, 1),
	[LanguageShortName]       NVARCHAR(3)             NOT NULL,
	[LanguageName]            NVARCHAR(30)             NOT NULL, 
    CONSTRAINT [PK_Languages] PRIMARY KEY ([LanguageId]),
)
