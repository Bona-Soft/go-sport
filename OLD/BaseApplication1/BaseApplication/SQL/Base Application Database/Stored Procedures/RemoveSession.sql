CREATE PROCEDURE [dbo].[RemoveSession]
	@Session VARCHAR(255)
AS
BEGIN
	DECLARE @_Session VARCHAR(255) = @Session

	DELETE FROM [Sessions]
	WHERE [Session] = @_Session 
END
