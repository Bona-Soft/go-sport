CREATE TABLE [dbo].[MatchPlayerRequests]
(
	[MatchPlayerRequestID] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	[MatchID] BIGINT NOT NULL,
    [PlayerRequestSenderID] BIGINT NOT NULL, 
    [PlayerRequestReceiverID] BIGINT NOT NULL, 
    [CommentID] BIGINT NULL, 
    [MatchPlayerRequestStateID] NCHAR(10) NOT NULL DEFAULT 1, 
    [RecieveDate] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LastStateChangeDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_MatchPlayerRequests_Matches] FOREIGN KEY ([MatchID]) REFERENCES [Matches]([MatchID]), 
    CONSTRAINT [FK_MatchPlayerRequests_Players_Sender] FOREIGN KEY ([PlayerRequestSenderID]) REFERENCES [Players]([PlayerID]),
	CONSTRAINT [FK_MatchPlayerRequests_Players_Receiver] FOREIGN KEY ([PlayerRequestReceiverID]) REFERENCES [Players]([PlayerID])
)
