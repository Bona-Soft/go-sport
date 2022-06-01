CREATE PROCEDURE [dbo].[GetFields]
	@SportID TINYINT = NULL
AS

	SELECT * 
	FROM 
		Fields 
	WHERE  
		@SportID IS NULL
			OR SportID = @SportID