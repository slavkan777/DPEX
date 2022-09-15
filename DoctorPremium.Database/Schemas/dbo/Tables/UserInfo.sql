CREATE TABLE [dbo].[UserInfo] (
    [UserInfoId]               INT              IDENTITY (1, 1) NOT NULL,
    [AspNetUserId]           INT NOT NULL,
    [LastName]   NVARCHAR(64)             NULL,
	[FirstName]   NVARCHAR(64)             NULL,
	[SurName]   NVARCHAR(64)             NULL,
	[Photo]   NVARCHAR(640)             NULL,
	[IsMale]   BIT         NULL,
	[BirthDate]   DATE         NULL,
	[Phone1]   NVARCHAR(32)         NULL,
	[Phone2]   NVARCHAR(32)         NULL,
	[CountryId]   INT            NULL,
	[CityId]   INT            NULL,
	[Address]   NVARCHAR(300)         NULL,
	[WorkTimeFrom]   TIME         NULL,
	[WorkTimeTo]   TIME         NULL,
	[Notepad]   NVARCHAR(1000)         NULL,
	[TimeZoneId]   INT        NOT NULL,
	[LanguageId]   INT        NOT NULL,
    [CreateDateUtc]   DATETIME         NOT NULL,
    [UpdateDateUtc]   DATETIME         NULL,
	[LastActivityDateUtc]   DATETIME  NOT NULL,
	[IsLocked] BIT NOT NULL, 
	[IsDeleted] BIT NOT NULL, 
    CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED ([UserInfoId] ASC),
    CONSTRAINT [FK_UserInfo_AspNetUser] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_UserInfo_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId]),
	CONSTRAINT [FK_UserInfo_Cities] FOREIGN KEY ([CityId]) REFERENCES [dbo].[Cities] ([CityId]),
	CONSTRAINT [FK_UserInfo_Languages] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([LanguageId]),
	CONSTRAINT [FK_UserInfo_TimeZones] FOREIGN KEY ([TimeZoneId]) REFERENCES [dbo].[TimeZones] ([TimeZoneId])
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [AspNetUserIdIndex]
    ON [dbo].[UserInfo]([AspNetUserId] ASC);
