CREATE TABLE [dbo].[Sessions](
	[SessionID] [bigint] IDENTITY(1,1) NOT NULL,
	[LoginID] [bigint] NOT NULL,
	[Session] [varchar](255) NOT NULL UNIQUE,
	[DateTimeCreated] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[DateTimeExpiration] [datetime] NULL,
	[IP] [varchar](255) NULL,
 [UserAgent] VARCHAR(255) NULL, 
    CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED ([SessionID]) ON [PRIMARY],
 CONSTRAINT [FK_Sessions_LoginNames_LoginID] FOREIGN KEY([LoginID])REFERENCES [dbo].[LoginNames] ([LoginID]),
 INDEX [IX_Sessions_Session] NONCLUSTERED ([Session])
);