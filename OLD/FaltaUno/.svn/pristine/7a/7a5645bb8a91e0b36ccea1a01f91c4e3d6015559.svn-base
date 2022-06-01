CREATE TABLE [dbo].[UserBondScore]
(
	[UserID] BIGINT NOT NULL ,
	[BondUserID] BIGINT NOT NULL,
	[Score] INT NOT NULL DEFAULT 0, 
   PRIMARY KEY ([BondUserID], [UserID]), 
   CONSTRAINT [FK_UserBondScore_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]),
	CONSTRAINT [FK_UserBondScore_Users_BondUserID] FOREIGN KEY ([BondUserID]) REFERENCES [Users]([UserID]),
)
