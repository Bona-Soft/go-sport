CREATE PROCEDURE [dbo].[GetUserByEmail]
	@Email VARCHAR(255)
AS
	DECLARE @_Email VARCHAR(255) = @Email


	SELECT
		*
	FROM
		Users U
		INNER JOIN LoginNames L ON L.UserID = U.UserID
	WHERE
		 L.LoginName = @_Email
		AND U.Deleted = 0
