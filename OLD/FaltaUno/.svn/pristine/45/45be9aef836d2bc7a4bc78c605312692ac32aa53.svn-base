CREATE PROCEDURE [dbo].[SearchMatch]
	@SportID TINYINT = NULL,
	@Name VARCHAR(255) = NULL,
	@PlayerID BIGINT = NULL,
	@MinAge TINYINT = NULL,
	@MaxAge TINYINT = NULL,
	@LocationID BIGINT = NULL,
	@HeadquarterID INT = NULL,
	@Lat FLOAT = NULL,
	@Lng FLOAT = NULL,
	@MatchTypeID SMALLINT = NULL,
	@FieldID INT = NULL,
	@ChallengeTypeID TINYINT = NULL,
	@StartDateTime DATETIME = NULL,
	@EndDateTime DATETIME = NULL,
	@Public BIT = NULL,
	@IsHotMatch BIT = NULL,
	@TOP SMALLINT = 20,
	@RadiusKM SMALLINT = 30
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @_SportID TINYINT = @SportID,
		@_Name VARCHAR(255) = '%' + @Name + '%',
		@_PlayerID BIGINT = @PlayerID,
		@_MinAge TINYINT = @MinAge,
		@_MaxAge TINYINT = @MaxAge,
		@_LocationID BIGINT = @LocationID,
		@_HeadquarterID INT = @HeadquarterID,
		@_MatchTypeID SMALLINT = @MatchTypeID,
		@_FieldID TINYINT = @FieldID,
		@_ChallengeTypeID TINYINT = @ChallengeTypeID,
		@_MatchStartTime DATETIME = @StartDateTime,
		@_MatchEndTime DATETIME = @EndDateTime,
		@_Public BIT = @Public,
		@_IsHotMatch BIT = @IsHotMatch,
		@_GeoPosition GEOGRAPHY = NULL

	IF (@Lat IS NOT NULL AND @Lng IS NOT NULL) 
	BEGIN
		SET @_GeoPosition = geography::Point(@Lat, @Lng, 4326);
	END 
		
	SELECT TOP(@TOP) M.* 
	FROM
		Matches M
		INNER JOIN Locations L ON L.LocationID = M.LocationID AND (@_LocationID IS NULL OR @_LocationID = L.LocationID)
		INNER JOIN MatchPlayerRequests MPR ON MPR.MatchID = M.MatchID AND (@_PlayerID IS NULL OR @_PlayerID = MPR.PlayerRequestReceiverID)
		LEFT JOIN Headquarters H ON H.HeadquarterID = M.HeadquarterID AND (@_HeadquarterID IS NULL OR @_HeadquarterID = H.HeadquarterID)
	WHERE
		(@_SportID IS NULL OR M.SportID = @_SportID) 
		AND (@_Name IS NULL OR M.[Name] LIKE @_Name)
		AND (@_MinAge IS NULL OR @_MinAge < AverageAge)
		AND (@_MaxAge IS NULL OR @MaxAge > AverageAge)
		AND (@_GeoPosition IS NULL OR @_GeoPosition.STDistance(geography::Point(L.Lat, L.Lng, 4326)) < (@RadiusKM * 1000))
		AND (@_MatchStartTime IS NULL  OR M.MatchDateTime >= @_MatchStartTime)
		AND (@_MatchEndTime IS NULL OR M.MatchDateTime < @_MatchEndTime)
		AND ((CONVERT(time(0), @_MatchEndTime) < CONVERT(time(0), @_MatchStartTime)
			AND (@_MatchStartTime IS NULL OR CONVERT(time(0), M.MatchDateTime) >= CONVERT(time(0), @_MatchStartTime))
				OR (@_MatchEndTime IS NULL OR CONVERT(time(0), M.MatchDateTime) <= CONVERT(time(0), @_MatchEndTime))) 
			OR (CONVERT(time(0), @_MatchEndTime) >= CONVERT(time(0), @_MatchStartTime)
			AND (@_MatchStartTime IS NULL OR CONVERT(time(0), M.MatchDateTime) >= CONVERT(time(0), @_MatchStartTime))
				AND (@_MatchEndTime IS NULL OR CONVERT(time(0), M.MatchDateTime) <= CONVERT(time(0), @_MatchEndTime))) )
		AND (@_MatchTypeID IS NULL OR M.MatchTypeID = @_MatchTypeID )
		AND (@_FieldID IS NULL OR M.FieldID = @_FieldID)
--		AND (@_IsHotMatch IS NULL OR  M.IsHotMatch )
		AND (@_Public IS NULL OR M.[Public] = @_Public)

END
