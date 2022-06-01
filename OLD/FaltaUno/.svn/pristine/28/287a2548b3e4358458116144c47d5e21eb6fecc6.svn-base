CREATE PROCEDURE [dbo].[GetLocations]
	@SearchText NVARCHAR(255) = NULL,
	@SportID TINYINT NULL,
	@GroupMemberID INT NULL,
	@UserID BIGINT NULL
AS
	SELECT 
		[LocationID],
		[SportID],
		[GroupMemberID], 
		[UserID],
		[Display], 
		[Lat],
		[Lng],
		[Value],
		[Country],
		[City],
		[State], 
		[Address],
		[AddressNumber], 
		[AddressFloor],
		[AddressApartament],
		[PostalCode]
	FROM
		Locations
	WHERE
		([GroupMemberID] = ISNULL(@GroupMemberID, [GroupMemberID])
			OR [GroupMemberID] = 0)
		AND ([UserID] = ISNULL(@UserID, [UserID])
			OR [UserID] IS NULL)
		AND ([SportID] = ISNULL(@SportID, [SportID])
			OR [SportID] IS NULL)
		AND ([Display] like '%' + ISNULL(@SearchText,'') + '%'
			OR [Address] like '%' + ISNULL(@SearchText,'') + '%')

