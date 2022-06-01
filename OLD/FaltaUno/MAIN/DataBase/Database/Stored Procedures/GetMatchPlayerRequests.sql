CREATE PROCEDURE [dbo].[GetMatchPlayerRequests]
	@MatchID BIGINT
AS
BEGIN
	DECLARE @_MatchID BIGINT = @MatchID

	SELECT *
	FROM
		MatchPlayerRequests
	WHERE
		MatchID = @_MatchID

END
