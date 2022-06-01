CREATE PROCEDURE [dbo].[GetRecommendedPlayers]
	@UserID BIGINT,
	@MatchID BIGINT
AS
BEGIN
	
	DECLARE @SportID TINYINT 
	SELECT @SportID = SportID FROM Matches WHERE MatchID = @MatchID

	SELECT TOP 10 p.*
	FROM
		Players p
	WHERE
		p.UserID <> @UserID
		AND p.SportID = @SportID
END