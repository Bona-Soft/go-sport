CREATE PROCEDURE [dbo].[GetMatchPlayerRequestsByUserID]
	@UserID BIGINT
AS
BEGIN

	DECLARE @_UserID BIGINT = @UserID

	SELECT M.*
	FROM
		MatchPlayerRequests M
		INNER JOIN Players P ON P.PlayerID = M.PlayerRequestReceiverID
	WHERE
		P.UserID = @_UserID

END
