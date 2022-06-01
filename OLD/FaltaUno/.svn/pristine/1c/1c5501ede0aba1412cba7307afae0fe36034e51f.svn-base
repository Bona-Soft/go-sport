CREATE PROCEDURE [dbo].[GetHeadquarters]
	@SportID TINYINT NULL,
	@SearchText NVARCHAR(255) NULL,
	@Lat FLOAT NULL,
	@Lng FLOAT NULL
AS
BEGIN

	DECLARE 
		@_SportID TINYINT = NULLIF(@SportID,0),
		@_SearchText NVARCHAR(255) = @SearchText,
		@_Lat FLOAT = NULLIF(@Lat,0),
		@_Lng FLOAT = NULLIF(@Lng,0)

	SELECT 
		HQ.[HeadquarterID],
		HQ.[SportID],
		HQ.[Name],
		HQ.[LocationID],
		L.[Lat] AS LocationLat,
		L.[Lng] AS LocationLng,
		L.[Display] AS LocationDisplay,
		L.[Value] AS LocationValue
	FROM
		Headquarters HQ
		LEFT JOIN Locations L ON HQ.LocationID = L.LocationID
	WHERE
		((L.[Lat] = @_Lat AND L.[Lng] = @_Lng)
			OR HQ.[Name] like '%' + ISNULL(@_SearchText,'') + '%')
		AND (HQ.[SportID] = ISNULL(@_SportID, HQ.[SportID]) OR HQ.[SportID] IS NULL)

END
	

