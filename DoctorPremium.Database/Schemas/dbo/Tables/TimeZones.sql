CREATE TABLE [dbo].[TimeZones]
(
	[TimeZoneId]   INT IDENTITY (1, 1),
	[SystemId]            NVARCHAR(50)           NOT NULL, 
	[UTCOffset]            INT            NOT NULL, 
	[TimeZoneNameRu]       NVARCHAR(50)             NOT NULL,
	[TimeZoneNameEn]       NVARCHAR(50)             NOT NULL,
	[SupportSummerTime]           bit            NOT NULL, 
    CONSTRAINT [PK_TimeZones] PRIMARY KEY ([TimeZoneId])
)
