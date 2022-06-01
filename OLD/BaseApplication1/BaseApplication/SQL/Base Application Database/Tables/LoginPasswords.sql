CREATE TABLE [dbo].[LoginPasswords](
	[LoginID] [bigint] NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Sequence] [tinyint] NOT NULL,
    CONSTRAINT [PK_LoginPasswords] PRIMARY KEY CLUSTERED ([LoginID] DESC, [Password] ASC) ON [PRIMARY],
	CONSTRAINT [FK_LoginPasswords_LoginNames_LoginID] FOREIGN KEY([LoginID])REFERENCES [dbo].[LoginNames] ([LoginID])
);