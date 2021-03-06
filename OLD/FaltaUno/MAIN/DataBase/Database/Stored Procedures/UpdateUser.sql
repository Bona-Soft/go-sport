CREATE PROCEDURE [dbo].[UpdateUser]
	@UserID BIGINT,
	@DefaultSportID INT= NULL,
	@LastActiveSportID INT = NULL,
	@Prefix VARCHAR(255) = NULL,
	@Name VARCHAR(255) = NULL,
	@SecondNames VARCHAR(255) = NULL,
	@LastName VARCHAR(255) = NULL,
	@Suffix VARCHAR(255) = NULL,
	@Alias VARCHAR(255) = NULL,
	@MobileNumber VARCHAR(255) = NULL,
	@LinePhoneNumber VARCHAR(255) = NULL,
	@WebAddress VARCHAR(255) = NULL,
	@BirthDate DATETIME = NULL,
	@Height TINYINT = NULL,
	@Weight TINYINT = NULL,
	@DefaultLanguage VARCHAR(2) = 'EN'
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM Users WHERE UserID = @UserID)
		RETURN -1

	UPDATE
		Users
	SET 
		DefaultSportID = @DefaultSportID,
		LastActiveSportID = @LastActiveSportID,
		Prefix = @Prefix,
		Name = @Name,
		SecondNames = @SecondNames,
		LastName = @LastName ,
		Suffix = @Suffix ,
		Alias = @Alias ,
		MobileNumber = @MobileNumber ,
		LinePhoneNumber = @LinePhoneNumber ,
		WebAddress = @WebAddress ,
		BirthDate = @BirthDate,
		Height = @Height ,
		Weight = @Weight,
		DefaultLanguage = @DefaultLanguage
	WHERE
		UserId = @UserID

	RETURN @UserID
END
	
