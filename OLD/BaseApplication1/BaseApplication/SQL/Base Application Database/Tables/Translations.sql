CREATE TABLE [dbo].[Translations]
(
	[LanguageCode] CHAR(2) NOT NULL , 
	[Module] NVARCHAR(60) NOT NULL DEFAULT '',
    [ConstantName] NVARCHAR(255) NOT NULL, 
    [Translation] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_Translations] PRIMARY KEY ([ConstantName], [Module], [LanguageCode]) 
)