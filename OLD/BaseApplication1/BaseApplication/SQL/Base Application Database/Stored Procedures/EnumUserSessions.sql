CREATE PROCEDURE [dbo].[EnumUserSessions]
	@UserID BIGINT
AS
BEGIN
	DECLARE @_UserID BIGINT = @UserID

	SELECT 
		[Session] 
	FROM 
		[Sessions] S
		INNER JOIN  LoginNames LN 
			ON LN.UserID = @_UserID 
			AND LN.Deleted = 0 
			AND LN.LoginID = S.LoginID
END