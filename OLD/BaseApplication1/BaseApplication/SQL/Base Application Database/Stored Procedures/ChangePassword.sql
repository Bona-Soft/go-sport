CREATE PROCEDURE [dbo].[ChangePassword]
	@UserID BIGINT,
	@Password VARCHAR(255),
	@LoginName VARCHAR(255) = NULL
AS
BEGIN

	UPDATE P
	SET 
		[Password] = @Password
	FROM
		LoginPasswords P
		INNER JOIN LoginNames N ON P.LoginID = N.LoginID
	WHERE
		N.UserID = @UserID
		OR N.LoginName = @LoginName

	RETURN @@ROWCOUNT
END