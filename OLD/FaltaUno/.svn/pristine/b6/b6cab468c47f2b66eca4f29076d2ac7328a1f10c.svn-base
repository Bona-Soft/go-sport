CREATE PROCEDURE [dbo].[GetPlayerFields]
	@PlayerID BIGINT
AS
BEGIN

	SELECT F.* 
	FROM 
		PlayerFields P
		INNER JOIN Fields F ON F.FieldID = P.FieldID
	WHERE 
		P.PlayerID = @PlayerID

END
	