CREATE PROCEDURE [dbo].[RemoveUser]
	@UserID BIGINT
AS
BEGIN
	DECLARE @_UserID BIGINT = @UserID

	UPDATE Users
	SET Deleted = 1
	WHERE UserID = @_UserID

	UPDATE LoginNames
	SET Deleted = 1
	WHERE UserID = @_UserID

END