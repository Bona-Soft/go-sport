CREATE TABLE [dbo].[MatchPlayers]
(
	[MatchID] BIGINT NOT NULL , 
    [PlayerID] BIGINT NOT NULL, 
    PRIMARY KEY ([PlayerID], [MatchID]), 
    CONSTRAINT [FK_MatchPlayers_Matches] FOREIGN KEY ([MatchID]) REFERENCES [Matches]([MatchID]),
    CONSTRAINT [FK_MatchPlayers_Players] FOREIGN KEY ([PlayerID]) REFERENCES [Players]([PlayerID])
)
