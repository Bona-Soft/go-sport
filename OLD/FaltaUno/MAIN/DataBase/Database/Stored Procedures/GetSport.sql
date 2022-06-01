CREATE PROCEDURE [dbo].[GetSport]
	@SportID tinyint
AS

	SELECT 
		SportId,
		[Value],
		Name
	FROM
		Sports
	WHERE
		SportID = @SportID 

