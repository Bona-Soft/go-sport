CREATE PROCEDURE [dbo].[GetMatchPlayerRequest]
	@MatchPlayerRequestID BIGINT
AS
BEGIN
	DECLARE @_MatchPlayerRequestID BIGINT = @MatchPlayerRequestID

	SELECT *
	FROM
		MatchPlayerRequests
	WHERE
		MatchPlayerRequestID = @_MatchPlayerRequestID 

END
