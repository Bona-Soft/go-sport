CREATE TABLE [dbo].[Users]
(
	[UserID] BIGINT NOT NULL IDENTITY(1,1), 
    [ImplementationID] INT NOT NULL DEFAULT 0, 
	 [Name] VARCHAR(255) NULL,
	 [LastName] VARCHAR(255) NULL,
    [DateTimeCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [Deleted] BIT NOT NULL DEFAULT 0, 
    [DefaultLanguage] VARCHAR(2) NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserID]) 
)
