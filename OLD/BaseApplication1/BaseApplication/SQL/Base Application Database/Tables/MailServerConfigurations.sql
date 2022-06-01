CREATE TABLE [dbo].[MailServerConfigurations]
(
	[Name] VARCHAR(255) NOT NULL PRIMARY KEY,
	[Subject] VARCHAR(255) DEFAULT '' ,
	[Body] VARCHAR(MAX) DEFAULT '' ,
	[IsBodyHtml] BIT DEFAULT 1,
	[Priority] TINYINT DEFAULT 0,
	[From] VARCHAR(255) NOT NULL,
	[Host] VARCHAR(255) NOT NULL,
	[Port] INT DEFAULT 587,
	[EnableSsl] BIT DEFAULT 1,
	[UseDefaultCredentials] BIT DEFAULT 0,
	[MailAddress] VARCHAR(255) NOT NULL,
	[Password] VARCHAR(255) NOT NULL
)
