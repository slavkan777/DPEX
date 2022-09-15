CREATE TABLE [dbo].[Regions]
(
	[RegionId]   INT IDENTITY (1, 1),
	[CountryId]       int            NOT NULL,
	[OID]       int            NOT NULL,
	[RegionNameRu]       NVARCHAR(50)             NOT NULL,
	[RegionNameEn]       NVARCHAR(50)             NOT NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY ([RegionId]), 
	CONSTRAINT [FK_Regions_Countries] FOREIGN KEY ([CountryId]) REFERENCES [Countries]([CountryId]),
)
