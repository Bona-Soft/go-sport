CREATE PROCEDURE [dbo].[ValidateUsername]
	@Username VARCHAR(255)
AS
	DECLARE @_Username VARCHAR(255) = @Username

	RETURN ISNULL((SELECT 1 from LoginNames WHERE LoginName = @_Username),0)