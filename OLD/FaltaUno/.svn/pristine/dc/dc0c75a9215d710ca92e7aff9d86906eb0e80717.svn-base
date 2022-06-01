CREATE PROCEDURE [dbo].[GetMatchTypes]
	@SportID TINYINT = NULL
AS
BEGIN

	SELECT * 
	FROM 
		MatchTypes 
	WHERE  
		@SportID IS NULL
		OR SportID = @SportID

END