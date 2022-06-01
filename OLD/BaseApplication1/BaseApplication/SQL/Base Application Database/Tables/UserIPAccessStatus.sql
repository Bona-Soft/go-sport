CREATE TABLE [dbo].[UserIPAccessStatus](
	[ImplementationID] [int] NOT NULL DEFAULT 0,
	[UserID] [bigint] NOT NULL,
	[IP] [varchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[DateTimeCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_UserIPAccessStatus_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users]([UserID]),
	CONSTRAINT [FK_UserIPAccessStatus_USers_ImplementationID] FOREIGN KEY ([ImplementationID]) REFERENCES [dbo].[Implementations]([ImplementationID]), 
	CONSTRAINT [PK_UserIPAccessStatus] PRIMARY KEY ([UserID])
) ON [PRIMARY]
