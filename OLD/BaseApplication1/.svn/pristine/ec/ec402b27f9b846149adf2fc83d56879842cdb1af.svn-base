CREATE PROCEDURE [dbo].[RemoveAllUserSession]
	@UserID BIGINT
AS
BEGIN
	DECLARE @_UserID BIGINT = @UserID

	DELETE S 
	FROM 
		[Sessions] S
		INNER JOIN LoginNames LN ON LN.UserID = @_UserID AND LN.LoginID = S.LoginID
END
