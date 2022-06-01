CREATE PROCEDURE [dbo].[UpdatePlayer]
	@PlayerID BIGINT, 
	@Alias NVARCHAR(255),
	@Active BIT
AS
BEGIN
	
	UPDATE 
		Players
	SET 
		Alias = @Alias,
		Active = @Active
	WHERE
		PlayerID = @PlayerID

	RETURN @PlayerID
END
	