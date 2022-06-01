CREATE PROCEDURE [dbo].[GetHeadquarter]
	@HeadquarterID INT
AS
BEGIN
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
		HeadquarterID = @HeadquarterID
END
