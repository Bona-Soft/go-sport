CREATE PROCEDURE [dbo].[EnumUserLogins]
	@UserID BIGINT
AS
BEGIN
	DECLARE @_UserID BIGINT = @UserID

	SELECT 
		LoginID,
		LoginName,
		DateTimeCreated 
	FROM 
		LoginNames
	WHERE
		UserID = @_UserID
		AND [Deleted] = 0
END