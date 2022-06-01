CREATE PROCEDURE [dbo].[PURGE_Matches]
	@LimitDate DATETIME
AS
BEGIN

	DECLARE @_LimitDate DATETIME = @LimitDate

	DELETE 
		MPR
	FROM 
		MatchPlayerRequests MPR
		INNER JOIN Matches M ON M.MatchID = MPR.MatchID
	WHERE
		M.MatchDateTime < @_LimitDate

	DELETE 
		Matches
	WHERE
		MatchDateTime < @_LimitDate
END