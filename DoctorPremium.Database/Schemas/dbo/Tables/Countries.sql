CREATE TABLE [dbo].[Countries]
(
	[CountryId]   INT IDENTITY (1, 1),
	[OID]       int            NOT NULL,
	[CountryNameRu]       NVARCHAR(50)             NOT NULL,
	[CountryNameEn]       NVARCHAR(50)             NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([CountryId]), 
)
