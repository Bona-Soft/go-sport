CREATE PROCEDURE [dbo].[DeleteLogin]
	@Username VARCHAR(255),
	@ImplementationID INT = 0
AS
	DECLARE 
		@_Username VARCHAR(255) = @Username,
		@_ImplementationID INT = @ImplementationID,
		@LoginID BIGINT 
		
	SELECT 
		@LoginID = [LoginId] 
	FROM 
		LoginNames 
	WHERE 
		UPPER(LoginName) = UPPER(@_Username) 
		AND ImplementationID = @_ImplementationID

	DELETE LoginPasswords
	WHERE LoginID = @LoginID

	DELETE [Sessions]
	WHERE LoginID = @LoginID

	DELETE LoginNames
	WHERE LoginID = @LoginID

