CREATE PROCEDURE [dbo].[GetPlayersByUserID]
	@UserID BIGINT
AS
	DECLARE @_UserID BIGINT = @UserID

	SELECT 
		*
	FROM
		Players
	WHERE
		UserID = @_UserID
		AND Deleted = 0
