CREATE PROCEDURE [dbo].[AddMatch]
	@SportID TINYINT,
	@Name VARCHAR(255), 
	@LocationID BIGINT NULL,
	@HeadquarterID INT NULL, 
	@FieldID INT NULL,
	@Public TINYINT = 0,
	@MatchDateTime DATETIME, 
	@MaxPlayers TINYINT NULL, 
	@MinPlayers TINYINT NULL, 
	@LimitlessPlayers BIT = 0, 
	@MatchTypeID TINYINT, 
	@ChallengeTypeID TINYINT,
	@AverageAge INT NULL, 
	@MatchStateID TINYINT = 1, 
	@PlayerID BIGINT,
	@Comments NVARCHAR(MAX) = NULL,
	@RegisterUserID BIGINT
AS
BEGIN

	INSERT INTO MATCHES
		(SportID,
		[Name],
		LocationID,
		HeadquarterID,
		FieldID,
		[Public],
		MatchDateTime,
		MaxPlayers,
		MinPlayers,
		LimitlessPlayers,
		MatchTypeID,
		ChallengeTypeID,
		AverageAge,
		MatchStateID,
		PlayerID,
		Comments,
		RegisterUserID)
	VALUES
		(@SportID,
		@Name,
		@LocationID,
		@HeadquarterID,
		@FieldID,
		@Public,
		@MatchDateTime,
		@MaxPlayers,
		@MinPlayers,
		@LimitlessPlayers,
		@MatchTypeID,
		@ChallengeTypeID,
		@AverageAge,
		@MatchStateID,
		@PlayerID,
		@Comments,
		@RegisterUserID)

	RETURN SCOPE_IDENTITY()
END
