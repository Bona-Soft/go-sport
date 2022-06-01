CREATE PROCEDURE [dbo].[GetMatchStates]
	@MatchStateID SMALLINT = NULL
AS

	SELECT * 
	FROM 
		MatchStates 
	WHERE  
		@MatchStateID IS NULL 
		OR MatchStateID = @MatchStateID 
