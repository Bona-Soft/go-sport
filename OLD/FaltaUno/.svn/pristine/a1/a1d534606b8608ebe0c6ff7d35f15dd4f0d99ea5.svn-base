CREATE PROCEDURE [dbo].[AddComment]
	@Text VARCHAR(MAX),
	@UserID BIGINT
AS
BEGIN

	INSERT INTO Comments
		([Text],
		UserID)
	VALUES
		(@Text,
		@UserID)

	RETURN SCOPE_IDENTITY()

END
