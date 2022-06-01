CREATE TABLE [dbo].[ChallengeTypes]
(
	[ChallengeTypeID] TINYINT NOT NULL PRIMARY KEY, 
    [Value] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    [Default] BIT NOT NULL DEFAULT 0
)
