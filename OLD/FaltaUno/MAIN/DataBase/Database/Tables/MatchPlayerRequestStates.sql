CREATE TABLE [dbo].[MatchPlayerRequestStates]
(
	[MatchPlayerRequestStateID] TINYINT NOT NULL PRIMARY KEY, 
    [Value] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(255) NOT NULL, 
	[DefaultComment] NVARCHAR(255) NOT NULL,
    [Type] CHAR(1) NOT NULL 
)
