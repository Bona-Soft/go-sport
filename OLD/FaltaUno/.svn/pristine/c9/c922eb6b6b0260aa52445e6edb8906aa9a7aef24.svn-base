CREATE TABLE [dbo].[UserPrivacy]
(
	[UserPrivacyID] BIGINT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[UserID] BIGINT NOT NULL,
	[UserPrivacyDefaultID] INT NOT NULL,
	[Value] CHAR(1) NOT NULL, 
   CONSTRAINT [FK_UserPrivacy_UserPrivacyDefault] FOREIGN KEY ([UserPrivacyDefaultID]) REFERENCES [UserPrivacyDefault]([UserPrivacyDefaultID])
)
