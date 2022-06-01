CREATE PROCEDURE [dbo].[Logout]
	@Session VARCHAR(255),
	@LoginID BIGINT = NULL,
	@AllSessions BIT = 0
AS
BEGIN

	DECLARE 
		@UserID BIGINT,
		@_LoginID BIGINT = @LoginID,
		@_Session VARCHAR(255) = @Session

	SELECT 
		TOP 1 @UserID = LN.UserID 
	FROM 
		LoginNames LN 
		INNER JOIN [Sessions] S ON [Session] = @_Session AND LN.LoginID = S.LoginID
	WHERE 
		LN.LoginID = @_LoginID OR @_LoginID IS NULL

	DELETE S
	FROM 
		[Sessions] S
		INNER JOIN LoginNames L ON L.LoginID = S.LoginID AND L.UserID = @UserID
	WHERE 
		S.[Session] = @_Session
		OR (@AllSessions = 1)
END