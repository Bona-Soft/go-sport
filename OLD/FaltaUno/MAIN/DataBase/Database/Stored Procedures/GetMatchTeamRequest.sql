CREATE PROCEDURE [dbo].[GetMatchTeamRequest]
	@MatchID BIGINT
AS
BEGIN
	DECLARE @_MatchID BIGINT = @MatchID

	SELECT *
	FROM
		MatchTeamRequests
	WHERE
		MatchID = @_MatchID

END
