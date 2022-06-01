CREATE TABLE [dbo].[Matches]
(
	[MatchID] BIGINT NOT NULL CONSTRAINT [Matches_PK] PRIMARY KEY IDENTITY(1,1), 
	[SportID] TINYINT NOT NULL,
    [Name] VARCHAR(255) NOT NULL, 
    [LocationID] BIGINT NULL,
	[HeadquarterID] BIGINT NULL, 
	[FieldID] INT NULL,
    [Public] TINYINT DEFAULT 0,
	[MatchDateTime] DATETIME NOT NULL, 
    [MaxPlayers] TINYINT NULL, 
    [MinPlayers] TINYINT NULL, 
    [LimitlessPlayers] BIT NOT NULL CONSTRAINT [Matches_LimitlessPlayers_Default] DEFAULT 0, 
    [MatchTypeID] SMALLINT NULL, 
	[ChallengeTypeID] TINYINT NOT NULL CONSTRAINT [Matches_ChallengeTypeID_Default] DEFAULT 1,
    [AverageAge] INT NULL, 
    [MatchStateID] TINYINT NOT NULL CONSTRAINT [Matches_MatchStateID_Default] DEFAULT 1, 
    [PlayerID] BIGINT NOT NULL, 
	[Comments] NVARCHAR(MAX) NULL,
    [RegisterUserID] BIGINT NOT NULL, 
	[Deleted] BIT NOT NULL CONSTRAINT [Matches_Deleted_Default] DEFAULT ((0)), 
	[DateTimeCreated] DATETIME NOT NULL CONSTRAINT [Matches_DateTimeCreated_Default] DEFAULT GETUTCDATE()
)
