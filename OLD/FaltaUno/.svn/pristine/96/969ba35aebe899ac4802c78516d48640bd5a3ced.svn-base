CREATE TABLE [dbo].[MatchTeamRequests]
(
	[MatchTeamRequestID] BIGINT NOT NULL PRIMARY KEY,
	[MatchID] BIGINT NOT NULL,
	[TeamRequestSenderID] INT NOT NULL,
	[TeamRequestReceiverID] INT NOT NULL,
	[CommentID] BIGINT NULL, 
    [RequestStateID] NCHAR(10) NOT NULL CONSTRAINT MatchTeamRequests_RequestStateID_Default DEFAULT 1, 
    [RecieveDate] DATETIME NOT NULL CONSTRAINT MatchTeamRequests_ReceiveData_Default DEFAULT GETUTCDATE(), 
    [LastStateChangeDate] DATETIME NULL
)
