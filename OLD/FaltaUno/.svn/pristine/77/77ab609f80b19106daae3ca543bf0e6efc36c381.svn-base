CREATE PROCEDURE [dbo].[AddUserPrivacy]
	@UserID BIGINT,
	@Entity VARCHAR(255),
	@Property VARCHAR(255),
	@Value CHAR(1)
AS
BEGIN

	DECLARE @UserPrivacyDefaultID BIGINT

	SELECT @UserPrivacyDefaultID = UserPrivacyDefaultID 
	FROM UserPrivacyDefault
	WHERE 
		@Entity = Entity
		AND @Property = Property

	IF @UserPrivacyDefaultID IS NULL
	BEGIN
		RETURN -1
	END

	INSERT INTO UserPrivacy
		(UserID,
		UserPrivacyDefaultID,
		[Value])
	VALUES
		(@UserID,
		@UserPrivacyDefaultID,
		@Value)

	RETURN SCOPE_IDENTITY()
END