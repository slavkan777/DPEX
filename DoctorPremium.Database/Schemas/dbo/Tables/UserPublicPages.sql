CREATE TABLE [dbo].[UserPublicPage]
(
	[Id]             INT              IDENTITY (1, 1) NOT NULL,
	[UserId]               INT  NOT NULL,
	[AboutOfDoctor]     NVARCHAR (4000)    NULL,
	[DoctorService]   nvarchar(MAX)             NULL,
	[PublicEmailAddress]   NVARCHAR (100)             NULL,
	[PublicAddressForPatients]   NVARCHAR (500)             NULL,
	[PublicPhone]   NVARCHAR (100)             NULL,
	[VisitCounter]               INT  NOT NULL,
	[IsPublic] BIT NOT NULL, 
    [CreateDateUtc]   DATETIME         NOT NULL,
    [UpdateDateUtc]   DATETIME         NULL,
	[IsDeleted] BIT NOT NULL, 
    CONSTRAINT [PK_PublicPage] PRIMARY KEY CLUSTERED ([Id] ASC), 
	CONSTRAINT [FK_AspNetUser_PublicPage] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserIdIndex]
    ON [dbo].[UserPublicPage]([UserId] ASC);
