CREATE TABLE [dbo].[Locations]
(
	[LocationID] BIGINT NOT NULL IDENTITY(1,1), 
    [SportID] TINYINT NULL ,
	[GroupMemberID] INT NOT NULL DEFAULT 0, 
    [UserID] BIGINT NULL, 
    [Display] NVARCHAR(255) NULL, 
    [Lat] FLOAT NULL, 
	[Lng] FLOAT NULL, 
	[Value] NVARCHAR(255) NULL, 
    [Country] NVARCHAR(255) NULL, 
    [City] NVARCHAR(255) NULL, 
    [State] NVARCHAR(255) NULL, 
    [Address] NVARCHAR(255) NULL, 
	[AddressNumber] NVARCHAR(255) NULL, 
	[AddressFloor] NVARCHAR(255) NULL,
	[AddressApartament] NVARCHAR(255) NULL,
    [PostalCode] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Locations] PRIMARY KEY ([LocationID]), 
    CONSTRAINT [FK_Locations_Sports] FOREIGN KEY ([SportID]) REFERENCES [Sports]([SportID]), 
    CONSTRAINT [FK_Locations_Users] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]) 
)
