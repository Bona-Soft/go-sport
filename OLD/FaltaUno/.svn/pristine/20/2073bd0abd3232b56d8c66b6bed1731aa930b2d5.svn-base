CREATE PROCEDURE [dbo].[AddLocation]
	@SportID TINYINT NULL,
	@UserID BIGINT NULL,
	@GroupMemberID TINYINT = 0,
	@Display NVARCHAR(255) NULL,
	@Lat FLOAT NULL,
	@Lng FLOAT NULL,
	@Value NVARCHAR(255) NULL,
	@Country NVARCHAR(255) NULL,
	@City NVARCHAR(255) NULL,
	@State NVARCHAR(255) NULL,
	@Address NVARCHAR(255) NULL,
	@AddressNumber NVARCHAR(255) NULL, 
	@AddressFloor NVARCHAR(255) NULL,
	@AddressApartament NVARCHAR(255) NULL,
	@PostalCode NVARCHAR(255) NULL
AS
BEGIN

	INSERT INTO Locations
		([SportID],
		[UserID],
		[GroupMemberID],
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
		[PostalCode])
	VALUES
		(@SportID,
		@UserID,
		@GroupMemberID,
		@Display,
		@Lat,
		@Lng,
		@Value,
		@Country,
		@City,
		@State,
		@Address,
		@AddressNumber, 
		@AddressFloor,
		@AddressApartament,
		@PostalCode)

	RETURN SCOPE_IDENTITY()


END

