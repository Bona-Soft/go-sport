CREATE TABLE [dbo].[MatchStates]
(
	[MatchStateID] TINYINT NOT NULL PRIMARY KEY, 
    [Value] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    [Closed] BIT NOT NULL CONSTRAINT [MatchStates_Closed_Default] DEFAULT 0, 
    [Hot] BIT NOT NULL CONSTRAINT [MatchStates_Hot_Default] DEFAULT 0,
	 [Order] TINYINT NOT NULL
)
