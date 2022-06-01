CREATE PROCEDURE [dbo].[VerifyUserEmail]
	@Username VARCHAR(255),
	@VerificationCode VARCHAR(255),
	@ImplementationID INT = 0
AS
BEGIN
	DECLARE 
		@_Username VARCHAR(255) = @Username,
		@_ImplementationID INT = ISNULL(@ImplementationID,0)

	IF NOT EXISTS(SELECT 1 FROM LoginNames WHERE ImplementationID = @_ImplementationID AND LoginName = @_Username)
	BEGIN
		RETURN -1
	END
	IF EXISTS(SELECT 1 FROM LoginNames WHERE ImplementationID = @_ImplementationID AND LoginName = @_Username AND VerificationCode IS NULL)
	BEGIN
		RETURN -2
	END
	IF NOT EXISTS(SELECT 1 FROM LoginNames WHERE ImplementationID = @_ImplementationID AND LoginName = @_Username AND VerificationCode = @VerificationCode)
	BEGIN
		RETURN -3
	END

	UPDATE 
		LoginNames
	SET
		Locked = 0,
		VerificationCode = null
	WHERE ImplementationID = @_ImplementationID 
		AND LoginName = @_Username

	RETURN 0
END

