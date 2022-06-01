CREATE TABLE [dbo].[LoginNames]( 
		[LoginID] BIGINT IDENTITY(1,1) NOT NULL,
		[ImplementationID] INT NOT NULL DEFAULT 0,
		[UserID] BIGINT NOT NULL,
		[LoginName] VARCHAR(255) NOT NULL,
		[Blocked] BIT NULL DEFAULT 0,
		[Locked] BIT NULL DEFAULT 0,
		[Deleted] BIT NULL DEFAULT 0,
		[DateTimeCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [VerificationCode] VARCHAR(255) NULL, 
    [VerificationCodeExpiration] DATETIME NULL, 
    CONSTRAINT [PK_LoginNames] PRIMARY KEY CLUSTERED ([LoginID] DESC) ON [PRIMARY], 
	CONSTRAINT [FK_LoginNames_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users]([UserID]),
	INDEX IX_LoginNames NONCLUSTERED (LoginName)
 ) ON [PRIMARY]
