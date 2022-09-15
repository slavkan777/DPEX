CREATE TABLE [dbo].[DentalCards]
(
	[DentalCardId] INT NOT NULL IDENTITY (1, 1),
	[PatientId]            INT              NOT NULL,
	[OrderNumber]           TINYINT  NOT NULL,
	[Description]		NVARCHAR(20)   NULL,
	[IsCheck]		BIT             NOT NULL,
	--[CreateDate]   DATETIME         NOT NULL,
 --   [UpdateDate]   DATETIME         NULL,
	--[IsDeleted] BIT NOT NULL, 
	CONSTRAINT [PK_DentalCards] PRIMARY KEY CLUSTERED ([DentalCardId] ASC), 
	CONSTRAINT [FK_DentalCards_Patients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId])
)
