CREATE PROCEDURE [dbo].[JobCloseExpiredMatchPlayerRequests]
AS
BEGIN

	DECLARE @SCALAR_MATCHPLAYERREQUESTSTATE_EXPIRED TINYINT = dbo.Scalar_MatchPlayerRequestState_Expired(),
			 @SCALAR_MATCHPLAYERREQUESTSTATE_COMPLETED TINYINT = dbo.Scalar_MatchPlayerRequestState_Completed(),
			 @SCALAR_MATCHPLAYERREQUESTSTATE_NOTCOMPLETED TINYINT = dbo.Scalar_MatchPlayerRequestState_NotCompleted(),
			 @SCALAR_MATCHSTATE_FINISHED TINYINT = dbo.Scalar_MatcState_Finished()

	UPDATE MPR
	SET 
		MPR.MatchPlayerRequestStateID = @SCALAR_MATCHPLAYERREQUESTSTATE_COMPLETED
	FROM 
		MatchPlayerRequests MPR
		INNER JOIN MatchPlayerRequestStates RS ON MPR.MatchPlayerRequestStateID = RS.MatchPlayerRequestStateID
		INNER JOIN Matches M ON M.MatchID = MPR.MatchID
	WHERE
		M.MatchStateID = @SCALAR_MATCHSTATE_FINISHED
		AND RS.[Type] = 'P' 
	
	UPDATE MPR
	SET 
		MPR.MatchPlayerRequestStateID = @SCALAR_MATCHPLAYERREQUESTSTATE_EXPIRED
	FROM 
		MatchPlayerRequests MPR
		INNER JOIN MatchPlayerRequestStates RS ON MPR.MatchPlayerRequestStateID = RS.MatchPlayerRequestStateID
		INNER JOIN Matches M ON M.MatchID = MPR.MatchID
	WHERE
		M.MatchStateID = @SCALAR_MATCHSTATE_FINISHED
		AND (RS.[Type] = 'I' OR RS.[Type] = 'T')
		 
	UPDATE MPR
	SET 
		MPR.MatchPlayerRequestStateID = @SCALAR_MATCHPLAYERREQUESTSTATE_COMPLETED
	FROM 
		MatchPlayerRequests MPR
		INNER JOIN MatchPlayerRequestStates RS ON MPR.MatchPlayerRequestStateID = RS.MatchPlayerRequestStateID
		INNER JOIN Matches M ON M.MatchID = MPR.MatchID
	WHERE
		M.MatchStateID = @SCALAR_MATCHSTATE_FINISHED
		AND RS.[Type] = 'N' 

END