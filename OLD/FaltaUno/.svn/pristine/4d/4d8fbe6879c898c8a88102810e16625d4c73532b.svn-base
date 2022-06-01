CREATE PROCEDURE [dbo].[GetMatchesByUser] 
	@UserID BIGINT,
	@SportID TINYINT = NULL,
	@MatchRequestType CHAR(1) = NULL,
	@MatchStateID SMALLINT = NULL,
	@Closed BIT = NULL,
	@PageNumber SMALLINT = 0, --0 = without pagination
 	@PageSize INT = 10, 
	@AscOrder BIT = 1
AS
BEGIN

	DECLARE @Qty BIGINT = @PageSize * @PageNumber

	IF @AscOrder = 1 
	BEGIN
		;WITH x AS (
			SELECT 
				M.MatchID, 
				k = ROW_NUMBER() OVER (ORDER BY M.MatchDateTime ASC) 
			FROM
			Matches M
			INNER JOIN MatchPlayerRequests R ON R.MatchID = M.MatchID 
			INNER JOIN MatchPlayerRequestStates RS ON RS.MatchPlayerRequestStateID = R.MatchPlayerRequestStateID 
			INNER JOIN Players PR ON PR.PlayerID = R.PlayerRequestReceiverID
			INNER JOIN Players PM ON PM.PlayerID = M.PlayerID
			INNER JOIN MatchStates S ON S.MatchStateID = M.MatchStateID AND (@MatchStateID IS NULL OR S.MatchStateID = @MatchStateID)
		WHERE
			PR.UserID = @UserID 
			AND (PM.UserID = @UserID OR (@MatchRequestType IS NULL OR RS.Type = @MatchRequestType))
			AND (@Closed IS NULL OR S.Closed = @Closed)
			AND (@SportID IS NULL OR M.SportID = @SportID)
		)
		SELECT M.*
		FROM x INNER JOIN Matches AS M
		ON x.MatchID = M.MatchID
		WHERE 
			(@PageNumber = 0 
				OR x.k BETWEEN (((@PageNumber - 1) * @PageSize) + 1) AND @Qty)
		ORDER BY x.K;
	
	END ELSE BEGIN
		;WITH x AS (
			SELECT 
				M.MatchID, 
				k = ROW_NUMBER() OVER (ORDER BY M.MatchDateTime DESC) 
			FROM
			Matches M
			INNER JOIN MatchPlayerRequests R ON R.MatchID = M.MatchID 
			INNER JOIN MatchPlayerRequestStates RS ON RS.MatchPlayerRequestStateID = R.MatchPlayerRequestStateID 
			INNER JOIN Players PR ON PR.PlayerID = R.PlayerRequestReceiverID
			INNER JOIN Players PM ON PM.PlayerID = M.PlayerID
			INNER JOIN MatchStates S ON S.MatchStateID = M.MatchStateID AND (@MatchStateID IS NULL OR S.MatchStateID = @MatchStateID)
		WHERE
			PR.UserID = @UserID 
			AND (PM.UserID = @UserID OR (@MatchRequestType IS NULL OR RS.Type = @MatchRequestType))
			AND (@Closed IS NULL OR S.Closed = @Closed)
			AND (@SportID IS NULL OR M.SportID = @SportID)
		)
		SELECT M.*
		FROM x INNER JOIN Matches AS M
		ON x.MatchID = M.MatchID
		WHERE 
			(@PageNumber = 0 
				OR x.k BETWEEN (((@PageNumber - 1) * @PageSize) + 1) AND @Qty)
		ORDER BY x.K;
	END
			
END