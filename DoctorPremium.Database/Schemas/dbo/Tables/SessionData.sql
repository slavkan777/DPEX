CREATE TABLE [dbo].[SessionData]
(
	[Id] INT IDENTITY (1, 1),
    [SessionId] CHAR(36) NOT NULL, 
    [SessionStart] DATETIME NOT NULL, 
    [SessionEnd] DATETIME NULL, 
    [AspUserId] INT NULL, 
    [IPAddress] CHAR(15) NULL, 
    CONSTRAINT [PK_SessionData] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_SessionData_AspNetUsers] FOREIGN KEY ([AspUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
)
