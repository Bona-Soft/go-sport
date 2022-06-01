CREATE TABLE [dbo].[Headquarters]
(
	[HeadquarterID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[LocationID] BIGINT NULL,
	[SportID] TINYINT NULL,
	[Name] NVARCHAR(255), 
    CONSTRAINT [FK_Headquarters_Locations] FOREIGN KEY ([LocationID]) REFERENCES [Locations]([LocationID]),
	CONSTRAINT [FK_Headquarters_Sports] FOREIGN KEY ([SportID]) REFERENCES [Sports]([SportID])
)
