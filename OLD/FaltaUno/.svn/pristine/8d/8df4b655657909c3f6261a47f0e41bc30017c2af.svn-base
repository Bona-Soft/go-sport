CREATE TABLE [dbo].[MatchTypes]
(
	[MatchTypeID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[SportID] TINYINT NOT NULL, 
	[Value] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(255) NOT NULL, 
   [Default] BIT NOT NULL CONSTRAINT [MatchTypes_Default_Default] DEFAULT 0, 
	[DefaultFieldID] INT NULL,
	[UniqueField] BIT NOT NULL CONSTRAINT [MatchTypes_UniqueField_Default] DEFAULT 0
   CONSTRAINT [FK_MatchTypes_Sports_SportID] FOREIGN KEY ([SportID]) REFERENCES [Sports]([SportID]), 
    [PlayersQuantity] TINYINT NULL, 
    CONSTRAINT [FK_MatchTypes_Fields_FieldID] FOREIGN KEY ([DefaultFieldID]) REFERENCES [Fields]([FieldID])
)
