CREATE PROCEDURE [dbo].[AddMatchPlayerRequest]
	@MatchID BIGINT,
	@PlayerRequestSenderID BIGINT,
	@PlayerRequestReceiverID BIGINT,
	@CommentID BIGINT = NULL,
	@MatchPlayerRequestStateID TINYINT
AS
BEGIN

	DECLARE @_Now DATETIME = GETUTCDATE()

	INSERT INTO MatchPlayerRequests
		(MatchID,
		PlayerRequestSenderID,
		PlayerRequestReceiverID,
		CommentID,
		MatchPlayerRequestStateID, 
		RecieveDate,
		LastStateChangeDate)
	VALUES
		(@MatchID,
		@PlayerRequestSenderID,
		@PlayerRequestReceiverID,
		@CommentID,
		@MatchPlayerRequestStateID,
		@_Now,
		@_Now)

	RETURN SCOPE_IDENTITY()
END