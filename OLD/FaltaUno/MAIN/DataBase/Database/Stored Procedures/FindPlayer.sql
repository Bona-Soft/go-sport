CREATE PROCEDURE [dbo].[FindPlayer]
	@UserID BIGINT,
	@SportID TINYINT
AS
BEGIN

	SELECT *
	FROM
		Players
	WHERE
		UserID = @UserID 
		AND SportID = @SportID 

END
	
