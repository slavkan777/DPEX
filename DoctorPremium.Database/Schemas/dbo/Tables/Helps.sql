CREATE TABLE [dbo].Helps (
    [HelpId] INT IDENTITY (1, 1) NOT NULL,
	[LanguageId] INT  NOT NULL,
	[URL] NVARCHAR (100)    NOT NULL,
	[Order] INT  NOT NULL, 
	[Description]   NVARCHAR(MAX)  NOT NULL,
	[Notes]   NVARCHAR(400)  NULL, 
	[CreateDateUtc] datetime NOT NULL,
	[UpdatedateUtc] datetime,
	[IsPublish]   BIT  NOT NULL, 
    CONSTRAINT [PK_Helps] PRIMARY KEY ([HelpId] ASC), 
	CONSTRAINT [FK_Helps_Languages] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([LanguageId]))
