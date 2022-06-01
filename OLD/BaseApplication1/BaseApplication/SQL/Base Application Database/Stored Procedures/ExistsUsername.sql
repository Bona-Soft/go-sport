CREATE PROCEDURE [dbo].[ExistsUsername]
	@Username VARCHAR(255),
	@ImplementationID INT = 0
AS
BEGIN
	DECLARE 
		@_Username VARCHAR(255) = @Username,
		@_ImplementationID INT = @ImplementationID

	IF EXISTS(SELECT 1 FROM LoginNames WHERE [LoginName] = @_Username AND ImplementationID = @_ImplementationID)
		RETURN 1
	RETURN -1
END