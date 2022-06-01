CREATE PROCEDURE [dbo].[SearchPlayers]
	@UserID BIGINT = NULL,
	@SportID TINYINT = NULL,
	@Alias VARCHAR(255) = NULL,
	@Name VARCHAR(255) = NULL,
	@LastName VARCHAR(255) = NULL,
	@Email VARCHAR(255) = NULL,
	@SearchText VARCHAR(255) = NULL,
	@MinAge TINYINT = NULL,
	@MaxAge TINYINT = NULL,
	@LocationID BIGINT = NULL,
	@HeadquarterID INT = NULL,
	@Lat FLOAT = NULL,
	@Lng FLOAT = NULL,
	@TOP SMALLINT = 20,
	@RadiusKM SMALLINT = 10
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @_UserID BIGINT = @UserID,
		@_SportID TINYINT = @SportID,
		@_SearchText VARCHAR(255) = '%' + @SearchText + '%',
		@_Alias VARCHAR(255) = '%' + @Alias + '%',
		@_Name VARCHAR(255) = '%' + @Name + '%',
		@_LastName VARCHAR(255) = '%' + @LastName + '%',
		@_MinAge TINYINT = @MinAge,
		@_MaxAge TINYINT = @MaxAge,
		@_LocationID BIGINT = @LocationID,
		@_HeadquarterID INT = @HeadquarterID,
		@_GeoPosition GEOGRAPHY = NULL,
		@_Email VARCHAR(255) = @Email

	IF (@Lat IS NOT NULL AND @Lng IS NOT NULL) 
	BEGIN
		SET @_GeoPosition = geography::Point(@Lat, @Lng, 4326);
	END 
		
	SELECT DISTINCT TOP(@TOP) P.* 
	FROM
		Players P
		INNER JOIN Users U ON U.UserID = P.UserID AND (@UserID IS NULL OR P.UserID <> @UserID)
		INNER JOIN LoginNames LN ON U.UserID = LN.UserID
		LEFT JOIN MatchesPlayers MP ON MP.PlayerID = P.PlayerID
		LEFT JOIN Matches M ON ( MP.MatchID = M.MatchID ) --M.MatchStateID != 5 AND M.MatchStateID != 4 AND
		LEFT JOIN Locations L ON L.LocationID = M.LocationID AND (@_LocationID IS NULL OR @_LocationID = L.LocationID)
		LEFT JOIN Headquarters H ON H.HeadquarterID = M.HeadquarterID AND (@_HeadquarterID IS NULL OR @_HeadquarterID = H.HeadquarterID)
	WHERE
		(@_SportID IS NULL OR P.SportID = @_SportID) 
		AND (@_Alias IS NULL OR U.[Alias] LIKE @_Alias)
		AND (@_Name IS NULL OR U.[Name] LIKE @_Name)
		AND (@_LastName IS NULL OR U.[LastName] LIKE @_LastName)
		AND (@_Email IS NULL OR LN.LoginName = @_Email)
		AND (@_SearchText IS NULL 
			OR U.[LastName] LIKE @_LastName
			OR U.[Name] LIKE @_SearchText 
			OR U.[Alias] LIKE @_SearchText)
		AND (@_MinAge IS NULL OR @_MinAge < DATEDIFF(DAY, U.BirthDate, GetDate()) / 365.25)
		AND (@_MaxAge IS NULL OR @MaxAge > DATEDIFF(DAY, U.BirthDate, GetDate()) / 365.25)
		AND (@_GeoPosition IS NULL OR (@_GeoPosition.STDistance(geography::Point(ISNULL(L.Lat,0), ISNULL(L.Lng,0), 4326)) < (@RadiusKM * 1000)))
END
