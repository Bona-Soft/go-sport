CREATE PROCEDURE [dbo].[DeleteUser]
	@UserID BIGINT
AS
BEGIN
	DECLARE 
		@_UserID BIGINT = @UserID,
		@LoginID BIGINT

	SELECT TOP 1 @LoginID = LoginID FROM LoginNames WHERE UserID = @_UserID

	DELETE FROM [Sessions] WHERE LoginID = @LoginID

	DELETE FROM LoginPasswords WHERE LoginID = @LoginID

	DELETE FROM LoginNames WHERE LoginID = @LoginID

	DELETE FROM UserIPAccessStatus WHERE UserID = @_UserID

	DELETE FROM Players WHERE UserID = @_UserID

	DELETE FROM Users WHERE UserID = @_UserID

END
