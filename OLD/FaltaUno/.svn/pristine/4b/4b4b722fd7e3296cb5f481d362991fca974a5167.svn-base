CREATE PROCEDURE [dbo].[GetPlayerPositions]
	@PlayerID BIGINT
AS
BEGIN

	SELECT FP.* 
	FROM 
		PlayerFieldPositions P
		INNER JOIN FieldPositions FP ON FP.FieldPositionID = P.FieldPositionID
	WHERE
		P.PlayerID = @PlayerID

END
