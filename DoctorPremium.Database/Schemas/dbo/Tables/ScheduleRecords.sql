CREATE TABLE [dbo].[ScheduleRecords]
(
	[ScheduleRecordId] INT NOT NULL IDENTITY (1, 1),
	[ScheduleRecordTypeId] INT  NOT NULL,
	[UserId]           INT NOT NULL,
	[isFinished] BIT NOT NULL,
	[PatientId] INT NULL,
	[PatientVisitId] INT NULL,
	[RecordDate]            DATE             NOT NULL,
    [RecordStartTime]       TIME (7)         NULL,
	[Duration]             TINYINT              NOT NULL,
	[CostOfFeautureServices]             DECIMAL(18, 2)              NULL,
	[Description]		NVARCHAR(200)   NULL,
	[Title]		NVARCHAR(80)             NULL,
	[CreateDateUtc]   DATETIME         NOT NULL,
    --[UpdateDate]   DATETIME         NULL,
	--[IsDeleted] BIT NOT NULL, 
	CONSTRAINT [PK_ScheduleRecords] PRIMARY KEY CLUSTERED ([ScheduleRecordId] ASC), 
	CONSTRAINT [FK_ScheduleRecord_AspNetUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_ScheduleRecord_ScheduleRecordType] FOREIGN KEY ([ScheduleRecordTypeId]) REFERENCES [dbo].[ScheduleRecordTypes] ([ScheduleRecordTypeId]),
	CONSTRAINT [FK_ScheduleRecord_Patients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId]),
	CONSTRAINT [FK_ScheduleRecord_PatientVisits] FOREIGN KEY ([PatientVisitId]) REFERENCES [dbo].[PatientVisits] ([PatientVisitId])

)
