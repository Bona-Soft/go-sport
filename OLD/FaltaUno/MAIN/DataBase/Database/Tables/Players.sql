﻿CREATE TABLE [dbo].[Players]
(
	[PlayerID] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserID] BIGINT NOT NULL, 
	[SportID] INT NOT NULL,
	[Alias] NVARCHAR(255) NULL,
	[Active] BIT NOT NULL CONSTRAINT Players_Active_Default DEFAULT 1,
	[Deleted] BIT NOT NULL CONSTRAINT Players_Deleted_Default DEFAULT 0, 
	[DateTimeCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
   CONSTRAINT [FK_Players_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID])
    
)
