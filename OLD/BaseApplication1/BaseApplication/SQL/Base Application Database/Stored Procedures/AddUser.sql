CREATE PROCEDURE [dbo].[AddUser]
	@Username VARCHAR(255),
	@Password VARCHAR(255),
	@Name VARCHAR(255),
	@LastName VARCHAR(255),
	@ImplementationID INT = 0,
	@VerificationCode VARCHAR(255) = null,
	@DefaultLanguage VARCHAR(2) = 'EN'
AS BEGIN
	DECLARE
		@_Username VARCHAR(255) = @Username,
		@_ImplementationID INT = @ImplementationID,
		@Lock BIT = 0

	IF (ISNULL(@ImplementationID,0) = 0) AND NOT EXISTS(SELECT 1 FROM Implementations WHERE ImplementationID = 0)
	BEGIN
		INSERT INTO Implementations (ImplementationID,[Description]) VALUES (0,'Default')
	END

	IF EXISTS(SELECT 1 FROM LoginNames WHERE LoginName = @_Username AND ImplementationID = @_ImplementationID )
	BEGIN
		RETURN -1
	END
	
	IF @VerificationCode IS NOT NULL
	BEGIN
		SET @Lock = 1
	END

	INSERT INTO Users
		(ImplementationID,
		Name,
		LastName,
		DefaultLanguage)
	VALUES
		(@ImplementationID,
		@Name,
		@LastName,
		@DefaultLanguage)

	DECLARE @UserID BIGINT = SCOPE_IDENTITY()

	INSERT INTO LoginNames
		([ImplementationID],
		[UserID],
		[LoginName],
		[Blocked],
		[Locked],
		[Deleted],
		[VerificationCode])
	VALUES
		(@ImplementationID,
		@UserID,
		@Username,
		0,
		@Lock,
		0,
		@VerificationCode)
	
	DECLARE @LoginID BIGINT = SCOPE_IDENTITY()
	
	INSERT INTO LoginPasswords
		([LoginID],
		[Password],
		[Sequence])
	VALUES
		(@LoginID,
		@Password,
		1)

	RETURN @UserID
END