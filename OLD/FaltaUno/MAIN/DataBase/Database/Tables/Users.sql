CREATE TABLE [dbo].[Users](
	[UserID] [bigint] IDENTITY(1,1) NOT NULL,
	[ImplementationID] [int] NOT NULL,
	[DefaultSportID] TINYINT NULL, 
	[LastActiveSportID] TINYINT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DefaultLanguage] VARCHAR(2) NULL, 
	[Prefix] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[SecondNames] [varchar](255) NULL,
	[LastName] [varchar](255) NULL,
	[Suffix] [varchar](255) NULL,
	[Alias] [varchar](255) NULL,
	[MobileNumber] [varchar](255) NULL,
	[LinePhoneNumber] [varchar](255) NULL,
	[WebAddress] [varchar](255) NULL,
	[BirthDate] [datetime] NULL,
	[Height] [smallint] NULL,
	[Weight] [smallint] NULL,
    [Avatar] [varchar](255) NULL,  
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [FK_Users_Sports_DefaultSportID_SportID] FOREIGN KEY ([DefaultSportID]) REFERENCES [Sports]([SportID]),
	CONSTRAINT [FK_Users_Sports_LastActiveSportID_SportID] FOREIGN KEY ([LastActiveSportID]) REFERENCES [Sports]([SportID])
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT 0 FOR [ImplementationID]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT GETUTCDATE() FOR [DateTimeCreated]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT 0 FOR [Deleted]
GO

