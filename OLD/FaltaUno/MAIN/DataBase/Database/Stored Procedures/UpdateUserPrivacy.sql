CREATE PROCEDURE [dbo].[UpdateUserPrivacy]
	@UserPrivacyID BIGINT,
	@UserID BIGINT,
	@Value CHAR(1)
AS
BEGIN

	UPDATE UserPrivacy
	SET [Value] = @Value
	WHERE
		UserPrivacyID = @UserPrivacyID
		AND UserID = @UserID 

	RETURN @UserPrivacyID
END
