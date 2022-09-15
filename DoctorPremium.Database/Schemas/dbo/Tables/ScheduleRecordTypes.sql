CREATE TABLE [dbo].[ScheduleRecordTypes]
(
	[ScheduleRecordTypeId]       INT IDENTITY (1, 1),
	[ScheduleRecordTypeName]            NVARCHAR(10)             NOT NULL,
	CONSTRAINT [PK_ScheduleRecordTypeId] PRIMARY KEY CLUSTERED ([ScheduleRecordTypeId] ASC), 
)
