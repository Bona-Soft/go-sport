CREATE PROCEDURE [dbo].[GetUser]
	@UserID BIGINT
AS
BEGIN
	DECLARE @_UserID BIGINT = @UserID


	SELECT
		*
	FROM
		Users U
	WHERE
		UserID = @_UserID
		AND Deleted = 0
END