CREATE TABLE [dbo].[Cities]
(
	[CityId]   INT IDENTITY (1, 1),
	[RegionId]            INT            NOT NULL, 
	[CountryId]            INT            NOT NULL, 
	[OID]            INT            NOT NULL, 
	[CityNameRu]       NVARCHAR(50)             NOT NULL,
	[CityNameEn]       NVARCHAR(50)             NOT NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([CityId]), 
	CONSTRAINT [FK_Cities_Regions] FOREIGN KEY ([RegionId]) REFERENCES [Regions]([RegionId]),
    CONSTRAINT [FK_Cities_Countries] FOREIGN KEY ([CountryId]) REFERENCES [Countries]([CountryId]),
)
