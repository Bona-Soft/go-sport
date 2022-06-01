﻿CREATE PROCEDURE [dbo].[JobRefreshStatusMatches]
AS
BEGIN

	DECLARE @SCALAR_MATCHSTATE_FINISHED TINYINT= dbo.Scalar_MatchState_Finished(),
		@SCALAR_MATCHSTATE_INPROGRESS TINYINT= dbo.Scalar_MatchState_InProgress(),
		@UTCNOW DateTime = GETUTCDATE()

	UPDATE Matches
	SET 
		MatchStateID = @SCALAR_MATCHSTATE_INPROGRESS 
	WHERE
		MatchDateTime < @UTCNOW
		AND DATEADD(hh,2,MatchDateTime) > @UTCNOW

	UPDATE Matches
	SET
		MatchStateID = @SCALAR_MATCHSTATE_FINISHED
	WHERE
		DATEADD(hh,2,MatchDateTime) < @UTCNOW
		AND MatchStateID <> @SCALAR_MATCHSTATE_FINISHED
END