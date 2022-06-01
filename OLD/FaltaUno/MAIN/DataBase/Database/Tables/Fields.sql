CREATE TABLE [dbo].[Fields]
(
	[FieldID] INT NOT NULL PRIMARY KEY,
	[SportID] TINYINT NOT NULL, 
	[Value] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(255) NOT NULL, 
   [Default] BIT NOT NULL DEFAULT 0, 
   CONSTRAINT [FK_Fields_Sports_SportID] FOREIGN KEY ([SportID]) REFERENCES [Sports]([SportID])
)
