CREATE PROCEDURE [dbo].[AddUserLogin]
	@UserID BIGINT,
	@Username VARCHAR(255),
	@Password VARCHAR(255),
	@ImplementationID INT = NULL
AS BEGIN
	DECLARE 
		@Sequence TINYINT = 1,
		@LoginID BIGINT = 0,
		@_ImplementationID INT = @ImplementationID,
		@_UserID BIGINT = @UserID,
		@_Username VARCHAR(255) = @Username

	IF NOT EXISTS(SELECT 1 FROM Users WHERE ImplementationID = ISNULL(@_ImplementationID,ImplementationID) AND UserID = @_UserID) BEGIN
		RETURN -2
	END

	IF NOT EXISTS(SELECT 1 FROM LoginNames WHERE UserID <> @_UserID AND LoginName = @_Username) BEGIN
		SELECT @ImplementationID = ImplementationID FROM Users WHERE UserID = @_UserID

		IF NOT EXISTS(SELECT 1 FROM LoginNames WHERE UserID = @_UserID AND LoginName = @_Username) BEGIN
			INSERT INTO LoginNames
				(ImplementationID,UserID,LoginName,Blocked,Locked,Deleted)
			VALUES
				(ISNULL(@ImplementationID,0),@UserID,@Username,0,0,0)

			SET @LoginID = SCOPE_IDENTITY()
		END ELSE BEGIN
			SELECT @LoginID = LoginID FROM LoginNames WHERE UserID = @_UserID AND LoginName = @_Username 
			SELECT @Sequence = MAX([Sequence]) + 1 FROM LoginPasswords WHERE LoginID = @LoginID 
		END
		
		INSERT INTO LoginPasswords
			(LoginID,[Password],[Sequence])
		VALUES
			(@LoginID,@Password,@Sequence)
	END ELSE BEGIN
		SET @LoginID = -1
	END

	RETURN @LoginID
END
