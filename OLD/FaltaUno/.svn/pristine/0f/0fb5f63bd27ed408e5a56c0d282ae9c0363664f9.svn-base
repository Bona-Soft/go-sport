CREATE PROCEDURE [dbo].[GetFrecuentlyPlayers]
	@UserID BIGINT,
	@SportID TINYINT
AS
BEGIN

	SELECT TOP 10 *
	FROM
		Players p
	WHERE
		p.UserID <> @UserID
		AND p.SportID = @SportID
		
END