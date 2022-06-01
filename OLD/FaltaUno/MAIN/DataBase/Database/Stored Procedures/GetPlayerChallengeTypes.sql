CREATE PROCEDURE [dbo].[GetPlayerChallengeTypes]
	@PlayerID BIGINT
AS
BEGIN

	SELECT C.*
	FROM
		PlayerChallengeTypes P
		INNER JOIN ChallengeTypes C ON C.ChallengeTypeID = P.ChallengeTypeID 
	WHERE
		P.PlayerID = @PlayerID

END