CREATE PROCEDURE [dbo].[UpdateMatch]
	@MatchID BIGINT,
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
	@Comments NVARCHAR(MAX) = NULL
AS
BEGIN
	
	UPDATE Matches
	SET [Name] = @Name,
		LocationID = @LocationID,
		HeadquarterID = @HeadquarterID,
		FieldID = @FieldID,
		[Public] = @Public,
		MatchDateTime = @MatchDateTime,
		MaxPlayers = @MaxPlayers,
		MinPlayers = @MinPlayers,
		LimitlessPlayers = @LimitlessPlayers,
		MatchTypeID = @MatchTypeID,
		ChallengeTypeID = @ChallengeTypeID,
		AverageAge = @AverageAge,
		MatchStateID = @MatchStateID,
		Comments = @Comments
	WHERE 
		MatchID = @MatchID
	
END
