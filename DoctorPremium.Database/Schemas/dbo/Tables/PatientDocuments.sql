CREATE TABLE [dbo].[PatientDocuments] (
    [PatientDocumentId]             INT              IDENTITY (1, 1) NOT NULL,
	[PatientId]             INT           NOT NULL,
    [FileName]        NVARCHAR (200)  NOT NULL,
    [Description]    NVARCHAR (200)  NULL,
    [CreateDateUtc] DATETIME         NOT NULL,
    CONSTRAINT [PK_PatientDocument] PRIMARY KEY CLUSTERED ([PatientDocumentId] ASC),
    CONSTRAINT [FK_PatientDocument_Patient] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId])
);