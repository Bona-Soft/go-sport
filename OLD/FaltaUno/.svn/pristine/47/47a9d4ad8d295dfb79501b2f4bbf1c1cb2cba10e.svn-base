CREATE PROCEDURE [dbo].[UpdateAvatar]
	@UserID BIGINT,
	@Avatar NVARCHAR(255)
AS
BEGIN
	
	UPDATE Users
	SET Avatar = @Avatar
	WHERE UserID = @UserID
	
	RETURN 1
END
