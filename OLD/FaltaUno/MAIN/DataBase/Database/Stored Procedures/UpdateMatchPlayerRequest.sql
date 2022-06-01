CREATE PROCEDURE [dbo].[UpdateMatchPlayerRequest]
	@MatchPlayerRequestID BIGINT,
	@MatchPlayerRequestStateID TINYINT,
	@CommentID BIGINT = NULL
AS
BEGIN
	DECLARE @_MatchPlayerRequestID BIGINT = @MatchPlayerRequestID

	UPDATE MatchPlayerRequests
	SET MatchPlayerRequestStateID  = @MatchPlayerRequestStateID, 
		LastStateChangeDate = GETUTCDATE(),
		CommentID = @CommentID
	WHERE 
		MatchPlayerRequestID = @_MatchPlayerRequestID

	RETURN @@ROWCOUNT
END
